using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.BrandModule
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
