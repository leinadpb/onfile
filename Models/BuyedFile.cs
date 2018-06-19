using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models
{
    public class BuyedFile
    {
        [Key]
        public int BuyedFileID { get; set; }

        //Navigation Properties
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int UploadedFileID { get; set; }
        public UploadedFile UploadedFile { get; set; }


    }
}
