using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Zakres Prac")]
        public string WorkRange { get; set; }
    }
}