using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models.CV
{
    public class CV
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string ForName { get; set; }
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Opis")]
        public string Description  { get; set; }
        [Display(Name = "Doświadczenie")]
        public virtual ICollection<Models.CV.CVJobs> Jobs { get; set; }
        [Display(Name = "Wykształcenie")]
        public virtual ICollection<Models.CV.CVSchool> School { get; set; }
        [Display(Name = "Umiejętności")]
        public virtual ICollection<Models.CV.CVSkills> Skill { get; set; }
    }
}