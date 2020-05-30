using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        [Required] public string username { get; set; }
        [Required] public string firstname { get; set; }
        [Required] public string lastname { get; set; }
        public string email { get; set; }
        [Required] public string password { get; set; }
        public string phone { get; set; }
        public int userStatus { get; set; }
    }
}
