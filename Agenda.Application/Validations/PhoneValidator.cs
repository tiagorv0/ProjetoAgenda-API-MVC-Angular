using Agenda.Application.Utils;
using Agenda.Application.ViewModels.Phone;
using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class PhoneValidator : AbstractValidator<RequestPhoneViewModel>
    {

        public PhoneValidator(RulesValidation rulesValidation)
        {
            RuleFor(x => x.FormattedPhone)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazio")
                .Must(x => PhoneUtils.IsValid(x))
                .WithMessage("{PropertyName}: {PropertyValue} - Formato de telefone inválido: (xx) x?xxxx-xxxx");

            RuleFor(x => x.FormattedPhone).MustAsync((p, cancelToken) => rulesValidation.ExistFormattedPhoneAsync(p , cancelToken))
                .WithMessage("Telefone já existe {PropertyName}: {PropertyValue}");

            RuleFor(x => x.PhoneTypeId)
                .Must(type => Enumeration.GetAll<PhoneType>().Any(x => x.Id == type))
                .WithMessage("{PropertyName} Tipo de telefone inválido");


            When(type => type.PhoneTypeId == PhoneType.Cellphone.Id, () =>
            {
                RuleFor(n => n.FormattedPhone.Length).Equal(CellPhone.TamanhoNumero)
                .WithMessage("O campo FormattedPhone precisa ser Ex: (11) 99999-9999");
            });

            When(type => type.PhoneTypeId == PhoneType.Residencial.Id, () =>
            {
                RuleFor(n => n.FormattedPhone.Length).Equal(Residencial.TamanhoNumero)
                .WithMessage("O campo FormattedPhone precisa ser Ex: (11) 3333-3333");
            });
        }
    }
}
