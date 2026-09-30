using AutoMapper;
using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Mappings
{
    public class MapingProfile : Profile
    {
        public MapingProfile()
        {
            CreateMap<UsuarioCreateDTO, Usuario>();
            CreateMap<UsuarioUpdateDTO, Usuario>();
            CreateMap<Usuario, UsuarioResponseDTO>();


            CreateMap<CategoriaCreateDTO, Categoria>();
            CreateMap<CategoriaUpdateDTO, Categoria>();
            CreateMap<Categoria, CategoriaResponseDTO>();


            CreateMap<MetaCreateDTO, Meta>();
            CreateMap<MetaUpdateDTO, Meta>();
            CreateMap<Meta, MetaResponseDTO>();


            CreateMap<TransaccionCreateDTO, Transaccion>();
            CreateMap<TransaccionUpdateDTO, Transaccion>();
            CreateMap<Transaccion, TransaccionResponseDTO>();
        }
    }
}
