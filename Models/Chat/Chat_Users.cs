using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Chat_Users
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime LastView { get; set; }
    }
}