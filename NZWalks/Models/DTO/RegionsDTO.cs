namespace NZWalks.Models.DTO
{
   public class RegionsDTO
    {
        public Guid Id { get; set; } // uniqueidentifier
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
