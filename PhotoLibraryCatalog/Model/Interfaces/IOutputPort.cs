using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Interfaces
{
    interface IOutputPort
    {
        void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v);
        void HandleProgress(ProgressReport progressReport);
        void HandleMessage(string message);
    }
}