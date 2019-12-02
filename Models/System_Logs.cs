using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public enum LogType
    {
        Update,
        Login,
        Logout,
        Create,
        Register,
        Delete,

    }
    public class System_Logs
    {
        public int Id { get; set; }
        public LogType Type { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }

    }
}