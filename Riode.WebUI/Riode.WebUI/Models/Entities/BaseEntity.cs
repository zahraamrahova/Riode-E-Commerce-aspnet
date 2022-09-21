using Riode.WebUI.Models.Entities.Membership;

namespace Riode.WebUI.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int? CreatedByUserId { get; set; }
        public virtual RiodeUser CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? DeletedByUserId { get; set; }
        public virtual RiodeUser DeletedByUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
