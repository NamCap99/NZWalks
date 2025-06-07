using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Code must be 10 characters or fewer.")] // MCV will validate string length on model-binding
        public string Code { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
        public string Name { get; set; }
        // optional field-on Requied attribute
        public string? RegionImageUrl { get; set; }
    }
}
