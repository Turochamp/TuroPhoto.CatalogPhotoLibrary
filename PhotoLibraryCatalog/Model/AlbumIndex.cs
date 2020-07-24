using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    internal class AlbumIndex
    {
        private List<Photo> _photos;
        public IReadOnlyCollection<Photo> Photos => _photos;

        private List<Directory> _directories;
        public IReadOnlyCollection<Directory> Directories => _directories;

        public int Id { get; }
        public string ComputerName { get; }
        public string DirectoryPath { get; }
        public DateTime Created { get; }

        public AlbumIndex(string computerName, string directoryPath, List<Photo> photos) : this()
        {
            ComputerName = computerName;
            DirectoryPath = directoryPath;
            _photos = photos;
        }

        public AlbumIndex()
        {
            Created = DateTime.Now;
            _directories = new List<Directory>();
            _photos = new List<Photo>();
        }

        public void Init()
        {
            // Photos
            _photos.ForEach(p => p.RelativeSourceFilePath = MakeRelative(p.SourceFilePath, DirectoryPath));

            // Directories
            ILookup<string, Photo> photosByDirectory = Photos.ToLookup(p => p.SourceDirectory);

            foreach (IGrouping<string, Photo> group in photosByDirectory)
            {
                // Create Directory
                // TODO: Use C# 9.0 features
                var directory = new Directory(group.Key);
                directory.RelativePath = MakeRelative(directory.Path, DirectoryPath);

                // Iterate through each value in the grouping
                foreach (Photo photo in group)
                {
                    directory.Add(photo);
                }

                _directories.Add(directory);
            }
        }

        // TODO: Refactor to Model
        public static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return Uri.UnescapeDataString(referenceUri.MakeRelativeUri(fileUri).ToString()).Replace('/', Path.DirectorySeparatorChar);
        }
    }
}