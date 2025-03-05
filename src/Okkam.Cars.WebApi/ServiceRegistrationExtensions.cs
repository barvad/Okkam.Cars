using Minio;
using NLog.Extensions.Logging;
using NLog.Web;
using Okkam.Cars.Core;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Services;
using Okkam.Cars.Repositories;

namespace Okkam.Cars.WebApi;

public static class ServiceRegistrationExtensions
{
    /// <summary>
    /// Добавляет сервисы логирования.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <param name="builder">Билдер.</param>
    public static void AddLogging(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();
        builder.Services.AddTransient<ILoggerFactory>(s => new NLogLoggerFactory(new NLogProviderOptions()));
    }

    /// <summary>
    /// Добавляет настройки.
    /// </summary>
    /// <param name="builder">Билдер.</param>
    public static void AddSettings(this WebApplicationBuilder builder)
    {
        var settings = new Settings();
        settings.FileStorageBucket = builder.Configuration["Minio:Bucket"]!;

        builder.Services.AddSingleton(settings);
    }

    /// <summary>
    /// Добавляет Minio.
    /// </summary>
    /// <param name="builder">Билдер.</param>
    public static void AddMinio(this WebApplicationBuilder builder)
    {
        var user = builder.Configuration["Minio:User"]!;
        var password = builder.Configuration["Minio:Password"]!;
        var host = builder.Configuration["Minio:Host"]!;
        builder.Services.AddScoped<IMinioClient>(s => new MinioClient()
            .WithEndpoint(host)
            .WithCredentials(user, password)
            .Build());
    }

    /// <summary>
    /// Добавляет сервисы.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICarsService, CarsService>();
        serviceCollection.AddScoped<IFiltersService, FiltersService>();
        return serviceCollection;
    }

    /// <summary>
    /// Добавляет репозитории.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICarsRepository, CarsRepository>();
        serviceCollection.AddScoped<IFilesRepository, FilesRepository>();
        serviceCollection.AddScoped<IFiltersRepository, FiltersRepository>();
        return serviceCollection;
    }
}