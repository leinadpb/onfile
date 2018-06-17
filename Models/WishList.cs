using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models
{
    public class WishList
    {
        [Key]
        public int WishListID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        //Navigation Properties
        public ICollection<File> Files { get; set; }

    }
}
