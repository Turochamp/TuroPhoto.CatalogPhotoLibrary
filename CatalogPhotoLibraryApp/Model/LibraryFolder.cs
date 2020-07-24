using System;

namespace TuroPhoto.CatalogPhotoLibraryApp.Model
{
    class LibraryFolder : IEquatable<LibraryFolder>, IComparable<LibraryFolder>
    {
        public static LibraryFolder Create(string path)
        {
            return new LibraryFolder(path);
        }

        public LibraryFolder(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public static string GetFolderName(DateTime dateTime)
        {
            return string.Format(@"{0:yyyy}{1}{0:yyyy-MM-dd}", dateTime, System.IO.Path.DirectorySeparatorChar);
        }

        public bool Equals(LibraryFolder other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Path, other.Path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LibraryFolder)obj);
        }

        public override int GetHashCode()
        {
            return Path != null ? Path.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return Path;
        }

        public int CompareTo(LibraryFolder other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Path, other.Path, StringComparison.Ordinal);
        }
    }
}