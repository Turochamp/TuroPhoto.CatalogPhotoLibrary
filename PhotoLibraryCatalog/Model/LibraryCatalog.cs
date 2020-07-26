using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    #pragma warning disable IDE0044 // Add readonly modifier, due to EF
    public class LibraryCatalog
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

        /// <summary>
        /// Make relative path. Simple implementation 
        /// </summary>
        /// <returns>
        /// Relative path or null if reference path 
        /// </returns>
        public static string MakeRelative(string filePath, string directoryPath)
        {
            var directoryPathWithTrailingDirectorySeparator =
                directoryPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            if (!filePath.StartsWith(directoryPathWithTrailingDirectorySeparator))
            {
                throw new ArgumentException($"Cannot make relative path (filePath: {filePath}, directoryPath: {directoryPath})");
            }

            return filePath.Substring(directoryPathWithTrailingDirectorySeparator.Length);
        }
    }
}