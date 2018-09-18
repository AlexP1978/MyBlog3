using Ninject.Modules;
using MyBlog3.BLL.Services;
using MyBlog3.BLL.Interfaces;

namespace MyBlog3.Util
{
    public class ArticleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleService>().To<ArticleService>();
        }
    }
}