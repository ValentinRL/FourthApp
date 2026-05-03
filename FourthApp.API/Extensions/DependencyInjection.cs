using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? [];

            services.AddControllers();
            services.AddOpenApi();

            services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
