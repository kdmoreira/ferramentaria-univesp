using System.Threading.Tasks;

namespace Service.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string templateId, TemplateData templateData);
    }
}
