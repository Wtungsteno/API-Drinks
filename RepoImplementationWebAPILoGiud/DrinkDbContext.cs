using Microsoft.EntityFrameworkCore;
using WebAPILoGiud.Data;

namespace WebAPILoGiud
{
    public class DrinkDbContext : DbContext
    {
        public DrinkDbContext(DbContextOptions<DrinkDbContext> options)
            : base(options) { }

        public DbSet<Drink> Drinks { get; set; }
    }
}
