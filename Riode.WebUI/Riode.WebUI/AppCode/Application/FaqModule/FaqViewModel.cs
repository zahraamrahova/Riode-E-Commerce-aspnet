using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.FaqModule
{
    public class FaqViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
