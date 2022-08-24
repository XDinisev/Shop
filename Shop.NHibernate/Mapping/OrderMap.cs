using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.NHibernate.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id);
            Map(x => x.Quantity).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.Status).Not.Nullable();
            References(x => x.Customer)
                .Column("CustomerId");
            References(x => x.Product)
                .Column("ProductId");
        }
    }
}
