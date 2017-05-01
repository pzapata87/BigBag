using AutoMapper;
using SIGCOMT.Domain;

namespace SIGCOMT.DTO.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<LogInDto, Usuario>();
        }
    }
}