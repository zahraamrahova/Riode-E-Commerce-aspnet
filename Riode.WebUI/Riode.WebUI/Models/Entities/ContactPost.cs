using Resources;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.Models.Entities
{
    public class ContactPost:BaseEntity
    {
        [Display(ResourceType=typeof(ContactResource),Name ="Name")]
        [Required(ErrorMessageResourceType =typeof(ContactResource),ErrorMessageResourceName = "Cannotbeempty")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(ContactResource), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "Cannotbeempty")]
        [EmailAddress(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "InvalidEmailAddress")]
        public string Email { get; set; }
        [Display(ResourceType = typeof(ContactResource), Name = "Comment")]
        [Required(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "Cannotbeempty")]
        public string Comment { get; set; }

        public string? Answer { get; set; }
        public DateTime? AnsweredDate { get; set; }
        public int? AnsweredByUserId { get;set; }
    }
}
