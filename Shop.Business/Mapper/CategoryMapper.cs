using System.Collections.Generic;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryViewModel MapToViewModel(this Category category)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Status = category.Status,
                IsDefault = category.IsDefault,
            };
        }
    }
}
