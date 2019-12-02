using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models.CV
{
    public class CVJobs
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Stand { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}