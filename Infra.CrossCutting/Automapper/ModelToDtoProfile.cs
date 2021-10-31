using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using System.Linq;

namespace Infra.CrossCutting.Automapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            // Colaborador
            CreateMap<Colaborador, ColaboradorListagemDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Emprestimos
                .Any(x => x.Status == StatusEmprestimoEnum.EmAtraso) ? "EM ATRASO" : "EM DIA"));

            CreateMap<Colaborador, ColaboradorDTO>();            

            // Ferramenta
            CreateMap<Ferramenta, FerramentaListagemDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()));

            CreateMap<Ferramenta, FerramentaDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()))
                .ForMember(dest => dest.Categoria, opt =>
                opt.MapFrom(src => src.Categoria.Descricao));

            // Emprestimo
            CreateMap<Emprestimo, EmprestimoListagemDTO>()
                .ForMember(dest => dest.ColaboradorCPF, opt =>
                opt.MapFrom(src => src.Colaborador.CPF))
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()));

            CreateMap<Emprestimo, EmprestimoDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()));            
        }
    }
}
