using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.ProductSizeModule
{
    public class ProductSizeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Abbr { get; set; }
        [Required]
        public string Name { get; set; }
  
        public string Description { get; set; }
    }
}
