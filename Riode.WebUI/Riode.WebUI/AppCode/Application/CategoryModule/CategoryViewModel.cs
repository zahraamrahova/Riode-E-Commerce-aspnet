using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.CategoryModule
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
