using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog3.DAL.EF;
using MyBlog3.DAL.Entities;
using MyBlog3.DAL.Interfaces;
using System.Data.Entity;

namespace MyBlog3.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private ArticleContext db;

        public CommentRepository(ArticleContext context)
        {
            this.db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public Comment Get(int id)
        {
            return db.Comments.FirstOrDefault( p => p.ArticleId == id );
        }

        public void Create(Comment comment)
        {
            db.Comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public IEnumerable<Comment> Find(Func<Comment, Boolean> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }
    }
}
