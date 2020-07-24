using System;
using TuroPhoto.CatalogPhotoLibraryApp.Model;

namespace TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.Repository
{
    interface IAlbumIndexRepository
    {
        void Insert(AlbumIndex albumIndex);
        void Save();
        Exception TrySave();
    }
}