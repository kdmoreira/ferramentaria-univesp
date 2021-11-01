using Domain.DTOs;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task PrimeiroAcessoAsync(NovaSenhaDTO dto);
        Task<string> LoginAsync(LoginDTO dto);
        Task AlterarSenhaAsync(AlterarSenhaDTO dto);
        Task<string> RecuperarSenhaAsync(RecuperarSenhaDTO dto);
        Task<Usuario> AdicionarAsync(ColaboradorCriacaoDTO dto, Colaborador colaborador, Guid usuarioLogadoID);
        void Alterar(ColaboradorCriacaoDTO dto, Colaborador colaborador, Colaborador antigo, Guid usuarioLogadoID);
        void Ativar(Colaborador colaborador, Guid usuarioLogadoID);
        void Inativar(Colaborador colaborador, Guid usuarioLogadoID);
    }
}
