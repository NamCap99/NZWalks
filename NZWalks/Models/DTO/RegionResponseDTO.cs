using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class RegionResponseDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
