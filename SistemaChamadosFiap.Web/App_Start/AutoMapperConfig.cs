using AutoMapper;
using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaChamadosFiap.Web
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                // map properties with public or internal getters
                //cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                cfg.CreateMap<tbAtendente, AtendenteModel>().ReverseMap();
                cfg.CreateMap<tbChamado, ChamadoModel>().ReverseMap();
                cfg.CreateMap<tbChamadoInteracao, ChamadoInteracaoModel>().ReverseMap();
                cfg.CreateMap<tbCliente, ClienteModel>().ReverseMap();
            });
        }
    }
}