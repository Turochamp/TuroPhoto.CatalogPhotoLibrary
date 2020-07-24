using System.Collections.Generic;
using TuroPhoto.CatalogPhotoLibraryApp.Model.Dto;

namespace TuroPhoto.CatalogPhotoLibraryApp.Model.Interfaces
{
    interface IOutputPort
    {
        void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v);
        void HandleProgress(ProgressReport progressReport);
        void HandleMessage(string message);
    }
}