using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.Data;

namespace NZWalks.Controllers
{
    // https://localhost:`1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext; // assign parameter to private field
        }

        // GET All Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _dbContext.Regions.ToListAsync();

            return Ok(regions);
        }

        // Get Regions by ID
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var regions = await _dbContext.Regions.FindAsync(id);
            if (regions == null) return NotFound();

            return Ok(regions);
        }
        [HttpPost]
        // POST: https://localhost:portnumber/api/regions
        public async Task<IActionResult> Create([FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            region.Id = Guid.NewGuid();
            _dbContext.Regions.Add(region);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new {id = region.Id}, region);

        }
        [HttpPut]
        // PUT: 
        public async Task<IActionResult> Update(Guid id, [FromBody] Region region)
        {
            if (id != region.Id) return BadRequest();

            if (!await _dbContext.Regions.AnyAsync(r => r.Id == id)) return NotFound(id);

            _dbContext.Entry(region).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();


            return NoContent(); // 204 No Content (succesful but no body)
        }

        // DELETE: api/RegionsApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var region = await _dbContext.Regions.FindAsync(id);
            if (region == null) return NotFound();

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            return NoContent(); // 204 no content
        }

    }
}
