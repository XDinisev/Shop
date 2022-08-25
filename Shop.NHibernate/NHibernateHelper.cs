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

        //public static ISessionFactory CreateSessionFactory()
        //{
        //    ISessionFactory sessionFactory = Fluently.Configure()
        //        .Database(MsSqlConfiguration.MsSql2012
        //            //.ConnectionString(@"Data Source=DEV-LT1;Initial Catalog=StudyBuddiesDb;User ID=wwwuser;Password=P@ssw0rd;Pooling=False")
        //            .ConnectionString(@"Data Source=PC;Initial Catalog=Shop;Integrated Security=SSPI")
        //            .ShowSql())
        //        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())

        //        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
        //        .BuildSessionFactory();

        //    return sessionFactory;
        //}

        private static void InitializeSessionFactory()
        {
			try
			{
                _sessionFactory = Fluently.Configure()
                 .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString("Data Source=PC;Initial Catalog=Shop;Integrated Security=True"))
                 .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<NHibernateHelper>())
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                    .Execute(true, true))
                 //.ExposeConfiguration(cfg => new SchemaExport(cfg)
                 //   .Execute(true, true, false))
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
