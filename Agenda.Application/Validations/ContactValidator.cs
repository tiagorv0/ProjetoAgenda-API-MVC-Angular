using Agenda.Application.Utils;
using Agenda.Application.ViewModels.Contact;

namespace Agenda.Application.Validations
{
    public class ContactValidator : ContactBaseValidator<RequestContactViewModel>
    {
        public ContactValidator(RulesValidation rulesValidation)
        {
            RuleForEach(x => x.Phones).SetValidator(new PhoneValidator(rulesValidation));
        }
    }
}
