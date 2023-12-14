using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Domain.Settings;
using EmporiumApp.Infraestructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfraestructure(this IServiceCollection service, IConfiguration config)
        {
            service.Configure<MailSettings>(config.GetSection("MailSettings"));
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<IUploadFileService, UploadFileService>();
        }
    }
}
