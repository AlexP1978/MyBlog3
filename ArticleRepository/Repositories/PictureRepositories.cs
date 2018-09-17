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
    public class PictureRepository : IRepository<Picture>
    {
        private ArticleContext db;

        public PictureRepository(ArticleContext context)
        {
            this.db = context;
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public void Create(Picture picture)
        {
            db.Pictures.Add(picture);
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
        }

        public IEnumerable<Picture> Find(Func<Picture, Boolean> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
        }
    }
}
