using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace VAN.WebCore.Swagger
{
    public class SwaggerInit
    {
        public void AddSwaggerExt(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                foreach (var Ver in typeof(DocVersion).GetEnumNames())
                {
                    option.SwaggerDoc(Ver, new OpenApiInfo()
                    {
                        Title = $"预科学生分流志愿填报系统API文档",
                        Version = Ver,
                        Description = $"预科学生分流志愿填报系统API文档，版本：{Ver}"
                    });
                }
                // 支持JWT token授权
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "输入你的token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                //添加安全要求
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
        public void UseSwaggerExt(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var Ver in typeof(DocVersion).GetEnumNames())
                {
                    c.SwaggerEndpoint($"/swagger/{Ver}/swagger.json", $"预科学生分流志愿填报系统API文档-{Ver}");
                }
            });
        }
    }
}
