using System.ComponentModel;

namespace MysiePysieMVC.Models
{
    public class UserModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Nick")]
        public string username { get; set; }
        [DisplayName("Imię")]
        public string firstname { get; set; }
        [DisplayName("Nazwisko")]
        public string lastname { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        public string password { get; set; }
        [DisplayName("Numer Telefonu")]
        public string phone { get; set; }
        [DisplayName("Rola")]
        public int userStatus { get; set; }
    }
}