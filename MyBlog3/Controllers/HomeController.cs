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

        //[HttpGet]
        //public ActionResult Adds()
        //{
        //    ViewBag.Name = "Добавление статьи";
        //    // передаем все объекты в динамическое свойство Articles в ViewBag
        //    return View();
        //}

        //[HttpPost]
        //public string Adds(ArticleViewModel article)
        //{
        //    // добавляем статью в базу данных
        //    article.DataTxt = DateTime.Now;
        //    article.Author = User.Identity.Name;
        //    db.Articles.Add(article);
        //    // сохраняем в бд все изменения
        //    //db.Entry(dtcomment).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return "Статья добавлена.";
        //}

        //[HttpGet]
        //public ActionResult Editing(int id)
        //{
        //    ViewBag.ArticleId = id;
        //    var dtarticle = db.Articles.FirstOrDefault(p => p.Id == id);
        //    ViewBag.Name = dtarticle.Name;
        //    ViewBag.FullBody = dtarticle.FullBody;

        //    return View();
        //}

        //[HttpPost]
        //public string Editing(Article article)
        //{
        //    // редактируем статью
        //    article.DataTxt = DateTime.Now;
        //    if ( article.Author == null ) article.Author = User.Identity.Name;
        //    //db.Articles.Add(article);
        //    // сохраняем в бд все изменения
        //    db.Entry(article).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return "Статья изменена.";
        //}

        //[HttpGet]
        //public string Deleting(Article article)
        //{
        //    // удаляем статью
        //    db.Entry(article).State = EntityState.Deleted;
        //    db.SaveChanges();
        //    // сохраняем в бд все изменения
        //    //db.SaveChanges();
        //    return "Статья удалена.";
        //}

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