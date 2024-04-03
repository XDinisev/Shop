using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Mapper
{
    public static class ProductMapper
    {
        public static ProductViewModel MapToViewModel(this Product product)
        {

            IFormFile file = null;
            if (product.Picture!=null)
            {
                byte[] bytes = Convert.FromBase64String(product.Picture);
                MemoryStream stream = new MemoryStream(bytes);
                file = new FormFile(stream, 0, bytes.Length, product.Title, product.Title);
            }
            //if (product != null)
            return new ProductViewModel()
            {
                Id = product.Id,
                Title = product.Title,
                Status = product.Status,
                Description = product.Description,
                CategoryId = product.Category.Id,
                Category = product.Category?.MapToViewModel(),
                Picture = file,
                PictureBase64 = product.Picture
            };
            //else
            //    return null;
        }
    }
}
