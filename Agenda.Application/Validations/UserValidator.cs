using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class UserValidator : AbstractValidator<RequestUserViewModel>
    {
        public UserValidator(IRulesValidation rulesValidation)
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");

            RuleFor(x => x.UserName)
                .MustAsync((username, cancelToken) => rulesValidation.ExistUsernameAsync(username, cancelToken))
                .WithMessage("Já existe um usuário com username informado.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio")
                .EmailAddress().WithMessage("{PropertyName} informado não é válido");

            RuleFor(x => x.Email)
                .MustAsync((email, cancelToken) => rulesValidation.ExistEmailAsync(email, cancelToken))
                .WithMessage("Já existe um usuário com e-mail informado.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");

            RuleFor(x => x.UserRoleId)
                .Must(type => Enumeration.GetAll<UserRole>().Any(x => x.Id == type))
                .WithMessage("{PropertyName}: Tipo do usuário inválido");
        }
    }
}
