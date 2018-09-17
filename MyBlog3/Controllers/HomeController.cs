using System.Collections.Generic;
using System.Web.Mvc;
using MyBlog3.Models;
using AutoMapper;
using MyBlog3.BLL.DTO;

namespace MyBlog3.Controllers
{
    public class HomeController : Controller
    {
        IArticleService articleService;
        public HomeController(IArticleService serv)
        {
            articleService = serv;
        }
        // создаем контекст данных
        //ArticleContext db = new ArticleContext();
        public ActionResult Index(int page = 1)
        {
            // получаем из бд все объекты Article
            //IEnumerable<Article> articles = db.Articles;
            IEnumerable<ArticleDTO> articleDtos = articleService.GetArticle();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ArticleDTO, ArticleViewModel>()).CreateMapper();
            var phones = mapper.Map<IEnumerable<ArticleDTO>, List<ArticleViewModel>>(articleDtos);

            //int pageSize = 3; // количество объектов на страницу
            //IEnumerable<ArticleDTO> articlesPerPages = articles.Skip((page - 1) * pageSize).Take(pageSize);
            //PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articles.Count() };
            //IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Articles = articlesPerPages };
            // возвращаем представление
            //return View(ivm);
            return View(articleDtos);
        }

        //[HttpGet]
        //public ActionResult More(int id)
        //{
        //    ViewBag.ArticleId = id;
        //    var dtarticle = db.Articles.FirstOrDefault(p => p.Id == id);
        //    ViewBag.Name = dtarticle.Name;
        //    ViewBag.FullBody = dtarticle.FullBody;
        //    // получаем из бд все объекты Article
        //    IEnumerable<Comment> comments = db.Comments.Where(v => v.ArticleId == id).ToList();
        //    // передаем все объекты в динамическое свойство Articles в ViewBag
        //    ViewBag.Comments = comments;
        //    return View();
        //}

        //[HttpPost]
        //public string More(Comment comment)
        //{
        //    // добавляем информацию о статье
        //    comment.DataComment = DateTime.Now;
        //    db.Comments.Add(comment);
        //    // сохраняем в бд все изменения
        //    //db.Entry(dtcomment).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return "Спасибо, за комментарий!";
        //}

        //[HttpGet]
        //public ActionResult Adds()
        //{
        //    ViewBag.Name = "Добавление статьи";
        //    // передаем все объекты в динамическое свойство Articles в ViewBag
        //    return View();
        //}

        //[HttpPost]
        //public string Adds(Article article)
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