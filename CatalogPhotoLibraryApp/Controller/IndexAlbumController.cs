using TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.File;
using TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.Repository;
using TuroPhoto.CatalogPhotoLibraryApp.Model;
using TuroPhoto.CatalogPhotoLibraryApp.View;

namespace TuroPhoto.CatalogPhotoLibraryApp.Controller
{
    // TODO: Make crawler and repository execute directories in parallel. Feasable?
    // TODO: Add IDispose
    // TODO: Refactor View into more suitable model for console. What's more suitable?
    class IndexAlbumController
    {
        public Configuration Configuration { get; set; }

        private readonly IndexAlbumView _view;
        private readonly IPhotoDirectoryCrawler _photoDirectoryCrawler;
        private readonly IAlbumIndexRepository _albumIndexRepository;

        public IndexAlbumController(
            IndexAlbumView view,
            IPhotoDirectoryCrawler photoDirectoryCrawler,
            IAlbumIndexRepository albumIndexRepository)
        {
            _view = view;
            _photoDirectoryCrawler = photoDirectoryCrawler;
            _albumIndexRepository = albumIndexRepository;
        }

        public void InitConfiguration(string[] directoryPaths)
        {
            Configuration = new Configuration();
            Configuration.InitConfiguration();
            Configuration.DirectoryPaths = directoryPaths;
        }

        public void Run()
        {
            Starting();

            foreach (var directoryPath in Configuration.DirectoryPaths)
            {
                RunDirectory(directoryPath);
            }

            // Output results
            Closing();
        }

        private void RunDirectory(string directoryPath)
        {
            // Find all photos. Does not read metadata.
            var (photosRead, readErrors) = _photoDirectoryCrawler.FindPhotos(
                directoryPath, _view, true);
            _view.HandleMessage($"\n{photosRead.Count} photos in source directory ({readErrors.Count} errors)");

            // TBD: Handle read errors?

            // Create album index with metadata
            var albumIndex = new AlbumIndex(Configuration.ComputerName, directoryPath, photosRead);
            albumIndex.Init();

            // Persist to database
            _albumIndexRepository.Insert(albumIndex);
            _view.HandleMessage($"\nPrepared saving AlbumIndex ({albumIndex.Directories.Count} directories, {albumIndex.Photos.Count} photos)");

            // Catch exception thrown by EF Core
            var exception = _albumIndexRepository.TrySave();

            var message = "Saved AlbumIndex";
            if (exception != null)
            {
                _view.HandleException(exception, message);
            }
            else
            {
                _view.HandleMessage(message);
            }
        }

        private void Closing()
        {
            _view.HandleMessage("Closing TBD");
        }

        private void Starting()
        {
            var settingsMessage =
                $" Directory Path: {Configuration.Version}\n Computer: {Configuration.ComputerName}";
            var message = $"AlbumIndexer1 starting up.\n{settingsMessage}.";

            _view.HandleMessage(message);
        }
    }
}
