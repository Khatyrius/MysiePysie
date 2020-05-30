using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.DTO
{
    public class SubjectDTO
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public int ECTSpoints { get; set; }
        [Required] public Teacher teacher { get; set; }
    }
}
