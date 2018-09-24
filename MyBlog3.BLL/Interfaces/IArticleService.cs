using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog3.BLL.DTO;

namespace MyBlog3.BLL.Interfaces
{
    public interface IArticleService
    {
        ArticleDTO GetArticle(int? id);
        IEnumerable<ArticleDTO> GetArticles();
        void CreateArticle(ArticleDTO article);
        void UpdateArticle(ArticleDTO article);
        void DeleteArticle(int? id);
        CommentDTO GetComment(int? id);
        IEnumerable<CommentDTO> GetComments();
        // from Comments where ArticleId = id
        IEnumerable<CommentDTO> FindComment( int? id );
        void CreateComment(CommentDTO comment);
        PictureDTO GetPicture(int? id);
        IEnumerable<PictureDTO> GetPictures();
        void Dispose();
    }
}
