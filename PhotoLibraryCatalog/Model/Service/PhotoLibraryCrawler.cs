using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Directory = System.IO.Directory;
using System.Text.RegularExpressions;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    class PhotoLibraryCrawler : IPhotoLibraryCrawler
    {
        private PhotoList _list;
        private IOutputPort _outputPort;
        private List<ImportError> _errorList;

        public PhotoLibraryCrawler()
        {
            PhotoReader = new PhotoReader();
        }

        public (PhotoList, List<ImportError>) FindPhotos(
            string sourcePath, IOutputPort outputPort, bool readImageMetaData)
        {
            _outputPort = outputPort;
            _list = new PhotoList();
            _errorList = new List<ImportError>();

            FindPhotosInDirectory(sourcePath);

            _outputPort?.TrackHandleTelemetry(_list, _errorList,
                // ReSharper disable once UseStringInterpolation
                string.Format("ImportPhotos: Found photos (Source: {0}, ExistingPhotos: {1}, Errors {2})",
                    sourcePath, _list.Count, _errorList.Count));

            if (readImageMetaData)
            {
                ReadImageMetaData();
            }

            return (_list, _errorList);
        }

        public IPhotoReader PhotoReader { get; }

        private void FindPhotosInDirectory(string directoryPath)
        {
            var subDirectories = Directory.GetDirectories(directoryPath);

            // Recursively process each directory
            foreach (var subDirectory in subDirectories)
            {
                // TODO: Add support for configurable ignore files/dirs
                if (Path.GetFileName(subDirectory).Equals("@eaDir", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                FindPhotosInDirectory(subDirectory);
            }

            // Get each file path in directory
            var pathsOfDirectoryFiles = new List<string>();

            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                pathsOfDirectoryFiles.Add(filePath);
            }

            // Filter to only include photos (and videos)
            var photoRegex = new Regex(@".*\.(gif|jpe?g|bmp|png|vmv)$", RegexOptions.IgnoreCase);
            var videoRegex = new Regex(@".*\.(mov|mpe?g|mp4|avi)$", RegexOptions.IgnoreCase);
            var pathsOfDirectoryPhotos = pathsOfDirectoryFiles
                .Where(p => photoRegex.IsMatch(p) || videoRegex.IsMatch(p))
                .ToList();

            // Process each photo in directory
            for (var index = 0; index < pathsOfDirectoryPhotos.Count; index++)
            {
                var filePath = pathsOfDirectoryPhotos[index];
                var (photo, error) = PhotoReader.ReadPhoto(filePath, false, true);
                if (photo != null)
                {
                    _list.Add(photo);
                }
                if (error != null)
                {
                    _errorList.Add(error);
                }

                var progressReport =
                    new ProgressReport(
                        $"Find photos {directoryPath}",
                        index,
                        pathsOfDirectoryPhotos.Count);
                _outputPort?.HandleProgress(progressReport);
            }
        }

        private void ReadImageMetaData()
        {
            foreach (var photo in _list)
            {
                try
                {
                    PhotoReader.ReadMetaData(photo);
                }
                catch (Exception ex)
                {
                    _errorList.Add(new ImportError(photo.SourceFilePath, ImportErrorType.Exception, ex));
                }

                var progressReport =
                    new ProgressReport(
                        $"Reading MetaData {photo.SourceDirectory}");
                _outputPort?.HandleProgress(progressReport);
            }
        }
    }
}
