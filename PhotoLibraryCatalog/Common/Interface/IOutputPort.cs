using System;
using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Common.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Common.Interface
{
    interface IOutputPort
    {
        void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v);
        void HandleProgress(ProgressReport progressReport);
        void HandleMessage(string message);
        void HandleException(Exception exception, string message);
    }
}