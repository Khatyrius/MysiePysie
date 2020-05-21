using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.Models
{
    public class Student
    {
        [Key] public int id { get; set; }
        [Required] public string forename { get; set; }
        [Required] public string surname { get; set; }

        [Required] public int age { get; set; }
        public string status { get; set; }
    }
}
