using System.Collections.Generic;
using System.Web.Mvc;
using MyBlog3.Models;
using AutoMapper;
using MyBlog3.BLL.DTO;
using MyBlog3.BLL.Interfaces;
using MyBlog3.BLL.Services;
using MyBlog3.BLL.Infrastructure;
using System;

namespace MyBlog3.Controllers
{
    public class HomeController : Controller
    {
        IArticleService articleService;
        public HomeController(IArticleService serv)
        {
            articleService = serv;
        }

        public ActionResult Index(int page = 1)
        {
            IEnumerable<ArticleDTO> articleDtos = articleService.GetArticles();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ArticleDTO, ArticleViewModel>()).CreateMapper();
            var articles = mapper.Map<IEnumerable<ArticleDTO>, List<ArticleViewModel>>(articleDtos);

            //int pageSize = 3; // the number of objects on the page
            //IEnumerable<ArticleDTO> articlesPerPages = articles.Skip((page - 1) * pageSize).Take(pageSize);
            //PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articles.Count() };
            //IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Articles = articlesPerPages };
            // возвращаем представление
            //return View(ivm);
            return View(articles);
        }

        [HttpGet]
        public ActionResult More(int id)
        {
            try
            {
                ViewBag.ArticleId = id;

                ArticleDTO article = articleService.GetArticle(id);
                var dtarticle = new ArticleViewModel
                {
                    Id       = article.Id,
                    Name     = article.Name,
                    DataTxt  = article.DataTxt,
                    FullBody = article.FullBody,
                };
                ViewBag.Name = dtarticle.Name;
                ViewBag.FullBody = dtarticle.FullBody;

                IEnumerable<CommentDTO> commentDtos = articleService.FindComment(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, CommentViewModel>()).CreateMapper();
                var comments = mapper.Map<IEnumerable<CommentDTO>, List<CommentViewModel>>(commentDtos);

                return View(comments);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public string More(CommentViewModel comment)
        {
            // add comment
            CommentDTO commentDTO = new CommentDTO();
            commentDTO.ArticleId = comment.ArticleId;
            commentDTO.DataComment = DateTime.Now;
            commentDTO.Author = User.Identity.Name;
            commentDTO.Comments = comment.Comments;
            articleService.CreateComment(commentDTO);
            return "Спасибо, за комментарий!";
        }

        [HttpGet]
        public ActionResult Adds()
        {
            ViewBag.Name = "Добавление статьи";
            // передаем все объекты в динамическое свойство Articles в ViewBag
            return View();
        }

        [HttpPost]
        public string Adds(ArticleViewModel articleView)
        {
            // add article
            ArticleDTO articleDTO = new ArticleDTO();
            articleDTO.Name       = articleView.Name;
            articleDTO.DataTxt    = DateTime.Now;
            articleDTO.Author     = User.Identity.Name;
            articleDTO.Category   = articleView.Category;
            articleDTO.ShortBody  = articleView.ShortBody;
            articleDTO.FullBody   = articleView.FullBody;

            articleService.CreateArticle(articleDTO);
            return "Статья добавлена.";
        }

        [HttpGet]
        public ActionResult Editing(int id)
        {
            ViewBag.ArticleId = id;

            ArticleDTO article = articleService.GetArticle(id);
            var dtarticle = new ArticleViewModel
            {
                Id        = article.Id,
                Name      = article.Name,
                Category  = article.Category,
                Author    = article.Author,
                DataTxt   = article.DataTxt,
                ShortBody = article.ShortBody,
                FullBody  = article.FullBody,
            };
            ViewBag.Name      = dtarticle.Name;
            ViewBag.FullBody  = dtarticle.FullBody;
            ViewBag.ShortBody = dtarticle.ShortBody;
            ViewBag.Author    = dtarticle.Author;

            return View();
        }

        [HttpPost]
        public string Editing(ArticleViewModel articleView)
        {
            //// редактируем статью
            //article.DataTxt = DateTime.Now;
            //if (article.Author == null) article.Author = User.Identity.Name;
            ////db.Articles.Add(article);
            //// сохраняем в бд все изменения
            //db.Entry(article).State = EntityState.Modified;
            //db.SaveChanges();
            // add article
            ArticleDTO articleDTO = new ArticleDTO();
            articleDTO.Name       = articleView.Name;
            articleDTO.DataTxt    = DateTime.Now;
            articleDTO.Author     = User.Identity.Name;
            articleDTO.Category   = articleView.Category;
            articleDTO.ShortBody  = articleView.ShortBody;
            articleDTO.FullBody   = articleView.FullBody;

            articleService.UpdateArticle(articleDTO);

            return "Статья изменена.";
        }

        [HttpGet]
        public string Deleting(int id)
        {
            //// удаляем статью
            //db.Entry(article).State = EntityState.Deleted;
            //db.SaveChanges();
            //// сохраняем в бд все изменения
            ////db.SaveChanges();

            articleService.DeleteArticle(id);

            return "Статья удалена.";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания вашего приложения.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ваша страница контактов.";

            return View();
        }
    }
}