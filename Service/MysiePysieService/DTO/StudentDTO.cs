using MysiePysieService.Models;
using System.ComponentModel.DataAnnotations;


namespace MysiePysieService.DTO
{
    public class StudentDTO
    {
        public int id { get; set; }
        [Required] 
        public string forename { get; set; }

        [Required] 
        public string surname { get; set; }

        [Required] 
        public int age { get; set; }

        public string status { get; set; }

        public Class @class { get; set; }
    }
}
