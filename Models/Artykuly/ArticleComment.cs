using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class ArticleComment
    {
        public long Id { get; set; }
        public bool IsShow { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
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
        [Display(Name = "Zgłoszony")]
        public bool Reported { get; set; }
        [Display(Name = "Powód")]
        public string Reported_reason { get; set; }
        [Display(Name = "Adres IP zgłaszającego")]
        public string Reported_ip { get; set; }

    }
}