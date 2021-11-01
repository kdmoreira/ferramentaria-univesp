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
                .ForMember(dest => dest.NomeCompleto, opt =>
                opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.SituacaoEmprestimo, opt =>
                opt.MapFrom(src => src.Emprestimos
                .Any(x => x.Status == StatusEmprestimoEnum.EmAtraso) ? "EM ATRASO" : "EM DIA"));

            CreateMap<Colaborador, ColaboradorDTO>()
                .ForMember(dest => dest.Perfil, opt =>
                opt.MapFrom(src => src.Perfil.GetDescription()))
                .ForMember(dest => dest.Role, opt =>
                opt.MapFrom(src => src.Usuario.Role.GetDescription()))
                .ForMember(dest => dest.Supervisor, opt =>
                opt.MapFrom(src => $"{src.Supervisor.Nome} {src.Supervisor.Sobrenome}"));

            // Ferramenta
            CreateMap<Ferramenta, FerramentaListagemDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()));

            CreateMap<Ferramenta, FerramentaDTO>()
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.GetDescription()))
                .ForMember(dest => dest.Categoria, opt =>
                opt.MapFrom(src => src.Categoria.Descricao));

            CreateMap<Categoria, CategoriaDTO>();

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
