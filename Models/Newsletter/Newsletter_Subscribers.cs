using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Newsletter_Subscribers
    {
        public int Id { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Token { get; set; }
    }
}