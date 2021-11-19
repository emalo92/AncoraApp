using Infrastructure.Dal;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace Contabilita
{
    public static class Program
    {
        public static IConfiguration Configuration { get; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var host = Host.CreateDefaultBuilder()
             .ConfigureAppConfiguration((context, builder) =>
             {
                 // Add other configuration files...
                 builder.AddJsonFile("appsettings.json", optional: true);
             })
             .ConfigureServices((context, services) =>
             {
                 ConfigureServices(context.Configuration, services);
             })
             .ConfigureLogging(logging =>
             {
                 // Add other loggers...
             })
             .Build();

            var services = host.Services;
            var mainForm = services.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // ...
            services.AddSingleton<MainForm>();
            services.AddScoped<IContabilitaDal, ContabilitaDal>();
            string connectionString = Configuration.GetConnectionString("ItalTechContext");
            var dalStrartup = new Infrastructure.Startup(Configuration);
            dalStrartup.ConfigureServices(services, connectionString);
            services.AddDbContext<ContabilitaDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}