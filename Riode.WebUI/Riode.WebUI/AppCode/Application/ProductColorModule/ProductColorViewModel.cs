using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string HexCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
