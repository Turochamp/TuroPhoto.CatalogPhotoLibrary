using System.Collections.Generic;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    public class LibraryCatalogDirectory
    {
        public int Id { get; }
        public string Path { get; }
        public List<Photo> Photos { get; private set; }
        public string RelativePath { get; internal set; }

        public LibraryCatalogDirectory(string path) : this()
        {
            Path = path;
            Photos = new List<Photo>();
        }

        public LibraryCatalogDirectory() { }

        internal void Add(Photo photo)
        {
            Photos.Add(photo);
        }

        public override string ToString()
        {
            return Path;
        }
    }
}