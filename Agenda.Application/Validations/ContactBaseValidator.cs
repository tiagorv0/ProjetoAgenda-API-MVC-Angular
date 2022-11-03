using Agenda.Application.ViewModels.Contact;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class ContactBaseValidator<T> : AbstractValidator<T> where T : RequestContactViewModel
    {
        public ContactBaseValidator()
        {
            RuleFor(x => x.Name)
                    .NotNull().WithMessage("{PropertyName} não pode ser nulo")
                    .NotEmpty().WithMessage("{PropertyName} não pode ser vazio");

            RuleFor(x => x.Phones)
                .Must(phones => phones.GroupBy(x => x.FormattedPhone).All(group => group.Count() == 1))
                .WithMessage("Existem telefones duplicados na lista.");
        }
    }
}
