using AutoMapper;
using prolabcheck.domain.DTOs;
using prolabcheck.domain.entities;

namespace prolabcheck.application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<tb_productos, ProductDTO>()
                .ForMember(dest => dest.product, opt => opt.MapFrom(src => src.TXT_NOMBRE_PRODUCTO))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.TXT_PRODUCTO_CONTENIDO))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.NU_PRODUCTO_CONTENIDO))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.TXT_FECHA_PRODUCTO))
                .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.TXT_PRODUCTO_CODIGO))
                .ForMember(dest => dest.numberOfSearches, opt => opt.MapFrom(src => src.NUM_CANTIDAD_BUSQUEDA));

            //CreateMap<tb_productos, ProductDTO>();
            //CreateMap<ProductDTO, tb_productos>();
        }
    }
}
