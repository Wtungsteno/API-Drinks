using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPILoGiud.Data;
using WebAPILoGiud.DTO;

namespace WebAPILoGiud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class DrinkController : ControllerBase
    {
        private readonly DrinkDbContext _ctx;
        private readonly Mapper _mapper;
        private readonly ILogger<DrinkDbContext> _logger;

        public DrinkController(DrinkDbContext ctx, Mapper mapper, ILogger<DrinkDbContext> logger)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Drink()
        {
            List<Drink>? records = await _ctx.Drinks.ToListAsync();
            List<DrinkDTO> converted = records.ConvertAll(_mapper.MapDrinkToGetDTO);
            return Ok(converted);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Drink(int id, CancellationToken ct)
        {
            Drink? found = await _ctx.Drinks.SingleOrDefaultAsync(d => d.Id == id);
            if (found == null)
            {
                return NotFound(id);
            }
            DrinkDTO converted = _mapper.MapDrinkToGetDTO(found);
            return Ok(converted);
        }

        [HttpPost]
        public async Task<IActionResult> Drink(DrinkIdLessDTO drinkDto)
        {
            Drink newDrink = _mapper.MapPostDTOToDrink(drinkDto);
            await _ctx.Drinks.AddAsync(newDrink);
            await _ctx.SaveChangesAsync();
            return Created($"{HttpContext.Request.Path}/{newDrink.Id}", newDrink);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Drink(DrinkIdLessDTO drinkDto, int id)
        {
            Drink? found = await _ctx.Drinks.SingleOrDefaultAsync(d => d.Id == id);
            if (found == null)
            {
                _logger.LogWarning($"Drink with id {id} not found.");
                return NotFound(id);
            }
            _mapper.MapUpdateDTOToDrink(found, drinkDto);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Drink(int id)
        {
            Drink? found = await _ctx.Drinks.SingleOrDefaultAsync(d => d.Id == id);
            if (found == null)
            {
                _logger.LogWarning($"Drink with id {id} not found.");
                return NotFound(id);
            }
            _ctx.Drinks.Remove(found);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
