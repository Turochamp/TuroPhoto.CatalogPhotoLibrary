using System;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using TuroPhoto.PhotoLibraryCatalog.Model;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Service.File
{
    // TODO: Fix security vulnerability
    class PhotoReader : IPhotoReader
    {
        public (Photo, ImportError) ReadPhoto(string filePath, bool readMetaData, bool readDateTimeFromFile)
        {
            ImportError readError = null;
            Photo photo = null;

            try
            {
                photo = Photo.Create(filePath);

                if (readMetaData)
                {
                    photo.ImageMetaData = ReadPhotoMetaData(filePath);
                }

                if (readDateTimeFromFile)
                {
                    photo.ReadDateTimeFromFile();
                }
            }
            catch (Exception exception)
            {
                readError = new ImportError(filePath, ImportErrorType.Read, exception);
            }

            return (photo, readError);
        }


        public void ReadMetaData(Photo photo)
        {
            if (string.IsNullOrEmpty(photo.SourceFilePath))
            {
                throw new ArgumentException("Photo missing SourceFilePath.", nameof(photo));
            }

            photo.ImageMetaData = ReadPhotoMetaData(photo.SourceFilePath);
        }

        private static ImageMetaData ReadPhotoMetaData(string filePath)
        {
            var imd = new ImageMetaData();

            var directories = ImageMetadataReader.ReadMetadata(filePath);

            var subIfdDirectory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
            imd.DateTime =
                subIfdDirectory != null && subIfdDirectory.TryGetDateTime(ExifDirectoryBase.TagDateTime, out var date)
                    ? date
                    : (DateTime?)null;

            var gpsDirectory = directories.FirstOrDefault(d => d.Name == "GPS");
            imd.GpsTags = gpsDirectory?.Tags
                .Select(x => new Model.Tag(x.Name, x.Description))
                .ToList();

            var gpsLocation = directories
                .OfType<GpsDirectory>()
                .FirstOrDefault();

            var geoLocation = gpsLocation?.GetGeoLocation();
            if (geoLocation != null)
            {
                imd.GeoLocation = new Model.GeoLocation(geoLocation.Latitude, geoLocation.Longitude);
            }

            return imd;
        }
    }
}