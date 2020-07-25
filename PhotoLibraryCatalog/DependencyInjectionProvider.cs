using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Reflection;
using TuroPhoto.PhotoLibraryCatalog.Controller;
using TuroPhoto.PhotoLibraryCatalog.Data;
using TuroPhoto.PhotoLibraryCatalog.Model.Service;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog
{
    // TODO: Refactor to separate from Program. Currently unnessecary coupling regarding dispose. Should use Configuration directly.
    class DependencyInjectionProvider
    {
        public static IServiceProvider BuildDi(IConfiguration config)
        {
            _serviceProvider = new ServiceCollection()
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

            return _serviceProvider;
        }

        private static IServiceProvider _serviceProvider;
        public static T GetRequiredService<T>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("DI Service provider has not been build.");
            }

            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
