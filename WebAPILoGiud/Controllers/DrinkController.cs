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
        public async Task<IActionResult> Create([FromBody] DrinkIdLessDto newDrink)
        {
            DrinkDto createdDrink = await svc.CreateAsync(newDrink, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetById), createdDrink.Id, createdDrink);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DrinkIdLessDto updateDrink)
        {
            bool found = await svc.UpdateAsync(id, updateDrink, HttpContext.RequestAborted);
            if (!found)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool found = await svc.DeleteAsync(id, HttpContext.RequestAborted);
            if (!found)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
