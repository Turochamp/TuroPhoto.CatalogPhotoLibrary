using System;
using System.Collections.Generic;
using System.Text;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;

namespace PhotoLibraryCatalog.Test
{
    class TestOutputPort : IOutputPort
    {
        public void HandleException(Exception exception, string message)
        {
        }

        public void HandleMessage(string message)
        {
        }

        public void HandleProgress(ProgressReport progressReport)
        {
        }

        public void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v)
        {
        }
    }
}
