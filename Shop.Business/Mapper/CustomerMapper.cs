using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Business.Models;
using Shop.Domain.Entities;

namespace Shop.Business.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerViewModel MapToViewModel(this Customer customer)
        {
            if (customer != null)
                return new CustomerViewModel()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Username = customer.Username,
                    Password = customer.Password,
                    UserType = customer.UserType
                };
            else
                return null;
        }
    }
}
