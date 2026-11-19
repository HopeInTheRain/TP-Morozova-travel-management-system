using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //получение списка всех стран

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }

        //получение одной страны 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        //Добавление страны
        [HttpPost]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CreateCountry),
                new { id = country.CountryId },
                country
            );
        }

        //Редвктироание страны
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, Country country)
        {
            if (id != country.CountryId) return BadRequest();
            
            var existingCountry = await _context.Countries.FindAsync(id);

            if (existingCountry == null)  return NotFound(); 

            existingCountry.CountryName = country.CountryName;
            existingCountry.CountryDescription = country.CountryDescription;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        //Удаление страны
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null) return NotFound();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
