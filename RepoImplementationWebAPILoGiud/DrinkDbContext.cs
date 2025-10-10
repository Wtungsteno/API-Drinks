using Microsoft.EntityFrameworkCore;
using WebAPILoGiud.Data;

namespace WebAPILoGiud
{
    public class DrinkDbContext : DbContext
    {
        public DrinkDbContext(DbContextOptions<DrinkDbContext> options)
            : base(options) { }

        public DbSet<Drink> Drinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Drink>(typeBuilder =>
            {
                typeBuilder
                    .ToTable(nameof(Drink));

                typeBuilder
                    .Property(d => d.Id)
                    .ValueGeneratedOnAdd();

                typeBuilder
                    .Property(d => d.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                typeBuilder
                    .Property(d => d.Size)
                    .HasMaxLength(200)
                    .IsRequired();

                typeBuilder
                    .Property(d => d.Fizz)
                    .IsRequired();

                typeBuilder
                    .HasKey(d => d.Id);

                typeBuilder
                    .HasIndex(d => d.Name)
                    .IsUnique();
            });
        }
    }
}
