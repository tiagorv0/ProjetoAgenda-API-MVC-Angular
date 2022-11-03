using System.Linq.Expressions;
using Agenda.Domain.Entities;
using LinqKit;

namespace Agenda.Application.Params
{
    public class AdminContactParams : ContactParams
    {
        public int? UserId { get; set; }

        public override Expression<Func<Contact, bool>> Filter()
        {

            var predicate = PredicateBuilder.New<Contact>();

            if (UserId.HasValue)
                predicate = predicate.And(x => x.UserId == UserId);

            return predicate = FilterContact();
        }
    }
}
