using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog3.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime DataComment { get; set; }
        public string Author { get; set; }
        public string Comments { get; set; }
    }
}
