using ContactForm.Function.Commands;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ContactForm.Function.EmailServices
{
    public class EmailService : IEmailService
    {
        private static readonly string _apiKey = Environment.GetEnvironmentVariable("SendGridAPIKey", EnvironmentVariableTarget.Process);
        private static readonly string _email = Environment.GetEnvironmentVariable("SendGridUserEmail", EnvironmentVariableTarget.Process);
        private static readonly string _userName = Environment.GetEnvironmentVariable("SendGridUserName", EnvironmentVariableTarget.Process);
        public async Task SendMail(ContactFormCommand command)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(command.Email, command.Name);
            var subject = "New message from your contact form";
            var to = new EmailAddress(_email, _userName);
            var plainTextContent = command.Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, string.Empty);

            await client.SendEmailAsync(msg);
        }
    }
}
