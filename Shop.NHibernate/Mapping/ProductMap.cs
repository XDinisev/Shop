using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.NHibernate.Mapping
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Status).Not.Nullable().Not.Nullable();
            Map(x => x.Description);
            References(x => x.Category)
                .Column("CategoryId");
            HasMany(x => x.Orders)
                .KeyColumn("ProductId")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
