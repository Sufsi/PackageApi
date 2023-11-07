using Microsoft.OpenApi.Models;
using PackageApi.Infrastructure.Factory;
using PackageApi.Infrastructure;
using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Package Api", Version = "1.0" });
});

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();

var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

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
