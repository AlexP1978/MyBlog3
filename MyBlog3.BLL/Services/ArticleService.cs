using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog3.BLL.Interfaces;
using MyBlog3.DAL.Interfaces;
using MyBlog3.BLL.DTO;
using MyBlog3.DAL.Entities;
using AutoMapper;
using MyBlog3.BLL.Infrastructure;

namespace MyBlog3.BLL.Services
{
    public class ArticleService : IArticleService
    {
        IUnitOfWork Database { get; set; }

        public ArticleService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<ArticleDTO> GetArticles()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Article, ArticleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Article>, List<ArticleDTO>>(Database.Articles.GetAll());
        }

        public ArticleDTO GetArticle(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id статьи", "");
            var article = Database.Articles.Get(id.Value);
            if (article == null)
                throw new ValidationException("Статья не найдена", "");

            return new ArticleDTO
            {
                Name = article.Name,
                Id = article.Id,
                Author = article.Author,
                ShortBody = article.ShortBody,
                FullBody = article.FullBody,
                Category = article.Category,
                DataTxt = article.DataTxt
            };
        }

        public void CreateArticle(ArticleDTO articleDTO)
        {
            Article article = Database.Articles.Get(articleDTO.Id);
            Database.Articles.Create(article);
            Database.Save();
        }

        public IEnumerable<CommentDTO> GetComments()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(Database.Comments.GetAll());
        }

        // from Comments where ArticleId = id 
        public IEnumerable<CommentDTO> FindComment(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(Database.Comments.Find( p => p.ArticleId == id ));
        }

        public CommentDTO GetComment(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id комментария", "");
            var comment = Database.Comments.Get(id.Value);
            if (comment == null)
                throw new ValidationException("Комментарий не найден", "");

            
            return new CommentDTO
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                DataComment = comment.DataComment,
                Author = comment.Author,
                Comments = comment.Comments,
            };
        }

        public void CreateComment(CommentDTO commentDTO)
        {
            Comment comment     = new Comment();
            comment.ArticleId   = commentDTO.ArticleId;
            comment.DataComment = commentDTO.DataComment;
            comment.Author      = commentDTO.Author;
            comment.Comments    = commentDTO.Comments;

            Database.Comments.Create(comment);
            Database.Save();
        }

        public IEnumerable<PictureDTO> GetPictures()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Picture>, List<PictureDTO>>(Database.Pictures.GetAll());
        }

        public PictureDTO GetPicture(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id рисунка", "");
            var picture = Database.Pictures.Get(id.Value);
            if (picture == null)
                throw new ValidationException("Рисунок не найден", "");

            return new PictureDTO
            {
                Id = picture.Id,
                ArticleId = picture.ArticleId,
                Name = picture.Name,
                URL = picture.URL
            };
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
