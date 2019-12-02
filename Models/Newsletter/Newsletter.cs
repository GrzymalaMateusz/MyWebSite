using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWebsite.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Treść")]
        [Required]
        [AllowHtml]
        public string Text { get; set; }
        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Wyslany")]
        public bool sended { get; set; }

        public void Send()
        {
            DAL.MyAppDbContext db = new DAL.MyAppDbContext();
            var listOfSubscribers = db.Newsletters_Subscribers;
            var mess = new Microsoft.AspNet.Identity.IdentityMessage();
            var newsletter = db.Newsletters.Find(this.Id);
            mess.Body = newsletter.Text;
            mess.Subject = newsletter.Title;
            String To = "";
            foreach (Newsletter_Subscribers item in listOfSubscribers)
            {
                To += item.Email + ";";
            }
            var delimiters = new[] { ',', ';' };
            var addresses = To.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string.Join(",", addresses);
            mess.Destination = string.Join(",", addresses);
            newsletter.sended = true;
            db.SaveChanges();



            MailMessage messs = new MailMessage();
            messs.To.Add(mess.Destination);
            messs.Subject = mess.Subject;
            messs.Body = string.Concat(mess.Body);
            messs.BodyEncoding = System.Text.Encoding.UTF8;
            messs.From = new MailAddress("admin@grzymek.cba.pl");
            messs.SubjectEncoding = System.Text.Encoding.UTF8;
            messs.IsBodyHtml = true;
            // Dołącz tutaj usługę poczty e-mail, aby wysłać wiadomość e-mail.
            SmtpClient client = new SmtpClient("mail.CBA.pl");
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("admin@grzymek.cba.pl", "Hs-39000");
            client.Send(messs);
        }
    }
}