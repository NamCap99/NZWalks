using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.Data;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    // https://localhost:`1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            //_dbContext = dbContext; // assign parameter to private field
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        // GET All Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from batabase - Domain models
            var regionsDomain = await _regionRepository.GetAllAsync();

            //var regionsDTO = regions
            //    .Select(r=> new RegionsDTO 
            //{
            //    Id = r.Id,
            //    Code = r.Code,
            //    Name = r.Name,
            //    RegionImageUrl = r.RegionImageUrl,
            //});

            // Map Domain Model to DTOs
            var regionsDTO = _mapper.Map<List<RegionsDTO>>(regionsDomain);
            // return DTOs
            return Ok(regionsDTO);
        }

        // Get Regions by ID
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            // Get Region Domain Model From Database
            //var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null) return NotFound();

            // Map the Region Domain Model to Region DTO
            //var regionDTO = new RegionsDTO
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    };

            var regionDTO = _mapper.Map<RegionsDTO>(regionDomain);


            return Ok(regionDTO);
        }
        [HttpPost]
        // POST: https://localhost:portnumber/api/regions
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDTO request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // 1) Map the incoming DTO to your domain/ entity
            var regionDomainModel = _mapper.Map<Region>(request);

            // 2) Use Domain Model to Create Region and get back to saved entity
            var created = await _regionRepository.CreateAsync(regionDomainModel);

            // 3) Map the saved entity to your response DTO
            var responseDTO = _mapper.Map<RegionResponseDTO>(created);

            return CreatedAtAction(nameof(Get), new { id = responseDTO.Id }, responseDTO); // 201

        }
        [HttpPut("{id:Guid}")]
        // PUT: 
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDTO update)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != update.Id) return BadRequest("Route ID and body ID do not match");

            //// 1) Load the existing Region entity
            //// check if region exists

            // 2) Map the only allowed fields from DTO --> Entity
            var domain = _mapper.Map<Region>(update);

            // Let the repository update and SAVE
            var updated = await _regionRepository.UpdateAsync(id, domain);
            if (updated == null) return NotFound();

            // convert Domain Model to DTO
            var regionDTO = _mapper.Map<RegionsDTO>(updated);

            return Ok(regionDTO);
        }

        // DELETE: api/RegionsApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _regionRepository.DeleteAsync(id);

            if (deleted == null) return NotFound();

            // return the deleted Region back
            // Map domal model to DTO
            var regionDTO = _mapper.Map<RegionsDTO>(deleted);

            return Ok(regionDTO);

            //return NoContent(); /// 204 no content
        }

    }
}
