using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class PageContact
    {
        public int Id { get; set; }
        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Treść")]
        public string Text { get; set; }
        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Adres Ip")]
        public string Ip { get; set; }
    }
}