using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TuroPhoto.PhotoLibraryCatalog.Model.Dto;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    class Photo : IEquatable<Photo>
    {
        public static Photo Create(string filePath, PhotoPathType pathType = PhotoPathType.Source)
        {
            if (pathType != PhotoPathType.Source)
            {
                throw new NotImplementedException("Currently only supports PhotoPathType.Source.");
            }

            return new Photo(filePath);
        }

        public Photo()
        {
        }

        public Photo(string filePath, PhotoPathType pathType = PhotoPathType.Source)
        {
            SourceFileName = Path.GetFileName(filePath);
            SourceDirectory = Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// Hash key based upon source file path and file datetime. Hash is deterministic, but not
        /// guaranteed to be unique.
        /// </summary>
        public string Key
        {
            get => $"{DateTimeFromFile.Ticks};{SourceFileName}";
        }

        public string SourceFilePath => Path.Combine(SourceDirectory, SourceFileName);

        public string SourceFileName { get; private set; }

        public string SourceDirectory { get; private set; }

        public string RelativeSourceFilePath { get; set; }

        public ImageMetaData ImageMetaData { get; set; }

        private DateTime? _dateTimeFromFile;
        public DateTime DateTimeFromFile
        {
            get
            {
                if (!_dateTimeFromFile.HasValue)
                {
                    ReadDateTimeFromFile();
                }

                Debug.Assert(_dateTimeFromFile != null, nameof(_dateTimeFromFile) + " != null");
                return _dateTimeFromFile.Value;
            }
        }

        public int Id { get; }

        public void ReadDateTimeFromFile()
        {
            _dateTimeFromFile = File.GetCreationTime(SourceFilePath);
        }

        public override string ToString()
        {
            return SourceFileName;
        }

        public static List<string> GetSourceDirectories(IEnumerable<Photo> photos)
        {
            return photos.Select(p => p.SourceDirectory).Distinct().ToList();
        }

        internal static DateTime? GetDateTimeFromFileName(string fileName)
        {
            if (Path.HasExtension(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName);
            }

            var regex = new Regex(@"[0-9]{8}_[0-9]{6}");
            var match = regex.Match(fileName);
            if (!match.Success)
            {
                return null;
            }

            var parseSuccess = DateTime.TryParseExact(
                match.Value, "yyyyMMdd_HHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateTimeValue);

            return parseSuccess ? (DateTime?)dateTimeValue : null;
        }

        public bool Equals(Photo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Photo)obj);
        }

        public override int GetHashCode()
        {
            return Key != null ? Key.GetHashCode() : 0;
        }
    }
}
