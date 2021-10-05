namespace LibraryManager.Infrastructure
{
    using LibraryManager.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class LibraryManagerDbContext : DbContext
    {
        public LibraryManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; }
    }
}
