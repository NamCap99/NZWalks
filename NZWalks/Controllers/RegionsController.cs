using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.Data;
using NZWalks.Models.DTO;

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
            //// Get Data from Database - Domain Models
            //var regionsDomain = await _dbContext.Regions.ToListAsync();

            //// Map Domain Models to DTOs
            //var regionsDTO = new List<RegionsDTO>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDTO.Add(new RegionsDTO()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}

            var regionsDTO = await _dbContext.Regions
                .Select(r => new RegionsDTO
                {
                    Id = r.Id,
                    Code = r.Code,
                    Name = r.Name,
                    RegionImageUrl = r.RegionImageUrl,
                }).ToListAsync();

            // return DTOs
            return Ok(regionsDTO);
        }

        // Get Regions by ID
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {   
            // Get Region Domain Model From Database
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null) return NotFound();

            // Map the Region Domain Model to Region DTO
            var regionDTO = await _dbContext.Regions
                .Select(r => new RegionsDTO
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                }).ToListAsync();

            return Ok(regionDTO);
        }
        [HttpPost]
        // POST: https://localhost:portnumber/api/regions
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDTO request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl,
            };

            // Use Domain Model to Create Region
            _dbContext.Regions.Add(regionDomainModel);

            await _dbContext.SaveChangesAsync();

            // Map Domain model back to DTO
            var responseDTO = new RegionResponseDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(Get), new {id = responseDTO.Id}, responseDTO); // 201

        }
        [HttpPut("{id:Guid}")]
        // PUT: 
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDTO update)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != update.Id) return BadRequest("Route ID and body ID do not match");

            // 1) Load the existing Region entity
            // check if region exists
            var region = await _dbContext.Regions.FindAsync(id); // FindAsync(id) slightly cleaner
            if (region == null) return NotFound();

            // 2) Map the only allowed fields from DTO --> Entity
            region.Code = update.Code;
            region.Name = update.Name;
            region.RegionImageUrl = update.RegionImageUrl;

            // EF Core 7+ 
            //var affected = await _dbContext.Regions
            //    .Where(r => r.Id == id)
            //    .ExecuteUpdateAsync(s => s
            //    .SetProperty(r => r.Code, update.Code)
            //    .SetProperty(r => r.Name, update.Name)
            //    .SetProperty(r => r.RegionImageUrl, update.Name));

            //if (affected == 0) return NotFound();


            // 3) Save changes (EF ready tracks 'region', so this issues an UPDATE)
            await _dbContext.SaveChangesAsync();

            // convert Domain Model to DTO
            var regionDTO = new RegionsDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDTO);

            //return NoContent(); // 204 No Content (succesful but no body)
        }

        // DELETE: api/RegionsApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var region = await _dbContext.Regions.FindAsync(id);
            if (region == null) return NotFound();

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            // return the deleted Region back
            // Map domal model to DTO
            var regionDTO = new RegionsDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDTO);

            //return NoContent(); /// 204 no content
        }

    }
}
