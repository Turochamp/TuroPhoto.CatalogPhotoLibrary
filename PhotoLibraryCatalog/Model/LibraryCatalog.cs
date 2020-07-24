using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    internal class LibraryCatalog
    {
        private List<Photo> _photos;
        public IReadOnlyCollection<Photo> Photos => _photos;

        private List<LibraryCatalogDirectory> _directories;
        public IReadOnlyCollection<LibraryCatalogDirectory> Directories => _directories;

        public int Id { get; }
        public string ComputerName { get; }
        public string DirectoryPath { get; }
        public DateTime Created { get; }

        public LibraryCatalog(string computerName, string directoryPath, List<Photo> photos) : this()
        {
            ComputerName = computerName;
            DirectoryPath = directoryPath;
            _photos = photos;
        }

        public LibraryCatalog()
        {
            Created = DateTime.Now;
            _directories = new List<LibraryCatalogDirectory>();
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
                var directory = new LibraryCatalogDirectory(group.Key);
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