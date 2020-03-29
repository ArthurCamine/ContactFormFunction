using ContactForm.Function.Commands;
using System.Threading.Tasks;

namespace ContactForm.Function.EmailServices
{
    public interface IEmailService
    {
        Task SendMail(ContactFormCommand command);
    }
}
