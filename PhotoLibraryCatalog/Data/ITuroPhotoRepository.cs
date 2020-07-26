using System;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Data
{
    public interface ITuroPhotoRepository
    {
        void Insert(LibraryCatalog catalog);
        void Save();
        Exception TrySave();
        void Dispose();
    }
}