using Infrastructure.Dal;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace Contabilita
{
    public class Program
    {
#pragma warning disable CS8618 // Il campo non nullable deve contenere un valore non Null all'uscita dal costruttore. Provare a dichiararlo come nullable.
        public static IConfiguration Configuration { get; set; }
#pragma warning restore CS8618 // Il campo non nullable deve contenere un valore non Null all'uscita dal costruttore. Provare a dichiararlo come nullable.

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
            ConfigureServices(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var form1 = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(form1);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ...
            services.AddSingleton<MainForm>();
            services.AddScoped<IContabilitaDal, ContabilitaDal>();
            var connectionString = Configuration.GetSection("ConnectionString:Contabilita").Value;
            var dalStrartup = new Infrastructure.Startup(Configuration);
            dalStrartup.ConfigureServices(services, connectionString);
            services.AddDbContext<ContabilitaDbContext>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(Program));
            services.AddLogging(option =>
            {
                option.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                option.AddNLog("NLog.config");
            });
        }
    }
}