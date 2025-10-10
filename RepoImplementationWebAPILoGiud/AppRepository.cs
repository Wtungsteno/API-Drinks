using D_RepoAbstrWebAPILoGiud;
using Microsoft.EntityFrameworkCore;
using WebAPILoGiud;
using WebAPILoGiud.Data;

namespace E_RepoImplWebAPILoGiud
{
    public partial class AppRepository(DrinkDbContext db) : IAppRepository
    {
        public IAsyncEnumerable<Drink> GetAll()
        {
            return db.Drinks
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .AsAsyncEnumerable();
        }

        public Task<Drink?> GetByIdAsync(int id, CancellationToken cancToken = default)
        {
            return db.Drinks.SingleOrDefaultAsync(d => d.Id == id, cancToken);
        }

        public void Add(Drink entity)
        {
            db.Drinks.Add(entity);
        }

        public void Delete(Drink entity)
        {
            db.Drinks.Remove(entity);
        }

        public Task SaveAsync(CancellationToken cancToken = default)
        {
            return db.SaveChangesAsync(cancToken);
        }
    }
}
