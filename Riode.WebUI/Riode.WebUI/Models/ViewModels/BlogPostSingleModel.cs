

using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Models.ViewModels
{
    public class BlogPostSingleModel
    {
        public BlogPost Post { get; set; }

        public List<Category> Categories { get; set; }

        //public List<BlogPostComment> Comments { get; set; }
    }
}
