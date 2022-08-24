using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.NHibernate.Mapping
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Status).Not.Nullable();
            Map(x => x.IsDefault).Not.Nullable();
            HasMany(x => x.Products)
                .KeyColumn("CategoryId")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }

    }
}
