using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();

            return services;
        }
    }
}
