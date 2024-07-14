using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAN.WebCore.Init
{
    public class WebCorsInit
    {
        public void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5173")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials(); // 允许携带凭证
                    });
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5174")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); // 允许携带凭证
                    });
            });
        }
    }
}
