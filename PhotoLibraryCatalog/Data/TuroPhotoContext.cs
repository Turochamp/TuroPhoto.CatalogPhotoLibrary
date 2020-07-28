using Microsoft.EntityFrameworkCore;
using TuroPhoto.PhotoLibraryCatalog.Model;

namespace TuroPhoto.PhotoLibraryCatalog.Data
{
    // TODO: Add schema. https://docs.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=fluent-api#table-schema
    // TODO: Move Migrations folder to Data\Migrations
    class TuroPhotoContext : DbContext
    {
        public DbSet<LibraryCatalog> LibraryCatalogs { get; set; }
        public DbSet<LibraryCatalogDirectory> LibraryCatalogDirectories { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public TuroPhotoContext()
        {
        }

        public TuroPhotoContext(DbContextOptions<TuroPhotoContext> options) : base(options)
        {
        }

        // TODO: Replace with configuration value. Removing DRY smell.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=TuroPhoto1;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryCatalog>().ToTable("LibraryCatalog");
            modelBuilder.Entity<LibraryCatalog>().HasKey(e => e.Id);
            modelBuilder.Entity<LibraryCatalog>().Property(e => e.Created);
            modelBuilder.Entity<LibraryCatalog>().Property(e => e.ComputerName);
            modelBuilder.Entity<LibraryCatalog>().Property(e => e.DirectoryPath);

            modelBuilder.Entity<LibraryCatalogDirectory>().ToTable("LibraryCatalogDirectory");
            modelBuilder.Entity<LibraryCatalogDirectory>().HasKey(e => e.Id);
            modelBuilder.Entity<LibraryCatalogDirectory>().Property(e => e.RelativePath).HasColumnName("Path");
            modelBuilder.Entity<LibraryCatalogDirectory>().Ignore(e => e.Path);

            modelBuilder.Entity<Photo>().ToTable("Photo");
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
