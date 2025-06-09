using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.Data;
using NZWalks.Models.DTO;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {

            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null) return null;

            //return await existingRegion.Regions.ToListAsync();
            return existingRegion;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FindAsync(id);
            if (existingRegion == null) return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FindAsync(id);

            if (existingRegion == null) return null;

           _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
