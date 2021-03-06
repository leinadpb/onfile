﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnFile.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please, provide your firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please, provide your lastname.")]
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please, provide your birthdate")]
        public DateTime Birthdate { get; set; }

        [DataType(DataType.Date)]
        private DateTime _SignInDate;

        public DateTime SignInDate
        {
            get { return _SignInDate; }
            set { _SignInDate = DateTime.Now.Date; }
        }

        public string Website { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Sex { get; set; }

        [MaxLength(200, ErrorMessage = "Sorry, description is too long.")]
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<BoughtFile> BoughtFiles { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<UploadedFile> UploadedFiles { get; set; }
        public ICollection<Comment> Comments { get; set; }




    }
}
