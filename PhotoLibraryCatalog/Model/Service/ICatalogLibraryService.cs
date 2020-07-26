using System;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    public interface ICatalogLibraryService : IDisposable
    {
        LibraryCatalog CreateLibraryCatalog(string computerName, string directoryPath, IOutputPort outputPort);
        void SaveLibraryCatalog(LibraryCatalog catalog, IOutputPort outputPort);
    }
}