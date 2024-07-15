using Microsoft.Extensions.DependencyInjection;

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
                               .AllowCredentials();
                    });
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5174")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
        }
    }
}
