using AutoMapper;
using SIGCOMT.Domain;

namespace SIGCOMT.DTO.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(p => p.NombreCompleto, q => q.MapFrom(x => x.Nombre + " " + x.Apellido));
        }
    }
}