using Microsoft.EntityFrameworkCore;
using TuroPhoto.CatalogPhotoLibraryApp.Model;

namespace TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.Repository
{
    class AlbumIndexContext : DbContext
    {
        public DbSet<AlbumIndex> AlbumIndex { get; set; }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public AlbumIndexContext()
        {
        }

        public AlbumIndexContext(DbContextOptions<AlbumIndexContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=AlbumIndex1;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumIndex>().HasKey(e => e.Id);
            modelBuilder.Entity<AlbumIndex>().Property(e => e.Created);
            modelBuilder.Entity<AlbumIndex>().Property(e => e.ComputerName);
            modelBuilder.Entity<AlbumIndex>().Property(e => e.DirectoryPath);

            modelBuilder.Entity<Directory>().HasKey(e => e.Id);
            modelBuilder.Entity<Directory>().Property(e => e.RelativePath).HasColumnName("Path");
            modelBuilder.Entity<Directory>().Ignore(e => e.Path);

            modelBuilder.Entity<Photo>().HasKey(e => e.Id);
            modelBuilder.Entity<Photo>().Property(e => e.SourceFileName).HasColumnName("FileName");
            modelBuilder.Entity<Photo>().Ignore(e => e.RelativeSourceFilePath);
            modelBuilder.Entity<Photo>().Ignore(e => e.SourceDirectory);
            modelBuilder.Entity<Photo>().Property(e => e.DateTimeFromFile);
            modelBuilder.Entity<Photo>().OwnsOne(
                e => e.ImageMetaData,
                imd =>
                {
                    imd.Property(e => e.DateTime).HasColumnName("DateTimeFromMetaData");
                    imd.Ignore(e => e.GeoLocation);
                    imd.Ignore(e => e.GpsTags);
                });
        }
    }
}
