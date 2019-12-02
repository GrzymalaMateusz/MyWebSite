using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyWebsite.DAL
{
    public class Initializer:DropCreateDatabaseIfModelChanges<MyAppDbContext>
    {
        protected override void Seed(MyAppDbContext context)
        {
            context.SaveChanges();
        }
    }
}