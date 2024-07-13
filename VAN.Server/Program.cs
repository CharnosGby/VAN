using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using VAN.Server.Service;
using VAN.WebCore.Swagger;
using VueASPNet.Server.Db;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer("Data Source=localhost;Database=workdatabase;User Id=root;Password=123456;Encrypt=False;"));
builder.Services.AddScoped<ITestService, TestService>();

new SwaggerInit().AddSwaggerExt(builder.Services);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // ‘ –Ì–Ø¥¯∆æ÷§
        });
});

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