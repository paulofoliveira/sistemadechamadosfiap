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
                cfg.CreateMap<tbChamadoInteracao, ChamadoInteracaoModel>().ReverseMap();
                cfg.CreateMap<tbChamado, ChamadoModel>()
                .ForMember(p => p.Interacoes, q => q.MapFrom(r => Mapper.Map<ICollection<tbChamadoInteracao>, IEnumerable<ChamadoInteracaoModel>>(r.tbChamadoInteracaos)))
                .ReverseMap()
                .ForMember(p => p.tbChamadoInteracaos, q => q.MapFrom(r => Mapper.Map<IEnumerable<ChamadoInteracaoModel>, ICollection<tbChamadoInteracao>>(r.Interacoes)))
                .AfterMap((chamadoModel, chamado) =>
                {
                    if (chamado.Id > 0)
                    {
                        foreach (var interacao in chamado.tbChamadoInteracaos)
                        {
                            interacao.IdChamado = chamado.Id;
                            interacao.tbChamado = chamado;
                        }
                    }
                });

                cfg.CreateMap<tbCliente, ClienteModel>().ReverseMap();
            });
        }
    }
}