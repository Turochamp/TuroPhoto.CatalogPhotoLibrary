using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    public interface IPhotoReader
    {
        (Photo, ImportError) ReadPhoto(string filePath, bool readMetaData, bool readDateTimeFromFile);
        void ReadMetaData(Photo photo);
    }
}