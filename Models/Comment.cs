using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnFile.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        [MaxLength(800, ErrorMessage = "Comment is too long.")]
        public string Text { get; set; }

        //Navigation properties
        public int UploadedFileID { get; set; }
        public UploadedFile UploadedFile { get; set; }

        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
