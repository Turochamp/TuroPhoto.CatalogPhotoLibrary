using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Reflection;
using TuroPhoto.PhotoLibraryCatalog.Controller;
using TuroPhoto.PhotoLibraryCatalog.Data;
using TuroPhoto.PhotoLibraryCatalog.Service;
using TuroPhoto.PhotoLibraryCatalog.Service.File;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog
{
    // x TODO: Add log rotation
    // x TODO: DAL and Service instead of Infrastructure (inspiration from MS book microservice ddd cqrs patterns)? Move to own projects.
    // x TODO: Move to namespace TuroPhoto.PhotoLibraryCatalog
    // TODO: Create UT and IT. IT should use InMemoryDB.
    // TODO: Make Configuration IoC instance. Adding all configuration to it.
    // TODO: Add, optional, AppInsight
    // TODO: Add input validation
    // TODO: Improve console output
    // TODO: Compile to exe. Executable TuroPhoto, which will be later expanded with more commands, such as "Catalog" and "Sync". 
    // TODO: Switch to .Net 5.0
    // TODO: Move common TuroPhoto code to nuget package(s)
    class Program
    {
        /// <remarks>Uses DragonFruit for typed command arguments.</remarks>
        /// <param name="directories">TBD</param>
        static void Main(string[] directories)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                _serviceProvider = BuildDi(config);
                using (_serviceProvider as IDisposable)
                {
                    var controller = GetRequiredService<LibraryCatalogController>();
                    controller.InitConfiguration(directories);

                    logger.SetProperty("Version", controller.Configuration.Version);

                    controller.Run();
                }
            }
            catch (Exception exception)
            {
                logger.Fatal(exception, "Stopped program because of exception.");
                Environment.ExitCode = -1;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit
                // (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<LibraryCatalogController>() // Runner is the custom class
                .AddTransient<LibraryCatalogView>()
                .AddTransient<ICatalogLibraryService, CatalogLibraryService>()
                .AddTransient<IPhotoLibraryCrawler, PhotoLibraryCrawler>()
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                })
                // TBD: Why AddLogging needs to be before this line?
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TuroPhotoContext>(options =>
                {
                    options.UseSqlServer(config["ConnectionString"],
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().
                        Assembly.GetName().Name));
                }, ServiceLifetime.Transient) // Note that Scoped is the default choice
                                           // in AddDbContext. It is shown here only for
                                           // pedagogic purposes.
                .AddTransient<ITuroPhotoRepository, TuroPhotoRepository>()
                .BuildServiceProvider();
        }

        private static IServiceProvider _serviceProvider;
        public static T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
