using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestBlog.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new Collection<Comment>();
            Tags = new Collection<Tag>();
        }

        public int ID { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [MinLength(5), MaxLength(50)]
        public string Title { get; set; }
        [MinLength(5), MaxLength(1000)]
        public string Body { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}