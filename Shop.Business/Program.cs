using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Shop.NHibernate;
using Shop.Domain.Entities;
using Shop.Business.Services;
using Shop.Business.Models;
using Shop.NHibernate.Repositories;

namespace Shop.Business
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Data Source=PC;Initial Catalog=New;Integrated Security=True";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });

            //var shit = typeof(CategoryMap);
            //var ss = Assembly.GetExecutingAssembly().GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Shop.NHibernate");
            ////cfg.AddAssembly(Assembly.GetAssembly(typeof(CategoryMap)));
            //cfg.AddAssembly(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Shop.NHibernate"));


            var sefact = cfg.BuildSessionFactory();

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {

                }
            }

        }
    }
}
