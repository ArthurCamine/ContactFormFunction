using ContactForm.Function.EmailServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContactForm.Function
{
    public static class ContactFormFunction
    {
        private static IEmailService _emailService;
        private static ContactFormCommandValidator _validator;
        private static ILogger _logger;

        [FunctionName("ContactFormFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] 
            HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ContactFormCommand command = JsonConvert.DeserializeObject<ContactFormCommand>(requestBody);

            ResolveDependencies(log);

            _logger.LogInformation("Validating command");
            var validatorResult = _validator.Validate(command);

            if (!validatorResult.IsValid)
            {
                return new BadRequestObjectResult(new CommandResult()
                {
                    Success = false,
                    Message = "Invalid command. See errors for more info.",
                    Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }

            await _emailService.SendMail(command);

            return new OkResult();
        }

        private static void ResolveDependencies(ILogger log)
        {
            _logger = log;
            _emailService = new EmailService();
            _validator = new ContactFormCommandValidator();
        }
    }
}
