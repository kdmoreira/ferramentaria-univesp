using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Infra.CrossCutting.Automapper
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            // Colaborador
            CreateMap<ColaboradorCriacaoDTO, Colaborador>();
            CreateMap<ColaboradorEdicaoDTO, Colaborador>();

            // Ferramenta
            CreateMap<FerramentaCriacaoDTO, Ferramenta>();
            CreateMap<FerramentaEdicaoDTO, Ferramenta>();

            // Emprestimo
            CreateMap<EmprestimoCriacaoDTO, Emprestimo>();
        }
    }
}
