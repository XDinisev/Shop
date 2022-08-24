using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NHibernate;
using Shop.Business.Services;
using Shop.NHibernate;
using Shop.NHibernate.Repositories;
//using NHibernate.Extensions.Logging;

namespace Shop.BlazorWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //var loggerFactory = builder.Services.BuildServiceProvider().GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>();
            //loggerFactory.UseAsNHibernateLoggerProvider();
            var session = NHibernateHelper.SessionFactory;
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            Business.ServiceProvider.GetCategoryService();
            builder.Services.AddSingleton(session);
            //builder.Services.AddSingleton(NHibernateHelper.SessionFactory);
            builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();

            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();

            builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
            builder.Services.AddSingleton<IOrderService, OrderService>();

            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IProductService, ProductService>();

            //builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));

            await builder.Build().RunAsync();
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {


   //         builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest().PropertiesAutowired();
   //         var sefact = NHibernateHelper.SessionFactory;
			//Console.WriteLine(sefact);
			//builder.RegisterInstance(sefact).As<ISessionFactory>().SingleInstance();

			//// scans for repositories in the repositories assembly
			//builder.RegisterAssemblyTypes(typeof(IRepository<Object>).Assembly)
			//    .Where(x => x.Namespace.EndsWith(".Repositories"))
			//    .AsImplementedInterfaces()
			//    .InstancePerRequest();

			//builder.RegisterAssemblyTypes(typeof(ICategoryService).Assembly)
			//    .Where(x => x.Namespace.EndsWith(".Services"))
			//    .AsImplementedInterfaces()
			//    .InstancePerRequest();
		}
    }
}
