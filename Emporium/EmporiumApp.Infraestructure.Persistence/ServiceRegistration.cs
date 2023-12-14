using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Infraestructure.Persistence.Context;
using EmporiumApp.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                service.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #region 'repositories'

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IImprovementRepository, ImprovementRepository>();
            service.AddTransient<ISaleTypeRepository, SaleTypeRepository>();
            service.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductCarritoRepository, ProductCarritoRepository>();
            service.AddTransient<IProductImprovementRepository, ProductImprovementRepository>();

            #endregion
        }
    }
}
