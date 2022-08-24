using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Customer : IEntity
    {
        protected Customer()
        {

        }
        public Customer(int id, string firstName, string lastName, string username, string password, string email, UserType userType, IList<Order> orders)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            UserType = userType;
            Orders = orders;
        }

        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual IList<Order> Orders { get; set; } = new List<Order>();
    }

    public enum UserType
    {
        @internal,
        external
    }
}
