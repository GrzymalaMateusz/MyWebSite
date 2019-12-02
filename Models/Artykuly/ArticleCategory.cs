using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
    }
}