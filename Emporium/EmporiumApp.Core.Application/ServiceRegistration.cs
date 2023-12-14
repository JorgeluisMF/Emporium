using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());


            #region Services

            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IImprovementService, ImprovementService>();
            service.AddTransient<ISaleTypeService, SaleTypeService>();
            service.AddTransient<IProductTypeService, ProductTypeService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IProductCarritoService, ProductCarritoService>();
            service.AddTransient<IProductImprovementService, ProductImprovementService>();

            #endregion
        }
    }
}
