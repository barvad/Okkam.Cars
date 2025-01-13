using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Services;
using Okkam.Cars.Ef;
using Okkam.Cars.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Okkam.Cars.WebApi.xml"), true);
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
}); 
builder.Services.AddLogging(builder);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.AddMinio();
builder.AddSettings();
builder.Services.AddHealthChecks();

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<CarsDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();