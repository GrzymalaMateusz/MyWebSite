using MyWebsite.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class Chat_Message
    {
        public int Id { get; set; }
        public virtual Chat_Users User { get; set; }
        public string Text { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public Chat_Message addMessageToRoom(int room, string user, string message)
        {
            MyAppDbContext db = new MyAppDbContext();
            var room_chat = db.Chat_Room.Find(room);
            var user_name = room_chat.Users.SingleOrDefault(s => s.User.Email == user);
            Chat_Message m = new Chat_Message()
            {
                Text = message,
                User = user_name,
                Date = DateTime.Now
            };
            room_chat.Messages.Add(m);
            room_chat.LastUpdate = DateTime.Now;
            user_name.LastView = DateTime.Now;
            db.Chat_Message.Add(m);
            db.SaveChanges();
            return m;
        }
    }
}