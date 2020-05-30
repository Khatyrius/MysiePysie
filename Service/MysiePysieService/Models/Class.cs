using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MysiePysieService.Models
{
    public class Class
    {
        [Key] public int id { get; set; }
        [Required] public string name { get; set; }
        public ICollection<Student> students { get; set; }
    }
}
