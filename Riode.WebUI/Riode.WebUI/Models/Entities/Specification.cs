namespace Riode.WebUI.Models.Entities
{
    public class Specification:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<SpecificationCategoryItem> SpecificationCategoryCollection { get; set; } 
    }
}
