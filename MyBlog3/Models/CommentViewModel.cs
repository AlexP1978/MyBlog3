using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog3.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime DataComment { get; set; }
        public string Author { get; set; }
        public string Comments { get; set; }
    }
}