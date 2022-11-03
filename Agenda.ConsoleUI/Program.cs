using Agenda.Application.Interfaces;
using Agenda.Application.Mappers;
using Agenda.Application.Services;
using Agenda.Application.Utils;
using Agenda.ConsoleUI.Navigation;
using Agenda.ConsoleUI.Views;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Context;
using Agenda.Infrastructure.Repositories;
using Agenda.Infrastructure.Storage;
using Agenda.Infrastructure.UnityOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Agenda.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(services);
            }).ConfigureLogging((context, logging) => {
                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection("Logging");
                logging.AddConfiguration(config);
                logging.AddConsole();
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                logging.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
                
            });

        private static void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JsonStorageOptions>(config =>
            {
                config.FilePath = "\\default_store.json";
            });

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(@"Data Source=DESKTOP-ONMGS4I;Database=AgendaDesafio4;Integrated Security=true;pooling=true")
                ,
                ServiceLifetime.Singleton
            );

            services.AddTransient<Menu>();
            services.AddTransient<AgendaFunctions>();
            services.AddTransient<Querys>();

            services.AddAutoMapper(typeof(AgendaProfile));

            services.AddTransient<IAgendaService, AgendaService>();

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IInteractionRepository, InteractionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IContactJsonStorage, ContactJsonStorage>();

            services.AddTransient<PhoneValidator>();

            services.AddHostedService<ConsoleHostedService>();
        }
    }
}
