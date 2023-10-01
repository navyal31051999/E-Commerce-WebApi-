using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebapiDemo.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddswaggerDocumentions(this IServiceCollection Services)
        {
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SkiNet API", Version = "v1" });
            });
            return Services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}