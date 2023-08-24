using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelDirectory.Models;
using HotelDirectory.Data;
using Microsoft.AspNetCore.Authorization;

namespace HotelDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelDirectoryDbContext _context;

        public HotelController(HotelDirectoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel(int PageNumber =1, int PageSize = 10)
        {
            return await _context.Hotel
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;
            _context.Entry(hotel).Property(x => x.Images).IsModified = false;
            _context.Entry(hotel).Property(x => x.Amenities).IsModified = false;
            _context.Entry(hotel).Property(x => x.Name).IsModified = false;
            _context.Entry(hotel).Property(x => x.Location).IsModified = false;
            _context.Entry(hotel).Property(x => x.Description).IsModified = false;
            _context.Entry(hotel).Property(x => x.WebUrl).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(hotel);
        }

        // POST: api/Hotel
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.Id == id);
        }

    }
}