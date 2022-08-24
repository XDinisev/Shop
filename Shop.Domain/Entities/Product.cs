using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Product : IEntity
    {
        protected Product()
        {

        }
        public Product(int id, string title, ProductStatus status, string description, IList<Order> orders, Category category)
        {
            Id = id;
            Title = title;
            Status = status;
            Description = description;
            Orders = orders;
            Category = category;
        }

        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual ProductStatus Status { get; set; }
        public virtual string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Order> Orders { get; set; } = new List<Order>();
    }
    public enum ProductStatus
    {
        Availiable,
        NotAvailiable
    }
}
