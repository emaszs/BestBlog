using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Required]
        public DateTime dateTime { get; set; }
        [MaxLength(25), MinLength(5)]
        public string name { get; set; }
        [EmailAddress, Required]
        public string email { get; set; }
        [MinLength(5), MaxLength(200)]
        public string body { get; set; }

        public virtual Post post { get; set; }
    }
}