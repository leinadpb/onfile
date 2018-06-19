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

        [StringLength(200, ErrorMessage = "Wishlist's name must be below 200 characters.")]
        [Required(ErrorMessage = "Please, specify a name for the list.")]
        public string Name { get; set; }

        [StringLength(600, ErrorMessage = "Wishlist's description must be below 600 characters.")]
        public string Description { get; set; }

        //Navigation Properties
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<UploadedFile> UploadedFiles { get; set; }


    }
}
