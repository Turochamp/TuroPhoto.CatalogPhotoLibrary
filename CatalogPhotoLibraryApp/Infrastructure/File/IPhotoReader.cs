using TuroPhoto.CatalogPhotoLibraryApp.Model.Dto;

namespace TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.File
{
    interface IPhotoReader
    {
        (Model.Photo, ImportError) ReadPhoto(string filePath, bool readMetaData, bool readDateTimeFromFile);
        void ReadMetaData(Model.Photo photo);
    }
}