// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.EmailSender.Interfaces;
using System.Threading.Tasks;

namespace Service.EmailService
{
    public class SendGridEmailSender : ISendGridEmailSender
    {
        public SendGridEmailSenderOptions Options { get; set; }

        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string templateId,
            TemplateData templateData)
        {
            await Execute(Options.ApiKey, subject, templateId, templateData, email);
        }

        private async Task<Response> Execute(
            string apiKey,
            string subject,
            string templateId,
            TemplateData templateData,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetTemplateId(templateId);
            msg.SetTemplateData(templateData);

            // disable tracking settings
            // ref.: https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            msg.SetOpenTracking(false);
            msg.SetGoogleAnalytics(false);
            msg.SetSubscriptionTracking(false);

            return await client.SendEmailAsync(msg);
        }
    }
}
