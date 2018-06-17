using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnFile.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please, provide your firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please, provide your lastname.")]
        public string Lastanme { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please, provide your birthdate")]
        public DateTime Birthdate { get; set; }

        [DataType(DataType.Date)]
        private DateTime SignInDate;

        public DateTime MyProperty
        {
            get { return SignInDate; }
            set { SignInDate = DateTime.Now.Date; }
        }

        public string Website { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
