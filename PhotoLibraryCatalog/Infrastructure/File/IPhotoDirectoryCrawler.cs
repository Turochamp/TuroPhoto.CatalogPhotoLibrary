using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model.Interfaces;

namespace TuroPhoto.PhotoLibraryCatalog.Infrastructure.File
{
    internal interface IPhotoDirectoryCrawler
    {
        (PhotoList, List<ImportError>) FindPhotos(string sourcePath, IOutputPort outputPort, bool readImageMetaData);
    }
}