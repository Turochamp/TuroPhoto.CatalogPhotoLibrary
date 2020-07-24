namespace TuroPhoto.CatalogPhotoLibraryApp.Model.Dto
{
    enum PhotoPathType
    {
        Source,
        Library
    }

    class PhotoPath
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