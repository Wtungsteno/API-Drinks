using WebAPILoGiud.Data;

namespace D_RepoAbstrWebAPILoGiud
{
    public interface IAppRepository
    {
        IAsyncEnumerable<Drink> GetAll();
        Task<Drink?> GetByIdAsync(int id, CancellationToken cancToken = default);
        void Add(Drink entity);
        void Delete(Drink entity);
        Task SaveAsync(CancellationToken cancTok = default);
    }
}
