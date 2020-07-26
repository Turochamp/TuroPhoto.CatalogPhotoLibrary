using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    public interface IPhotoLibraryCrawler
    {
        (PhotoList, List<ImportError>) FindPhotos(string sourcePath, IOutputPort outputPort, bool readImageMetaData);
    }
}