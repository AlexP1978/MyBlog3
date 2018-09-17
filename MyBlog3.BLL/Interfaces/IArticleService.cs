using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog3.BLL.DTO;

namespace MyBlog3.BLL.Interfaces
{
    interface IArticleService
    {
        ArticleDTO GetArticle(int? id);
        IEnumerable<ArticleDTO> GetArticles();
        CommentDTO GetComment(int? id);
        IEnumerable<CommentDTO> GetComments();
        PictureDTO GetPicture(int? id);
        IEnumerable<PictureDTO> GetPictures();
        void Dispose();
    }
}
