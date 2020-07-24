using System;
using TuroPhoto.PhotoLibraryCatalog.Model.Interfaces;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog.Service
{
    interface ICatalogLibraryService : IDisposable
    {
        void CreateLibraryCatalog(string computerName, string directoryPath, IOutputPort outputPort);
    }
}