using B_BusinessLogicWebAPILoGiud;
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
    public class DrinkController(IDrinkService svc) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await svc.GetAll().ToListAsync(HttpContext.RequestAborted));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            DrinkDto? found = await svc.GetByIdAsync(id, HttpContext.RequestAborted);
            if (found == null)
            {
                return NotFound($"Drink with id {id} not found.");
            }
            return Ok(found);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DrinkIdLessDTO drinkDto)
        {
            Drink newDrink = _mapper.MapPostDTOToDrink(drinkDto);
            await _ctx.Drinks.AddAsync(newDrink);
            await _ctx.SaveChangesAsync();
            return Created($"{HttpContext.Request.Path}/{newDrink.Id}", newDrink);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(DrinkIdLessDTO drinkDto, int id)
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
        public async Task<IActionResult> Delete(int id)
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
