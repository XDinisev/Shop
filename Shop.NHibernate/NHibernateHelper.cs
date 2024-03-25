using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Shop.NHibernate.Mapping;

namespace Shop.NHibernate.Repositories
{
    public class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;
        
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }
        private static void InitializeSessionFactory()
        {
			try
			{
                _sessionFactory = Fluently.Configure()
                 .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True"))
                 .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<NHibernateHelper>())
                 //.ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                 //.Execute(true, true))
                 .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Execute(true, true, false))
                 .BuildSessionFactory();
            }
            catch(Exception e)
			{
                Console.WriteLine("NASHA: " + e.InnerException);
			}
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
