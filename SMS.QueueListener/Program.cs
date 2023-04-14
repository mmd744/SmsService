using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SMS.QueueListener.Repositories.Abstract;
using SMS.QueueListener.Repositories.Implementation;
using SMS.Shared;
using SMS.Shared.Contexts;
using System.Reflection;

namespace SMS.QueueListener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string dbConnectionString = configuration[AppSettingsSections.DbConnectionString];
            string rabbitMqHost = configuration[AppSettingsSections.RabbitMQHost];
            string rabbitMqVirtualHost = configuration[AppSettingsSections.RabbitMQVirtualHost];

            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<SmsDbContext>(options => options.UseSqlServer(dbConnectionString));
                services.AddScoped<ISmsRepository, SmsRepository>();
                services.AddMassTransit(busConfigurator =>
                {
                    busConfigurator.AddConsumer<SmsSentConsumer>();
                    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                    {
                        busFactoryConfigurator.Host(rabbitMqHost, rabbitMqVirtualHost, h => { });
                        busFactoryConfigurator.ConfigureEndpoints(context);
                    });
                });
            });

            builder.Build().Run();

            Console.ReadKey();
        }
    }
}