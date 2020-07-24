using System;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Infrastructure.Repository
{
    interface ITuroPhotoRepository
    {
        void Insert(LibraryCatalog catalog);
        void Save();
        Exception TrySave();
    }
}