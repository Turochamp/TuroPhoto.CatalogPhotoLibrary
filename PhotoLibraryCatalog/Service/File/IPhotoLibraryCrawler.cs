using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model.Interfaces;

namespace TuroPhoto.PhotoLibraryCatalog.Service.File
{
    internal interface IPhotoLibraryCrawler
    {
        (PhotoList, List<ImportError>) FindPhotos(string sourcePath, IOutputPort outputPort, bool readImageMetaData);
    }
}