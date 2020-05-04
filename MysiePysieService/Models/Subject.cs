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
        public string Name { get; set; }

        public int ECTSpoints { get; set; }

        public Teacher Teacher { get; set; }
    }
}