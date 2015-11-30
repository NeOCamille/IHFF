using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Emailadres moet worden ingevuld!")]
        [Display(Name = "Emailadres")]
        public string EmailAdres { get; set; }

        [Required(ErrorMessage = "Wachtwoord moet worden ingevuld!")]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        public Login()
        {

        }

        public Login(string EmailAdres, string Password)
        {
            this.EmailAdres = EmailAdres;
            this.Password = Password;
        }
    }
}