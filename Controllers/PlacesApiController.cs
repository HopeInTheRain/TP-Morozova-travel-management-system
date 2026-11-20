
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlacesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение списка всех мест
        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            var places = await _context.Places
                .Include(p => p.Country)
                .ToListAsync();

            return Ok(places);
        }

        // Получение одного места
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlace(int id)
        {
            var place = await _context.Places
                .Include(p => p.Country)
                .FirstOrDefaultAsync(p => p.PlaceId == id);

            if (place == null)
                return NotFound();

            return Ok(place);
        }

        // Добавление места
        [HttpPost]
        public async Task<IActionResult> CreatePlace(Place place)
        {
            var country = await _context.Countries
                .FindAsync(place.CountryId);

            if (country == null)
                return BadRequest("Указанная страна не найдена");

            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPlace),
                new { id = place.PlaceId },
                place
            );
        }

        // Редактирование места
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, Place place)
        {
            if (id != place.PlaceId)
                return BadRequest();

            var existingPlace = await _context.Places
                .FindAsync(id);

            if (existingPlace == null)
                return NotFound();

            var country = await _context.Countries
                .FindAsync(place.CountryId);

            if (country == null)
                return BadRequest("Указанная страна не найдена");

            existingPlace.PlaceName = place.PlaceName;
            existingPlace.PlaceDescription = place.PlaceDescription;
            existingPlace.BaseCost = place.BaseCost;
            existingPlace.CountryId = place.CountryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Удаление места
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
                return NotFound();

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
