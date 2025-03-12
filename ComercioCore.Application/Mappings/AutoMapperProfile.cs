using AutoMapper;
using ComercioCore.Application.DTOs;
using ComercioCore.Application.DTOs.Comerciante;
using ComercioCore.Application.DTOs.Establecimiento;
using ComercioCore.Application.DTOs.Reportes;
using ComercioCore.Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Usuarios
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap()
                .ForMember(dest => dest.Contrasena, opt => opt.Ignore());

            CreateMap<UsuarioCreateDto, Usuario>()
                .ForMember(dest => dest.Contrasena, opt => opt.Ignore()); // La contraseña se maneja aparte

            // Comerciantes
            CreateMap<Comerciante, Comerciante>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore()); 

            CreateMap<Comerciante, ComercianteDto>()
                .ForMember(dest => dest.NombreRazonSocial,
                    opt => opt.MapFrom(src => src.RazonSocial))
                .ForMember(dest => dest.MunicipioNombre,
                    opt => opt.MapFrom(src => src.Municipio.Nombre))
                .ForMember(dest => dest.Establecimientos,
                    opt => opt.MapFrom(src => src.Establecimientos))
                .ReverseMap()
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore());

            CreateMap<Comerciante, ComercianteCreateDto>()
                .ForMember(dest => dest.NombreRazonSocial,
                    opt => opt.MapFrom(src => src.RazonSocial))
                .ReverseMap();
            CreateMap<Comerciante, ComercianteUpdateDto>()
                .ForMember(dest => dest.Telefono,
                    opt => opt.MapFrom(src => src.Telefono))
                .ReverseMap();

            CreateMap<Comerciante, ComercianteReporteDto>()
                .ForMember(dest => dest.CantidadEstablecimientos,
                    opt => opt.MapFrom(src => src.Establecimientos.Count))
                .ForMember(dest => dest.TotalIngresos,
                    opt => opt.MapFrom(src => src.Establecimientos.Sum(e => e.Ingresos)))
                .ForMember(dest => dest.TotalEmpleados,
                    opt => opt.MapFrom(src => src.Establecimientos.Sum(e => e.NumeroEmpleados)));

            // Establecimientos
            CreateMap<Establecimiento, EstablecimientoDto>()
                .ForMember(dest => dest.ComercianteId,
                    opt => opt.MapFrom(src => src.ComercianteId))
                .ReverseMap()
                .ForMember(dest => dest.ComercianteId,
                    opt => opt.MapFrom(src => src.ComercianteId))
                .ForMember(dest => dest.Comerciante, opt => opt.Ignore());

            CreateMap<Establecimiento, EstablecimientoDetailDto>()
                .IncludeBase<Establecimiento, EstablecimientoDto>()
                .ForMember(dest => dest.Comerciante,
                    opt => opt.MapFrom(src => src.Comerciante))
                .ReverseMap()
                .ForMember(dest => dest.Comerciante, opt => opt.Ignore());

            //Reportes
            CreateMap<ReporteComercianteActivoSP, ReportesComerciantesActivosDto>();
        }
    }
}