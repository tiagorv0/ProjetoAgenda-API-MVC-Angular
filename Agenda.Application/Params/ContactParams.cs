using System.Linq.Expressions;
using Agenda.Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Params
{
    public class ContactParams : BaseParams<Contact>
    {
        public string? Name { get; set; }
        public int? DDD { get; set; }
        public string? Number { get; set; }

        public override Expression<Func<Contact, bool>> Filter()
        {
            return FilterContact();
        }

        protected Expression<Func<Contact, bool>> FilterContact()
        {
            var predicate = PredicateBuilder.New<Contact>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{Name}%"));

            if (DDD.HasValue)
                predicate = predicate.And(x => x.Phones.Any(x => x.DDD == DDD));

            if (!string.IsNullOrEmpty(Number))
                predicate = predicate.And(x => x.Phones.Any(x => EF.Functions.Like(x.Number, $"%{Number}%")));

            if (predicate.IsStarted)
                return predicate;
            else
                return null;
        }
    }
}
