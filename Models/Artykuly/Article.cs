using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebsite.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Display(Name = "Miniaturka")]
        public string Thumb { get; set; }
        [Display(Name = "Tytuł")]
        [Required]
        public string Title{ get; set; }
        [AllowHtml]
        [Display(Name = "Treść")]
        public string Text { get; set; }
        [Display(Name = "Kategoria")]
        public virtual ArticleCategory Category { get; set; }
        [Display(Name = "Wyświetlenia")]
        public int Views { get; set; }
        [Display(Name = "Data Publikacji")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
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
        [Display(Name = "Użytkownik")]
        public virtual User User { get; set; }
        [Display(Name = "Galeria")]
        public virtual ICollection<Photo> Photos { get; set; }
        [Display(Name = "Komentarze")]
        public virtual ICollection<ArticleComment> Comments { get; set; }
        [Display(Name = "Tagi")]
        public string Tags { get; set; }
        [Display(Name = "Komentarze")]
        public bool CommentAloved { get; set; }
        [Display(Name = "Pokaż Użytkownika")]
        public bool ShowUser { get; set; }
    }
}