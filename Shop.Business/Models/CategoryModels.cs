using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using Shop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Business.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public CategoryStatus Status { get; set; }
        [Required(ErrorMessage = "Default Status is required")]
        [Display(Name = "Default")]
        public IsDefault IsDefault { get; set; }
    }
    public class CategoryProductsViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        public List<Product> Products { get; set; }
    }
}
