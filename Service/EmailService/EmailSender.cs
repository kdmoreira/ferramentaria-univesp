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

        public async Task EnviarEmailEmprestimoAtrasadoAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo)
        {
            var assunto = "Empréstimo em Atraso";
            var templateID = "d-6049f33c6e1f495e9a65044a50e36399";
            var templateData = new EmprestimoAtrasadoTemplateData
            {
                Username = colaborador.Nome,
                Quantidade = emprestimo.Quantidade.ToString(),
                Ferramenta = ferramenta.Descricao,
                Weblink = $"https://www.exemplo.com/Emprestimo/Colaborador/" + $"{colaborador.ID}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
        }

        public async Task EnviarEmailEmprestimoPorColaboradorAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo, Colaborador admin)
        {
            var assunto = "Empréstimo Realizado por Colaborador";
            var templateID = "d-97bbacfa71024f079e7c658299b89615";
            var templateData = new EmprestimoPorColaboradorTemplateData
            {
                Username = admin.Nome,
                Colaborador = $"{colaborador.Nome} {colaborador.Sobrenome}",
                Quantidade = emprestimo.Quantidade.ToString(),
                Ferramenta = ferramenta.Descricao,
                Devolucao = emprestimo.DataDevolucao.Date.ToString(),
                Weblink = $"https://www.exemplo.com/Emprestimo/" + $"{emprestimo.ID}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
        }

        public async Task EnviarEmailEmprestimoRealizadoAsync(Colaborador colaborador, Ferramenta ferramenta, Emprestimo emprestimo)
        {
            var assunto = "Empréstimo de Ferramenta";
            var templateID = "d-58c5999912b24710a58744eddba74401";
            var templateData = new EmprestimoRealizadoTemplateData
            {
                Username = colaborador.Nome,
                Quantidade = emprestimo.Quantidade.ToString(),
                Ferramenta = ferramenta.Descricao,
                Devolucao = emprestimo.DataDevolucao.Date.ToString(),
                Weblink = $"https://www.exemplo.com/Emprestimo/Colaborador/" + $"{colaborador.ID}"
            };
            await _sendGridEmailSender.SendEmailAsync(colaborador.Email, assunto, templateID, templateData);
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
