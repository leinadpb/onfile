using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
