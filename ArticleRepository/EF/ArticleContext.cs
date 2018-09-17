using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using MyBlog3.DAL.Entities;


namespace MyBlog3.DAL.EF
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(string connectionString)
            : base(connectionString)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}