using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using TuroPhoto.PhotoLibraryCatalog.Controller;
using TuroPhoto.PhotoLibraryCatalog.Infrastructure.File;
using TuroPhoto.PhotoLibraryCatalog.Infrastructure.Repository;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog
{
    // x TODO: Add log rotation
    // TODO: Add input validation
    // TODO: DAL and Service instead of Infrastructure (inspiration from MS book microservice ddd cqrs patterns)? Move to own projects.
    // x TODO: Move to namespace TuroPhoto.PhotoLibraryCatalog
    // TODO: Make Configuration IoC instance. Adding all configuration to it.
    // TODO: Compile to exe. Executable TuroPhoto, which will be later expanded with more commands. 
    // TODO: Create UT and IT. IT should use InMemoryDB.
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

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<IndexAlbumController>();

                    runner.InitConfiguration(directories);

                    logger.SetProperty("Version", runner.Configuration.Version);

                    // TODO: Move directories loop here
                    runner.Run();
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
                .AddTransient<IndexAlbumController>() // Runner is the custom class
                .AddTransient<IndexAlbumView>()
                .AddTransient<IPhotoDirectoryCrawler, PhotoDirectoryCrawler>()
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                })
                // TBD: Why AddLogging needs to be before this line?
                .AddEntityFrameworkSqlServer()
                .AddDbContext<AlbumIndexContext>(options =>
                {
                    options.UseSqlServer(config["ConnectionString"],
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().
                        Assembly.GetName().Name));
                }, ServiceLifetime.Scoped) // Note that Scoped is the default choice
                                           // in AddDbContext. It is shown here only for
                                           // pedagogic purposes.
                .AddTransient<IAlbumIndexRepository, AlbumIndexRepository>()
                .BuildServiceProvider();
        }
    }
}
