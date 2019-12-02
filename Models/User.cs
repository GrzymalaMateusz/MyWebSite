using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public enum UserType
    {
        Admin,
        Mod,
        User
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [Display(Name = "Imię")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Imię nie może zawierać cyfr.")]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 3)]
        public string ForName { get; set; }
        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Nazwisko nie może zawierać cyfr.")]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 3)]
        public string SurName { get; set; }
        [Display(Name = "Numer Telefonu")]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Invalid Phone Number.")]
        public long Phone { get; set; }
        [Display(Name = "Avatar")]
        public string Photo { get; set; }
        public DateTime LastLogin { get; set; }
        public virtual ICollection<Notification> User_Notification { get; set; }
        public DateTime Notifification_last_see { get; set; }
        public UserType UserType { get; set; }
    }
}