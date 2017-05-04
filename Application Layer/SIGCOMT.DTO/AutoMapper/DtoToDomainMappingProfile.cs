using AutoMapper;
using SIGCOMT.Common;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;

namespace SIGCOMT.DTO.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<LogInDto, Usuario>();
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(p => p.Estado, q => q.MapFrom(x => TipoEstado.Activo.GetNumberValue()));
        }
    }
}