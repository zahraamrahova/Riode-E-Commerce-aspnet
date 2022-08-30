using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.Models.Entities
{
    public class Faq:BaseEntity
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
