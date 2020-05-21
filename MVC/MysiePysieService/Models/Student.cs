using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MysiePysieService.Models
{
    public class Student
    {
        [DisplayName("Id")]
        public int id { get; set; }
        [DisplayName("Imię")]
        public string forename { get; set; }
        [DisplayName("Nazwisko")]
        public string surname { get; set; }
        [DisplayName("Wiek")]
        public int age { get; set; }
        [DisplayName("Status")]
        public string status { get; set; }
    }
}