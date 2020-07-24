using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;
using TuroPhoto.PhotoLibraryCatalog.Model.Interfaces;

namespace TuroPhoto.PhotoLibraryCatalog.View
{
    class IndexAlbumView : IOutputPort
    {
        public IndexAlbumView(ILogger<IndexAlbumView> logger)
        {
            Logger = logger;
        }

        private string _handleProgressOperationDescription = null;

        public ILogger<IndexAlbumView> Logger { get; }

        public void HandleProgress(ProgressReport report)
        {
            if (report.OperationDescription == _handleProgressOperationDescription)
            {
                return;
            }

            _handleProgressOperationDescription = report.OperationDescription;
            Logger.LogDebug($"{_handleProgressOperationDescription.Trim()}");
        }

        public void TrackHandleTelemetry(PhotoList list, List<ImportError> errorList, string v)
        {
        }

        public void HandleMessage(string message)
        {
            Logger.LogInformation(message);
        }

        internal void HandleException(Exception exception, string message)
        {
            Logger.LogError(exception, message);
        }
    }
}