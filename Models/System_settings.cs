using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class System_settings
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyValue { get; set; }
    }
}