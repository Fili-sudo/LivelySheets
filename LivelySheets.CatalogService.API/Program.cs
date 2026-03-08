using LivelySheets.CatalogService.API.Extensions;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Application.Utils;
using LivelySheets.CatalogService.Infrastructure;
using LivelySheets.CatalogService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var config = builder.Configuration;

builder.Services
    .ConfigureSwager()
    .ConfigureJWTAuthentication(config.GetSection(AuthExtensions.Auth))
    .RegisterOptions(config)
    .AddEndpoints(Assembly.GetExecutingAssembly())
    .AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(config.GetConnectionString("CString")))
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Helper.AssemblyReference));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();


app.Run();