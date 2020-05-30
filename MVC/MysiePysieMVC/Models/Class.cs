using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MysiePysieMVC.Models
{
    public class Class
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string name { get; set; }

        [Display(Name = "Studenci")]
        public List<Student> students { get; set; }
    }
}