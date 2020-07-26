using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using TuroPhoto.PhotoLibraryCatalog.Controller;

namespace TuroPhoto.PhotoLibraryCatalog
{
    // x TODO: Add log rotation
    // x TODO: DAL and Service instead of Infrastructure (inspiration from MS book microservice ddd cqrs patterns)? Move to own projects.
    // x TODO: Move to namespace TuroPhoto.PhotoLibraryCatalog
    // TODO: (o) Create UT and IT. IT should use InMemoryDB.
    // TODO: Add GitHub pipeline (ensuring that emails are received properly)
    // TODO: Make Configuration IoC instance. Adding all configuration to it.
    // TODO: Add, optional, AppInsight
    // TODO: Add, optional, File Content comparision (such as CRC)
    // TODO: Add readme.md
    // TODO: Improve console output
    // TODO: Add input validation
    // TODO: Compile to exe. Executable TuroPhoto, which will be later expanded with more commands, such as "Catalog" and "Sync".
    // TODO: Switch to .Net 5.0
    // TODO: Add Architectural fitness functions
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

                var serviceProvider = DependencyInjectionProvider.BuildDi(config); // TBD: Make into constructor
                using (serviceProvider as IDisposable)
                {
                    var controller = serviceProvider.GetRequiredService<LibraryCatalogController>();
                    controller.InitConfiguration(directories);
                    // TBD: controller.DependencyInjectionProvider = serviceProvider

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
    }
}
