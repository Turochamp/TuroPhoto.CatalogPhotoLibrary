using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Common.Dto;
using TuroPhoto.PhotoLibraryCatalog.Common.Interface;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Service.File
{
    internal interface IPhotoLibraryCrawler
    {
        (PhotoList, List<ImportError>) FindPhotos(string sourcePath, IOutputPort outputPort, bool readImageMetaData);
    }
}