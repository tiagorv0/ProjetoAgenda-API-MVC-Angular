using Agenda.Application.Interfaces;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels.AdminContact;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class AdminContactValidator : ContactBaseValidator<RequestAdminContactViewModel>
    {
        public AdminContactValidator(IRulesValidation userValidation)
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("{PropertyName} não pode ser nulo")
                                  .NotEmpty().WithMessage("{PropertyName} não pode ser vazio")
                                  .NotEqual(0).WithMessage("{PropertyName} não pode ser 0")
                                  .MustAsync((userid, cancelToken) => userValidation.ExistUserIdAsync(userid, cancelToken))
                                  .WithMessage("{PropertyName} Id não encontrado");

            RuleForEach(x => x.Phones).SetValidator(new PhoneValidator(userValidation));
        }
    }
}
