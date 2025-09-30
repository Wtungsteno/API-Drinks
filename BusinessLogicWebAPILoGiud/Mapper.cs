using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace WebAPILoGiud
{
    public class Mapper
    {
        public DrinkDTO MapDrinkToGetDTO(Drink entity)
        {
            return new DrinkDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Size = entity.Size,
                Fizz = entity.Fizz
            };
        }

        public Drink MapPostDTOToDrink(DrinkIdLessDTO dto)
        {
            return new Drink()
            {
                Id = 0,
                Name = dto.Name,
                Size = dto.Size,
                Fizz = dto.Fizz
            };
        }

        public void MapUpdateDTOToDrink(Drink entity, DrinkIdLessDTO dto)
        {
            entity.Name = dto.Name;
            entity.Size = dto.Size;
            entity.Fizz = dto.Fizz;
        }
    }
}
