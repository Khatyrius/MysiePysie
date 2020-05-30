using System.ComponentModel.DataAnnotations;

namespace MysiePysieMVC.Models
{
    public class Subject
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Nazwa")]
        public string name { get; set; }

        [Display(Name = "Punkty ECTS")]
        public int ECTSpoints { get; set; }

        [Display(Name = "Nauczyciel")]
        public Teacher teacher { get; set; }
    }
}