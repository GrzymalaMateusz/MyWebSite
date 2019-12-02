using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyWebsite.DAL
{
    public class MyAppDbContext:DbContext
    {
        public MyAppDbContext():base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public System.Data.Entity.DbSet<MyWebsite.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.ProductsCategory> ProductsCategory { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Purchase> Purchases { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Photo> Photos { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.PageContact> PageContacts { get; set; }
        //Artykuły
        public System.Data.Entity.DbSet<MyWebsite.Models.Article> Articles { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.ArticleComment> ArticleComment { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.ArticleCategory> ArticleCategories { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.User> Users { get; set; }
        //Chat
        public System.Data.Entity.DbSet<MyWebsite.Models.Chat_Room> Chat_Room { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Chat_Message> Chat_Message { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Chat_Users> Chat_Users { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Newsletter> Newsletters { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Newsletter_Subscribers> Newsletters_Subscribers { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.System_Logs> System_Logs { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Notification> Notifications { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.BMWoptions> BMWoptions { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.Files> Files { get; set; }
        //CV
        public System.Data.Entity.DbSet<MyWebsite.Models.CV.CV> CV { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.CV.CVJobs> CVJobs { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.CV.CVSchool> CVSchools { get; set; }
        public System.Data.Entity.DbSet<MyWebsite.Models.CV.CVSkills> CVSkills { get; set; }
        //CMS

        //Portfolio
        public System.Data.Entity.DbSet<MyWebsite.Models.Portfolio> Portfolios { get; set; }

    }
}