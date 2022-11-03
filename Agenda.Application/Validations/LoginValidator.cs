using Agenda.Application.ViewModels.Login;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");
        }
    }
}
