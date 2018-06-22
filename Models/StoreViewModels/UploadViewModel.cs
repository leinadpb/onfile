using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models.StoreViewModels
{
    public class UploadViewModel
    {
        [Display(Name = "Name (*)")]
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Display(Name = "All file details (*)")]
        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }
        public float Length { get; set; }
        public float Price { get; set; }
        public string MainPictureUrl { get; set; }
        public string ResourceUrl { get; set; }

        [Display(Name = "Short description (*)")]
        [Required(ErrorMessage = "This field is required.")]
        public string ShortDescription { get; set; }
        
    }
}
