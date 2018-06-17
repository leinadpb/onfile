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
        public ICollection<File> Files { get; set; }

        public ApplicationUser User { get; set; }
        public int ApplicationUserID { get; set; }

    }
}
