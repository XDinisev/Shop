using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shop.Business.Models;
using Shop.Business.Services;
using Shop.Business;
using Shop.Domain.Entities;

namespace Shop.BlazorWeb.Pages
{
    public partial class CategoryPageasdasd
    {
        protected ICategoryService _categoryService { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        private void InitializeCategories()
        {
            //Categories = categoryService.GetAll();
            Categories = new List<Category>()
            {
                new Category(1, "Kategorija 1", CategoryStatus.Disabled, IsDefault.No, new List<Product>()),
                new Category(2, "Kategorija 2", CategoryStatus.Enabled, IsDefault.Yes, new List<Product>()),
                new Category(3, "Kategorija 3", CategoryStatus.Disabled, IsDefault.No, new List<Product>()),
                new Category(4, "Kategorija 4", CategoryStatus.Enabled, IsDefault.No, new List<Product>())
            };
        }
    }
}
