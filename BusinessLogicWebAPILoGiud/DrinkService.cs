using D_RepoAbstrWebAPILoGiud;
using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace B_BusinessLogicWebAPILoGiud
{
    public class DrinkService(IAppRepository repo, IMapper mapper) : IDrinkService
    {
        public IAsyncEnumerable<DrinkDto> GetAll()
        {
            return repo.GetAll().Select(mapper.MapGetEntityToDto);
        }

        public async Task<DrinkDto?> GetByIdAsync(int id, CancellationToken cancToken = default)
        {
            Drink? found = await repo.GetByIdAsync(id, cancToken);
            if(found != null)
            {
                return mapper.MapGetEntityToDto(found);
            }
            return null;
        }
    }
}
