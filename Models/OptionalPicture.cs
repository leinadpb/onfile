using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnFile.Models
{
    public class OptionalPicture
    {
        [Key]
        public int OptionalPictureID { get; set; }

        public string PictureUrl { get; set; }

        //Navigation Properties
        public File File { get; set; }

    }
}
