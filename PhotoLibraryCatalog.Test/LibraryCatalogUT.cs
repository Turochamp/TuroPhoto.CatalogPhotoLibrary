using System;
using System.Collections.Generic;
using System.Text;
using TuroPhoto.PhotoLibraryCatalog.Model;
using Xunit;

namespace PhotoLibraryCatalog.Test
{
    public class LibraryCatalogUT
    {

        [Theory]
        [InlineData(@"C:\Photos\Jörgens\2006", @"C:\Photos\Jörgens\")]
        [InlineData(@"C:\Photos\Jörgens\2006", @"C:\Photos\Jörgens")]
        [InlineData(@"..\Jörgens\2006", @"..\Jörgens\")]
        [InlineData(@"..\Jörgens\2006", @"..\Jörgens")]
        public void MakeRelative_Valid_Success(string filePath, string directoryPath)
        {
            // Act
            var relativePath = LibraryCatalog.MakeRelative(filePath, directoryPath);

            // Assert
            Assert.Equal(@"2006", relativePath);
        }

        [Theory]
        [InlineData(@"C:\Photos\Jörgens\2006", @"C:\Src\Jörgens\")]
        public void MakeRelative_Invalid_Exception(string filePath, string directoryPath)
        {
            Assert.Throws<ArgumentException>(
                () => LibraryCatalog.MakeRelative(filePath, directoryPath));
        }
    }
}
