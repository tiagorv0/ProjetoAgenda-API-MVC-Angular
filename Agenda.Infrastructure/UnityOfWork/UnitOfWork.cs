using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Context;

namespace Agenda.Infrastructure.UnityOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
