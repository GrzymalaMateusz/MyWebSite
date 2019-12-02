using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models.CV
{
    public class CVSchool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string TitleOfThesis { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}