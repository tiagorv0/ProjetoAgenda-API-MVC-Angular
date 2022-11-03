using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Agenda.Infrastructure.Storage
{
    public class ContactJsonStorage : JsonStorage<Contact>, IContactJsonStorage
    {
        public ContactJsonStorage(IOptions<JsonStorageOptions> options) : base(options)
        {

        }

        public override Contact Create(Contact model)
        {
            base.Create(model);

            foreach(var phone in model.Phones)
            {
                phone.Id = model.Phones.Any() ? model.Phones.LastOrDefault().Id + 1 : model.Phones.Count() + 1;
                phone.ContactId = model.Id;
                phone.CreatedAt = DateTime.Now;
            }

            return model;
        }
    }
}
