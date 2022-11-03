using System.Linq.Expressions;
using Agenda.Domain.Core;

namespace Agenda.Application.Params
{
    public abstract class BaseParams<T> where T : Register
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 5;

        public abstract Expression<Func<T, bool>> Filter();
        protected BaseParams() { }
    }
}
