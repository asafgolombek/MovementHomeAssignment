namespace MovementHomeAssignment.DTOs;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Maps from Domain Entity to DTO
        CreateMap<Data, DataDto>();

        // Maps from DTOs to Domain Entity
        CreateMap<CreateDataDto, Data>();
        CreateMap<UpdateDataDto, Data>()
            .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));
    }
}
