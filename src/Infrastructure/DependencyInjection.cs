using Application.Common.Interfaces;
using Application.Workshops.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDatabaseDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                      options.UseSqlServer(
                          configuration.GetConnectionString("DefaultConnection"),
                          b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            services.AddHttpClient<IHttpClientService, HttpClientService>((serviceProvider, client) =>
            {
                string url = configuration["WorkshopApi:Url"];
                string username = configuration["WorkshopApi:Username"];
                string password = configuration["WorkshopApi:Password"];

                client.BaseAddress = new Uri(url);
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{username}:{password}")
                );
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            });

            services.AddMemoryCache();

            services.AddScoped<IWorkshopService, WorkshopService>();

            return services;
        }
    }
}
