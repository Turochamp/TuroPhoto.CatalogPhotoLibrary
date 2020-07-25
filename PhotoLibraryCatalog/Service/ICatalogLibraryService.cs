using System;
using TuroPhoto.PhotoLibraryCatalog.Common.Interface;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog.Service
{
    interface ICatalogLibraryService : IDisposable
    {
        void CreateLibraryCatalog(string computerName, string directoryPath, IOutputPort outputPort);
    }
}