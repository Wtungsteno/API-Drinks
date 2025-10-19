using WebAPILoGiud.DTO;

namespace B_BusinessLogicWebAPILoGiud
{
    public interface IDrinkService
    {
        IAsyncEnumerable<DrinkDto> GetAll();
        Task<DrinkDto?> GetByIdAsync(int id, CancellationToken cancTok = default);
        Task<DrinkDto> CreateAsync(DrinkIdLessDto dto, CancellationToken cancTok = default);
        Task<bool> UpdateAsync(int id, DrinkIdLessDto dto, CancellationToken cancTok = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancTok = default);
    }
}
