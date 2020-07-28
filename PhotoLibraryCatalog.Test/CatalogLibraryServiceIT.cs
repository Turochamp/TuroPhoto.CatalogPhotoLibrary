using PhotoLibraryCatalog.Test;
using TuroPhoto.PhotoLibraryCatalog.Model.Service;
using Xunit;

namespace PhotoLibraryCatalog.Model.Test
{
    public class CatalogLibraryServiceIT
    {
        [Fact]
        public void CreateLibraryCatalog_TestJorgens_Success()
        {
            // Assemble
            var computerName = "TestComputerName";
            var sourceDirectory = @"..\..\..\Jörgens";
            var crawler = new PhotoLibraryCrawler();
            var service = new CatalogLibraryService(crawler, null);
            var outputPort = new TestOutputPort();

            // Act
            var catalog = service.CreateLibraryCatalog(computerName, sourceDirectory, outputPort);

            // Assert
            Assert.Equal(11, catalog.Directories.Count);
            Assert.Equal(51, catalog.Photos.Count); // 52 files of which one is not a photo
        }
    }
}
