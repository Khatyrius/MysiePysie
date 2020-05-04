using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MysiePysieService.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        public int Age { get; set; }

        [Required]
        public string Status { get; set; }
    }
}