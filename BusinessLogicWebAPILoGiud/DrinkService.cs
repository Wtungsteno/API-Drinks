using D_RepoAbstrWebAPILoGiud;
using ITS.Day2.BL;
using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace B_BusinessLogicWebAPILoGiud
{
    public class DrinkService(IAppRepository repo, IMapper mapper, IHttpClientFactory httpClientFactory) : IDrinkService
    {
        public IAsyncEnumerable<DrinkDto> GetAll()
        {
            return repo.GetAll().Select(mapper.MapGetEntityToDto);
        }

        public async Task<DrinkDto?> GetByIdAsync(int id, CancellationToken cancToken = default)
        {
            Drink? found = await repo.GetByIdAsync(id, cancToken);
            if (found != null)
            {
                return mapper.MapGetEntityToDto(found);
            }
            return null;
        }

        public async Task<DrinkDto> CreateAsync(DrinkIdLessDto dto, CancellationToken cancToken = default)
        {
            Drink entity = mapper.MapPostDtoToEntity(dto);
            repo.Add(entity);
            await repo.SaveAsync(cancToken);
            return mapper.MapGetEntityToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, DrinkIdLessDto dto, CancellationToken cancToken = default)
        {
            Drink? found = await repo.GetByIdAsync(id, cancToken);
            if (found is null)
            {
                return false;
            }
            mapper.MapUpdateDtoToEntity(found, dto);
            await repo.SaveAsync(cancToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancToken = default)
        {
            Drink? found = await repo.GetByIdAsync(id, cancToken);
            if(found is null)
            {
                return false;
            }
            repo.Delete(found);
            await repo.SaveAsync(cancToken);
            return true;
        }

        public async Task TestExternalApi(CancellationToken cancToken)
        {
            HttpClient client = httpClientFactory.CreateClient("ExternalApi");
            using HttpResponseMessage response = await client.GetAsync("https://webhook.site/6f7616cd-b9b5-4120-aff9-425773f52142", cancToken);

            if (!response.IsSuccessStatusCode)
            {
                throw SolutionLogging.ExternalApiError.ToException(await response.Content.ReadAsStringAsync(cancToken));
            }
        }
    }
}