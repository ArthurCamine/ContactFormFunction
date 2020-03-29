using FluentValidation;

namespace ContactForm.Function.Commands
{
    public class ContactFormCommandValidator : AbstractValidator<ContactFormCommand>
    {
        public ContactFormCommandValidator()
        {
            ValidatorOptions.LanguageManager.Enabled = false;
            RuleFor(command => command.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(command => command.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(command => command.Message).NotNull().NotEmpty().MinimumLength(10).MaximumLength(500);
        }
    }
}
