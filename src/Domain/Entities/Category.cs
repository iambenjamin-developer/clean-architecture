using Domain.Common;

namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
