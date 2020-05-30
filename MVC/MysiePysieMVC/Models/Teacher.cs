using System.ComponentModel;

namespace MysiePysieMVC.Models
{
    public class Teacher
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Imię")]
        public string forename { get; set; }

        [DisplayName("Nazwisko")]
        public string surname { get; set; }

        [DisplayName("Wiek")]
        public int age { get; set; }
    }
}