using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.NHibernate.Repositories;

namespace Shop.NHibernate.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
