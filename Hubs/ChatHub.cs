using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyWebsite.DAL;
using MyWebsite.Models;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        MyAppDbContext db = new MyAppDbContext();

        public void Send(int id,string user, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var m=new Chat_Message().addMessageToRoom(id, user, message);
            string name = "";
            if(m.User.User.ForName!=null|| m.User.User.SurName != null)
            {
                name = m.User.User.ForName + " " + m.User.User.SurName;
            }
            else
            {
                name = m.User.User.Email;
            }
            Clients.Group(id.ToString()).addChatMessage(m.User.User.Email, name,m.User.User.Photo, message);
            
            
        }
        public void Join(string id)
        {
            // Call the addNewMessageToPage method to update clients.
            Groups.Add(Context.ConnectionId, id);
        }
        public void Leave(int id)
        {
            Groups.Remove(Context.ConnectionId, id.ToString());
        }
    }
}