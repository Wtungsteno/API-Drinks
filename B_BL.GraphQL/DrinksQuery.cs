using Microsoft.EntityFrameworkCore;
using WebAPILoGiud;
using WebAPILoGiud.DTO;

namespace B_BL.GraphQL
{
    public class DrinksQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<DrinkDto> GetDrinks(DrinkDbContext context)
        {
            return context
                .Drinks
                .AsNoTracking()
                .Select(d => new DrinkDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Size = d.Size,
                    Fizz = d.Fizz
                });
        }
    }
}
