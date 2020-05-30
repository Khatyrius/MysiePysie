using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MysiePysieMVC.Models
{
    public class UserAuth
    {
        [DisplayName("Nazwa użytkownika")]
        public string username { get; set; }
        [DisplayName("Hasło")]
        public string password { get; set; }
    }
}