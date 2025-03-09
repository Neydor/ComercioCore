﻿using AutoMapper;
using ComercioCore.Application.DTOs;
using ComercioCore.Application.DTOs.Comerciante;
using ComercioCore.Application.DTOs.Establecimiento;
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
            CreateMap<Comerciante, ComercianteDto>()
                .ForMember(dest => dest.MunicipioNombre,
                    opt => opt.MapFrom(src => src.Municipio.Nombre))
                .ForMember(dest => dest.Establecimientos,
                    opt => opt.MapFrom(src => src.Establecimientos))
                .ReverseMap()
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore());

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
        }
    }
}