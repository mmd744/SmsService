using MassTransit;
using Microsoft.EntityFrameworkCore;
using SMS.Shared;
using SMS.Shared.Contexts;
using SMS.WebService.AutoMapper;
using SMS.WebService.Repositories.Abstract;
using SMS.WebService.Repositories.Implementation;
using SMS.WebService.Services.Abstract;
using SMS.WebService.Services.Implementation;

namespace SMS.WebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Services
            builder.Services.AddScoped<ISmsService, SmsService>();

            // Repositories
            builder.Services.AddScoped<ISmsRepository, SmsRepository>();

            // Data
            builder.Services.AddDbContext<SmsDbContext>(options => 
                options.UseSqlServer(builder.Configuration[AppSettingsSections.DbConnectionString]));

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(SmsMappingProfile));

            // Bus
            builder.Services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    busFactoryConfigurator.Host(
                        builder.Configuration[AppSettingsSections.RabbitMQHost], hostConfigurator => { });
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) { }

            app.UseSwagger();
            app.UseSwaggerUI();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SmsDbContext>();
                context.Database.Migrate();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}