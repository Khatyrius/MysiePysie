using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MysiePysieService.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Forename { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Wiek")]
        public int Age { get; set; }
    }
}