using System;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    interface ICatalogLibraryService : IDisposable
    {
        void CreateLibraryCatalog(string computerName, string directoryPath, IOutputPort outputPort);
    }
}