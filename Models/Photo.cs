using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
    }
}