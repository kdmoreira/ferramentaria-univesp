using Domain.Models;
using Service.EmailSender.Interfaces;
using Service.EmailService;
using Service.EmailService.Interfaces;
using System.Threading.Tasks;

namespace Service.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridEmailSender _sendGridEmailSender;
        public EmailSender(ISendGridEmailSender sendGridEmailSender)
        {
            _sendGridEmailSender = sendGridEmailSender;
        }

        public async Task EnviarEmailPrimeiroAcessoAsync(Colaborador colaborador, string token)
        {
            var assunto = "Primeiro Acesso";
            var templateID = "d-1fac830baf394a0186bdd185b8c12b14";
            var templateData = new TemplateData
            {
                Username = colaborador.Nome,
                Weblink = $"https://www.exemplo.com/primeiro-acesso/" + $"{token}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
        }
    }
}
