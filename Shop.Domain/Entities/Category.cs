using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Category : IEntity
    {
        protected Category()
        {            
        }
        public Category(string title, CategoryStatus status, IsDefault isDefault)
        {
            Title = title;
            Status = status;
            IsDefault = isDefault;
        }
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual CategoryStatus Status { get; set; }
        public virtual IsDefault IsDefault { get; set; }
        public virtual IList<Product> Products { get; set; } = new List<Product>();
    }
    public enum CategoryStatus
    {
        Enabled,
        Disabled
    }
    public enum IsDefault
    {
        Yes,
        No
    }
}
