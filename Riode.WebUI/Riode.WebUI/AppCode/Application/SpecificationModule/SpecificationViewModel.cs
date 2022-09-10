using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
