using Example.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Context
{
    internal interface IExampleDbContext
    {
        DbSet<Satellite> Satellites { get; set; }
        DbSet<User> Users { get; set; }
    }

    internal class ExampleDbContext : DbContext, IExampleDbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Satellite> Satellites { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Satellite>().ToTable("Satellites");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}