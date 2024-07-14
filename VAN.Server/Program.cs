using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using VAN.SQLServerCore.SQLServer;
using VAN.WebCore.Init;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// Init

builder.Services.AddDbContext<SQLServerInit>(options =>
    options.UseSqlServer("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;"));
new ServiceInit().AddService(builder.Services);
new SwaggerInit().AddSwaggerExt(builder.Services);
new WebCorsInit().AddCors(builder.Services);

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