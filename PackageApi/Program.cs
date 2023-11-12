using Microsoft.OpenApi.Models;
using PackageApi.Infrastructure.Factory;
using PackageApi.Infrastructure;
using PackageApi.Infrastructure.Interfaces;
using PackageApi.Infrastructure.Repositories;
using PackageApi.Infrastructure.Database;
using FluentValidation;
using PackageApi.Facades;
using Swashbuckle.AspNetCore.Filters;
using PackageApi.Examples;
using PackageApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Package Api", Version = "1.0" });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.ExampleFilters();
    c.EnableAnnotations();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<PackageExample>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddTransient<IPackageFacade, PackageFacade>();
builder.Services.AddTransient<IDatabase, PackageDatabase>();
builder.Services.AddTransient<IMapperHelper, MapperHelper>();
builder.Services.AddValidatorsFromAssemblyContaining<PackageApi.Validators.PackageValidator>();


// I would really want to use this and stop our packages getting imported based on the validation result directly.
//builder.Services.AddFluentValidationAutoValidation();

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
