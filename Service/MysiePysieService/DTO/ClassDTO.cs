using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.DTO
{
    public class ClassDTO
    {
       public int id { get; set; }
       [Required] public string name { get; set; }
       public List<Student> students { get; set; }
    }
}
