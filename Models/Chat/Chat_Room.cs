using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Chat_Room
    {
        public int ID { get; set; }
        public virtual ICollection<Models.Chat_Users> Users{ get; set; }
        public virtual ICollection<Models.Chat_Message> Messages { get; set; }
        public DateTime LastUpdate { get; set; }
    }
   
}