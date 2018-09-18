using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog3.BLL.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ShortBody { get; set; }
        public string FullBody { get; set; }
        public DateTime DataTxt { get; set; }
        public string Category { get; set; }
    }
}
