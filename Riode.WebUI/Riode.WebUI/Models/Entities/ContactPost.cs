using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.Models.Entities
{
    public class ContactPost:BaseEntity
    {
        
        [Required(ErrorMessage ="Can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Can not be null")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Can not be null")]
        public string Comment { get; set; }

        public string? Answer { get; set; }
        public DateTime? AnsweredDate { get; set; }
        public int? AnsweredByUserId { get;set; }
    }
}
