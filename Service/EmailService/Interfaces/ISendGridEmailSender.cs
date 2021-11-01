using Service.EmailService;
using System.Threading.Tasks;

namespace Service.EmailSender.Interfaces
{
    public interface ISendGridEmailSender
    {
        Task SendEmailAsync(string email, string subject, string templateId, BaseTemplateData templateData);
    }
}
