using FileUploaderAzure.Functions.ServiceContracts;
using FileUploaderAzure.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(FileUploaderAzure.Functions.Startup))]
namespace FileUploaderAzure.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
