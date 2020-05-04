using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MysiePysieService.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Punkty ECTS")]
        public int ECTSpoints { get; set; }

        [Display(Name = "Nauczyciel")]
        public Teacher Teacher { get; set; }
    }
}