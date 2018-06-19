using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models.StoreViewModels
{
    public class UploadViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Length { get; set; }
        public float Price { get; set; }
        public string MainPictureUrl { get; set; }
        public string ResourceUrl { get; set; }

    }
}
