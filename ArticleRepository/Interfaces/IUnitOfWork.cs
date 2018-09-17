using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog3.DAL.Entities;
using MyBlog3.DAL.EF;

namespace MyBlog3.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> Articles { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Picture> Pictures { get; }
        void Save();
    }
}
