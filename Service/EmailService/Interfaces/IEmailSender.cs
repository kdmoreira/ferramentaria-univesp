using Domain.Models;
using System.Threading.Tasks;

namespace Service.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task EnviarEmailPrimeiroAcessoAsync(Colaborador colaborador, string token);
        Task EnviarEmailRecuperacaoSenhaAsync(Colaborador colaborador, string token);
        Task EnviarEmailEmprestimoAtrasadoAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo);
        Task EnviarEmailEmprestimoRealizadoAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo);
        Task EnviarEmailEmprestimoPorColaboradorAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo, Colaborador admin);
    }
}
