using Domain.Models;
using System.Threading.Tasks;

namespace Service.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task EnviarEmailPrimeiroAcessoAsync(Colaborador colaborador, string token);
    }
}
