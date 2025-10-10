using D_RepoAbstrWebAPILoGiud;
using WebAPILoGiud.DTO;

namespace B_BusinessLogicWebAPILoGiud
{
    public class DrinkService(IAppRepository repo, IMapper mapper) : IDrinkService
    {
        public IAsyncEnumerable<DrinkDto> GetAll()
        {
            return repo.GetAll().Select(mapper.MapGetEntityToDto);
        }
    }
}
