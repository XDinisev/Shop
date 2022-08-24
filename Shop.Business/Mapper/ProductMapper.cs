using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Mapper
{
    public static class ProductMapper
    {
        public static ProductViewModel MapToViewModel(this Product product)
        {
            //if (product != null)
            return new ProductViewModel()
            {
                Id = product.Id,
                Title = product.Title,
                Status = product.Status,
                Description = product.Description,
                CategoryId = product.Category.Id,
                Category = product.Category?.MapToViewModel()
            };
            //else
            //    return null;
        }
    }
}
