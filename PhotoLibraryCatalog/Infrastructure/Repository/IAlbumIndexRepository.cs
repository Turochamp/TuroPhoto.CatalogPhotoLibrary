using System;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Infrastructure.Repository
{
    interface IAlbumIndexRepository
    {
        void Insert(AlbumIndex albumIndex);
        void Save();
        Exception TrySave();
    }
}