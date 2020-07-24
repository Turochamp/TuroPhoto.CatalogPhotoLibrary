using Microsoft.Extensions.Logging;
using System;
using TuroPhoto.CatalogPhotoLibraryApp.Model;

namespace TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.Repository
{
    class AlbumIndexRepository : IAlbumIndexRepository, IDisposable
    {
        private readonly AlbumIndexContext _context;
        private readonly ILogger<AlbumIndexRepository> _logger;

        public AlbumIndexRepository(AlbumIndexContext context, ILogger<AlbumIndexRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Insert(AlbumIndex albumIndex)
        {
            _context.AlbumIndex.Add(albumIndex);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Exception TrySave()
        {
            try
            {
                Save();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
