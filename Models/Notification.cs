using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public enum Notification_type
    {
        comment,
        purchase,
        events,
        message,
        mail
    }
    public class Notification
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool isReaded { get; set; }
        public Notification_type Type { get; set; }
    }
}