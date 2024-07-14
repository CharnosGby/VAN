using Microsoft.Extensions.DependencyInjection;
using VAN.WebCore.WebService;

namespace VAN.WebCore.Init
{
    public class ServiceInit
    {
        public void AddService(IServiceCollection services)
        {
            services.AddScoped<ITestService, TestService>();
        }
    }
}
