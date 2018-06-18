using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models
{
    public class OptionalPicture
    {
        [Key]
        public int OptionalPictureID { get; set; }

        [Required(ErrorMessage = "Please, upload a picture.")]
        [StringLength(1800, ErrorMessage = "Url is too long.")]
        public string PictureUrl { get; set; }

        //Navigation Properties
        public int UploadedFileID { get; set; }
        public UploadedFile UploadedFile { get; set; }


    }
}
