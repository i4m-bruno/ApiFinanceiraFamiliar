using AutoMapper;
using ControleFinanceiro.Application.DTOs.Despesa;
using ControleFinanceiro.Application.DTOs.Receita;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Despesa, DespesaDto>().ReverseMap();
            CreateMap<CreateDespesaDto, Despesa>();

            CreateMap<Receita, ReceitaDto>().ReverseMap();
            CreateMap<CreateReceitaDto, Receita>();
        }
    }
}