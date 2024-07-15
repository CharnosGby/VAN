using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
namespace AOPInit
{
    public class CustomLogInterceptor : IInterceptor
    {
        private readonly ILogger<CustomLogInterceptor> _Logger;

        public CustomLogInterceptor(ILogger<CustomLogInterceptor> logger) 
        {
            _Logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _Logger.LogInformation($"{invocation.Method.Name}-执行前");
            invocation.Proceed();
            _Logger.LogInformation($"{invocation.Method.Name}-执行后");
        }
    }
}
