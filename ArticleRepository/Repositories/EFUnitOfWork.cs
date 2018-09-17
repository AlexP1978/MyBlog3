using System;
using MyBlog3.DAL.EF;
using MyBlog3.DAL.Interfaces;
using MyBlog3.DAL.Entities;

namespace MyBlog3.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ArticleContext db;
        private ArticleRepository articleRepository;
        private CommentRepository commentRepository;
        private PictureRepository pictureRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ArticleContext(connectionString);
        }
        public IRepository<Article> Articles
        {
            get
            {
                if (articleRepository == null)
                    articleRepository = new ArticleRepository(db);
                return articleRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new CommentRepository(db);
                return commentRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
