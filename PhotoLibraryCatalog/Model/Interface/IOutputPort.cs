using System;
using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Interface
{
    public interface IOutputPort
    {
        void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v);
        void HandleProgress(ProgressReport progressReport);
        void HandleMessage(string message);
        void HandleException(Exception exception, string message);
    }
}