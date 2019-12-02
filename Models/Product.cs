using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MyWebsite.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Miniaturka")]
        public string Thumb { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public double Prine { get; set; }
        [Display(Name = "Demo")]
        public string Demo { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Opis")]
        public virtual ProductsCategory Category { get; set; }
        [Display(Name = "Krótki opis")]
        public string ShortDescription { get; set; }
        [Display(Name = "Data Utworzenia")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Data Modyfikacji")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ModDate { get; set; }
        [Display(Name = "Create IP")]
        public string CreateIp { get; set; }
        [Display(Name = "Mod IP")]
        public string ModIp { get; set; }
        [ScriptIgnore]
        public virtual ICollection<Models.Photo> Photos { get; set; }
    }
}