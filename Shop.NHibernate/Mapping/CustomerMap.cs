using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.NHibernate.Mapping
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Username).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.UserType).Not.Nullable();
            HasMany(x => x.Orders)
                .KeyColumn("CustomerId")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
