using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnFile.Models
{
    public class File
    {
        [Key]
        public int FileID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DownloadedTimes { get; set; }

        public int AuthorID { get; set; }

        public string MainPictureUrl { get; set; }

        public float Price { get; set; }

        public string ShortDescription { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime LastTimeDownloaded { get; set; }

        public string ResourceUrl { get; set; }

        public string Visibility { get; set; }

        //Navigation Properties
        public ICollection<OptionalPicture> OptionalPictures { get; set; }

        public WishList WishList { get; set; }

        public BuyedFile BuyedFile { get; set; }
        
    }
}
