using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models
{
    public class UploadedFile
    {
        [Key]
        public int UploadedFileID { get; set; }

        [StringLength(80, ErrorMessage = "Title too long.")]
        [Required(ErrorMessage = "Please specify a title.")]
        public string Name { get; set; }

        [StringLength(1800, ErrorMessage = "Description is too long.")]
        [Required(ErrorMessage = "Please, provide a description.")]
        public string Description { get; set; }

        private int _DownloadedTimes;

        public int DownloadedTimes
        {
            get { return _DownloadedTimes; }
            set { _DownloadedTimes = 0; }
        }

        [StringLength(800, ErrorMessage = "Picture url is too long.")]
        public string MainPictureUrl { get; set; }

        [Required(ErrorMessage = "Please, select a price.")]
        [Range(0.0,1, ErrorMessage = "The price for this file must be from $0.0 to $1.00")]
        public float Price { get; set; }

        [StringLength(600, ErrorMessage = "Short description is too long.")]
        public string ShortDescription { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime PublishedDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime LastTimeDownloaded { get; set; }

        [StringLength(1200, ErrorMessage = "The provided string is too long.")]
        [Required]
        public string ResourceUrl { get; set; }

        [Required]
        public string Visibility { get; set; }

        //Navigation Properties
        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<OptionalPicture> OptionalPictures { get; set; }

        public int WishListID { get; set; }
        public WishList WishList { get; set; }


    }
}
