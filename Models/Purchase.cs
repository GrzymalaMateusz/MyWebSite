using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace MyWebsite.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        [Display(Name = "Informacje dodatkowe")]
        public string info { get; set; }
        [Display(Name = "Mail kontaktowy")]
        public string Email { get; set; }
        [Display(Name = "Nazwa/Imię i Nazwisko")]
        public string Name { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Kod pocztowy, Miejscowość")]
        public string City { get; set; }
        [ScriptIgnore]
        public virtual Product Product { get; set; }
    }
}