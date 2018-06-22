using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace OnFile.Services
{
    public class AmazonUploader
    {
        public Amazon.S3.AmazonS3Client S3Client = null;

        public AmazonUploader(string accessKeyId, string secretAccessKey, string serviceUrl)
        {
            Amazon.S3.AmazonS3Config s3Config = new Amazon.S3.AmazonS3Config();
            s3Config.ServiceURL = serviceUrl;

            this.S3Client = new Amazon.S3.AmazonS3Client(accessKeyId, secretAccessKey, s3Config);
        }

        public async Task UploadFile(string filePath, string s3Bucket, string newFileName, bool deleteLocalFileOnSuccess)
        {
            //save in s3
            Amazon.S3.Model.PutObjectRequest s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest.FilePath = filePath;
            s3PutRequest.BucketName = s3Bucket;
            //s3PutRequest.ContentType = contentType;

            //key - new file name
            if (!string.IsNullOrWhiteSpace(newFileName))
            {
                s3PutRequest.Key = newFileName;
            }

            s3PutRequest.Headers.Expires = new DateTime(2020, 1, 1);

            try
            {
                Amazon.S3.Model.PutObjectResponse s3PutResponse = await S3Client.PutObjectAsync(s3PutRequest);

                if (deleteLocalFileOnSuccess)
                {
                    //Delete local file
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                //handle exceptions
            }
        }
    }
}
