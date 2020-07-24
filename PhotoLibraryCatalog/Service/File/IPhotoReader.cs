﻿using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Service.File
{
    interface IPhotoReader
    {
        (Model.Photo, ImportError) ReadPhoto(string filePath, bool readMetaData, bool readDateTimeFromFile);
        void ReadMetaData(Model.Photo photo);
    }
}