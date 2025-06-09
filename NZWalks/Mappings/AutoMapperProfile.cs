using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionsDTO>().ReverseMap();
            CreateMap<CreateRegionRequestDTO, Region>().ReverseMap();  // for POST response
            CreateMap<RegionResponseDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        }
    }
}
