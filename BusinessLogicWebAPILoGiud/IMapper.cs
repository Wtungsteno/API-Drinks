using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace B_BusinessLogicWebAPILoGiud
{
    public interface IMapper
    {
        public DrinkDto MapGetEntityToDto(Drink entity);
        public Drink MapPostDtoToEntity(DrinkIdLessDto dto);
        public void MapUpdateDtoToEntity(Drink entity, DrinkIdLessDto dto);
    }
}
