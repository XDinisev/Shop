using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Mapper
{
    public static class OrderMapper
    {
        public static OrderViewModel MapToViewModel(this Order order)
        {
            return new OrderViewModel()
            {
                Id = order.Id,
                Quantity = order.Quantity,
                Date = order.Date,
                Status = order.Status,
                CustomerId = order.Customer.Id,
                Customer = order.Customer?.MapToViewModel(),
                ProductId = order.Product.Id,
                Product = order.Product?.MapToViewModel()
            };
        }
    }
}
