using B_BusinessLogicWebAPILoGiud;
using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace WebAPILoGiud
{
    public class Mapper : IMapper
    {
        public DrinkDto MapGetEntityToDto(Drink entity)
        {
            return new DrinkDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Size = entity.Size,
                Fizz = entity.Fizz
            };
        }

        public Drink MapPostDtoToEntity(DrinkIdLessDto dto)
        {
            return new Drink()
            {
                Id = 0,
                Name = dto.Name,
                Size = dto.Size,
                Fizz = dto.Fizz
            };
        }

        public void MapUpdateDtoToEntity(Drink entity, DrinkIdLessDto dto)
        {
            entity.Name = dto.Name;
            entity.Size = dto.Size;
            entity.Fizz = dto.Fizz;
        }
    }
}
