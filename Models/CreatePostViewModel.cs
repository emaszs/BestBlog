using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestBlog.Models
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }
}