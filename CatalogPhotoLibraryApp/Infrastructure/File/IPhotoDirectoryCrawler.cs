using System.Collections.Generic;
using TuroPhoto.CatalogPhotoLibraryApp.Model;
using TuroPhoto.CatalogPhotoLibraryApp.Model.Dto;
using TuroPhoto.CatalogPhotoLibraryApp.Model.Interfaces;

namespace TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.File
{
    internal interface IPhotoDirectoryCrawler
    {
        (PhotoList, List<ImportError>) FindPhotos(string sourcePath, IOutputPort outputPort, bool readImageMetaData);
    }
}