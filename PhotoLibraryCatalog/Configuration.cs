using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace TuroPhoto.PhotoLibraryCatalog
{
    class Configuration
    {
        // TODO: Make version number automatic
        // https://stackoverflow.com/questions/42138418/equivalent-to-assemblyinfo-in-dotnet-core-csproj
        public string Version { get; } = "0.1.0";
        public string[] DirectoryPaths { get; set; } = new[] { @"." };
        public string ComputerName { get; internal set; } = Environment.MachineName;

        public Configuration InitConfiguration()
        {
            var configuration = BuildConfiguration("appsettings.json");

            // TelemetryInstrumentationKey = configuration.GetValue("InstrumentationKey", TelemetryInstrumentationKey);

            return this;
        }

        private static IConfigurationRoot BuildConfiguration(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}