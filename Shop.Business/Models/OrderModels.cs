using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Shop.Domain.Entities;

namespace Shop.Business.Models
{
    [Validator(typeof(OrderViewModelValidator))]
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        [Required(ErrorMessage = "Product is required")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
    }

    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {

        }
    }
}
