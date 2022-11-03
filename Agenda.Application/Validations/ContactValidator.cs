using Agenda.Application.Interfaces;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels.Contact;

namespace Agenda.Application.Validations
{
    public class ContactValidator : ContactBaseValidator<RequestContactViewModel>
    {
        public ContactValidator(IRulesValidation rulesValidation)
        {
            RuleForEach(x => x.Phones).SetValidator(new PhoneValidator(rulesValidation));
        }
    }
}
