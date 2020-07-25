using Microsoft.Extensions.Logging;
using System;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Data
{
    // TODO: Add progress tracking events
    class TuroPhotoRepository : ITuroPhotoRepository, IDisposable
    {
        private readonly TuroPhotoContext _context;
        private readonly ILogger<TuroPhotoRepository> _logger;

        public TuroPhotoRepository(TuroPhotoContext context, ILogger<TuroPhotoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Insert(LibraryCatalog catalog)
        {
            _context.LibraryCatalogs.Add(catalog);
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
