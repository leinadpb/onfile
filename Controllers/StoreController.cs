using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnFile.Models;
using OnFile.Models.StoreViewModels;
using OnFile.Services;
using Microsoft.Extensions.Configuration;
using Amazon;
using Amazon.S3;

using Amazon.S3.Model;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.SharedInterfaces;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OnFile.Data;

namespace OnFile.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class StoreController : Controller
    {
        IConfiguration _configuration;
        IHostingEnvironment _hostingEnviroment;
        ApplicationDbContext _context;

        public StoreController(IConfiguration config, IHostingEnvironment host, ApplicationDbContext ctx)
        {
            _configuration = config;
            _hostingEnviroment = host;
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile ResourceUrl, string Name, string ShortDescription, string Description, IFormFile CustomThumbnail, bool withCustomThumbnail)
        {

            string bucketName = _configuration.GetSection("S3Bucket").GetValue<String>("BucketName");
            string AccessKey = _configuration.GetSection("S3Bucket").GetValue<String>("AWSAccessKey");
            string SecretKey = _configuration.GetSection("S3Bucket").GetValue<String>("AWSSecretKey");
            string newFileName_File = "";
            string newFileName_OptionalPicture = "";
            bool UploadedSingleFile = false;
            bool UploadedFileWithThumb = false;

            UploadViewModel model = null;
            
            if(ResourceUrl != null)
            {
                IFormFile file = ResourceUrl;
                if(Name != null && ShortDescription != null && Description != null)
                {
                    newFileName_File = DateTime.Now.Ticks.ToString() + "-" + file.FileName;
                    if (withCustomThumbnail) //Upload file and custom thumbnail
                    {
                        if(CustomThumbnail != null)
                        {
                            newFileName_OptionalPicture = DateTime.Now.ToString() + "-" + CustomThumbnail.FileName;
                            bool canContinue = false;
                            //bool successfullOperation = false;
                            if (await TryUploadFile(AccessKey, SecretKey, bucketName, file, newFileName_File))
                            {
                                canContinue = true;
                            }
                            else
                            {
                                ViewBag.FaultInformation = "Cannot upload main resource. Please, try again later.";
                                return View();
                            }
                            if (canContinue)
                            {
                                if (await TryUploadOptionalPicture(AccessKey, SecretKey, bucketName, CustomThumbnail, newFileName_OptionalPicture))
                                {
                                    //successfullOperation = true;
                                    ViewBag.SuccessInformation = "Your file has been uploaded successfully! You have uploaded a file with a custom thumbnail.";
                                    UploadedFileWithThumb = true;
                                    //return View();
                                }
                            }
                        }
                        else
                        {
                            ViewBag.FaultInformation = "Please, select a custom thumbnail for your file.";
                            return View();
                        }
                    }
                    else //Upload just file
                    {
                        if (await TryUploadFile(AccessKey, SecretKey, bucketName, file, newFileName_File))
                        {
                            //ViewBag.SuccessInformation = "Your file has been uploaded successfully!";
                            UploadedSingleFile = true;
                            //return View();
                        }
                    }
                }
                else
                {
                    ViewBag.FaultInformation = "Por favor, rellene todos los campos obligatorios.";
                    return View();
                }
            }
            else
            {
                ViewBag.FaultInformation = "Por favor, seleccione un archivo para subir.";
                return View();
            }

            if (UploadedSingleFile)
            {
                var uf = new UploadedFile() {
                    Name = Name,
                    Description = Description,
                    ShortDescription = ShortDescription,
                    DownloadedTimes = 0,
                    Visible = true,
                    MainPictureUrl = Path.Combine(_hostingEnviroment.WebRootPath, "images", "default_file_image.png"),
                    Price = 1,
                    PublishedDate = DateTime.Now,
                    ResourceUrl = bucketName + @"/resources/" + newFileName_File,
                    FileLength = ResourceUrl.Length,
                    Rating = 0
                };
                try
                {
                    await _context.UploadedFiles.AddAsync(uf);
                    ViewBag.dbSuccess = "Your file has been uploaded successfully!";
                    return View();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    ViewBag.dbError = "Couldn't save to database.";
                }
            }
            if (UploadedFileWithThumb)
            {
               
                var uf = new UploadedFile()
                {
                    Name = Name,
                    Description = Description,
                    ShortDescription = ShortDescription,
                    DownloadedTimes = 0,
                    Visible = true,
                    MainPictureUrl = bucketName + @"/OptionalPictures/" + newFileName_OptionalPicture,
                    Price = 1,
                    PublishedDate = DateTime.Now,
                    ResourceUrl = bucketName + @"/resources/" + newFileName_File,
                    FileLength = ResourceUrl.Length,
                    Rating = 0
                };
                var op = new OptionalPicture()
                {
                    PictureUrl = uf.ResourceUrl,
                    UploadedFile = uf
                };
                uf.OptionalPictures.Add(op);
                try
                {
                    await _context.UploadedFiles.AddAsync(uf);
                    await _context.OptionalPictures.AddAsync(op);
                    ViewBag.dbSuccess = "Your file has been uploaded successfully!";
                    return View();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    ViewBag.dbError = "Couldn't save to database.";
                }
            }

            return View(model);
        }

        private async Task<Boolean> TryUploadOptionalPicture(string AccessKey, string SecretKey, string bucketName, IFormFile picture, string newFileName)
        {
            IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.USEast1);
            PutObjectRequest request = new PutObjectRequest();
            request.BucketName = bucketName + @"/OptionalPictures";
            request.ContentType = picture.ContentType;
            request.Key = newFileName;
            request.InputStream = picture.OpenReadStream();
            request.AutoCloseStream = true;
            try
            {
                await client.PutObjectAsync(request);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return false;
        }
        private async Task<Boolean> TryUploadFile(string AccessKey, string SecretKey, string bucketName, IFormFile file, string newFileName)
        {
            IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.USEast1);
            PutObjectRequest request = new PutObjectRequest();
            request.BucketName = bucketName + @"/resources";
            request.ContentType = file.ContentType;
            request.Key = newFileName;
            request.InputStream = file.OpenReadStream();
            request.AutoCloseStream = true;
            try
            {
                await client.PutObjectAsync(request);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return false;
        }

        [HttpGet]
        public IActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy(UploadedFile file)
        {
            return View();
        }
    }
}