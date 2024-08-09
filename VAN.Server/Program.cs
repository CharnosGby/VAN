using AOPInit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Utils;
using VAN.SQLServerCore.SQLServer;
using VAN.WebCore.Init;
using VAN.WebCore.WebService;
using VAN.WebCore.WebService.WebServiceImpl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// ³õÊ¼»¯
# region SqlServer ×¢²á
builder.Services.AddDbContext<DbContext, SQLServerInit>(builderOptions =>
{
    builderOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
# endregion

# region WebService ×¢²á
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{

    # region Service ×¢²á
    builder.RegisterType<TestServiceImpl>().As<ITestService>()
    .EnableInterfaceInterceptors();
    # endregion

    # region AOP ×¢²á
    builder.RegisterType<CustomLogInterceptor>();
    # endregion

    # region Utils ×¢²á
    builder.RegisterType<MyUtils>();
    # endregion
});
# endregion

# region Swagger ×¢²á
new SwaggerInit().AddSwaggerExt(builder.Services);
# endregion

# region WebCors ×¢²á
new WebCorsInit().AddCors(builder.Services);
# endregion

# region Automapper ×¢²á
builder.Services.AddAutoMapper(typeof(AutoMapInit));
# endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    new SwaggerInit().UseSwaggerExt(app);
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
