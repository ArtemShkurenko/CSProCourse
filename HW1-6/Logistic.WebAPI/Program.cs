using AutoMapper;
using Logistic.Core.Services;
using Logistic.DAL.DataBase;
using Logistic.Models;
using Logistic.WebAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddSingleton<IRepository<Vehicle>, InMemoryRepository<Vehicle>>();
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<VehicleModel, Vehicle>();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddSingleton<IRepository<Warehouse>, InMemoryRepository<Warehouse>>();
var configWh = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<WarehouseModel, Warehouse>();
});
IMapper mapperWh = config.CreateMapper();
builder.Services.AddSingleton(mapperWh);

builder.Services.AddScoped<ReportService<Vehicle>>();
builder.Services.AddSingleton<IReportRepository<Vehicle>, XmlFileRepository<Vehicle>>();
builder.Services.AddSingleton<IReportRepository<Vehicle>, JsonFileRepository<Vehicle>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
