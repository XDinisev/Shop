using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Order : IEntity
    {
        protected Order()
        {

        }
        public Order(int id, int quantity, DateTime date, OrderStatus status, Customer customer, Product product)
        {
            Id = id;
            Quantity = quantity;
            Date = date;
            Status = status;
            Customer = customer;
            Product = product;
        }

        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
    public enum OrderStatus
    {
        Complete,
        NotComplete
    }
}
