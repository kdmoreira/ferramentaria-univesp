using Domain.Models;
using Service.EmailSender.Interfaces;
using Service.EmailService.Interfaces;
using System.Threading.Tasks;

namespace Service.EmailService
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
            var templateData = new PrimeiroAcessoTemplateData
            {
                Username = colaborador.Nome,
                Weblink = $"https://www.exemplo.com/primeiro-acesso/" + $"{token}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
        }

        public async Task EnviarEmailRecuperacaoSenhaAsync(Colaborador colaborador, string token)
        {
            var assunto = "Recuperação de Senha";
            var templateID = "d-33338b8fed3b4d578af6833093a7000a";
            var templateData = new RecuperacaoSenhaTemplateData
            {
                Username = colaborador.Nome,
                Weblink = $"https://www.exemplo.com/recuperacao-senha/" + $"{token}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
        }
    }
}
