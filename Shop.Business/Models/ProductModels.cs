using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Models
{
    [Validator(typeof(ProductViewModelValidator))]
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public ProductStatus Status { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>();
    }

    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Title shouldn't be empty!")
                .Length(5)
                    .WithMessage("Title should be 5 or more characters!");

        }
    }
}
