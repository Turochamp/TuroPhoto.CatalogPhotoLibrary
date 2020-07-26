namespace TuroPhoto.PhotoLibraryCatalog.Model.Dto
{
    public enum PhotoPathType
    {
        Source,
        Library
    }

    public class PhotoPath
    {
        public PhotoPath(PhotoPathType type, string path)
        {
            Type = type;
            Path = path;
        }

        public PhotoPathType Type { get; set; }
        public string Path { get; set; }
    }
}