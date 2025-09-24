using SmsVideoDemo.Configurations;
using SmsVideoDemo.Interfaces;
using SmsVideoDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<LabsMobileOptions>(
builder.Configuration.GetSection("ApiKeys:Labsmobile"));

builder.Services.AddHttpClient("LabsMobile");
builder.Services.AddScoped<ISmsService, SmsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
