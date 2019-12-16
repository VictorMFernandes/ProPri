using Microsoft.EntityFrameworkCore;
using ProPri.Core.Communication.Handlers;
using System;
using System.Threading.Tasks;

namespace ProPri.Core.Data
{
    public class Repository<T> where T : DbContext, IDisposable
    {
        private readonly IMediatorHandler _mediatorHandler;
        protected readonly T Context;

        public Repository(IMediatorHandler mediatorHandler,
                          T context)
        {
            _mediatorHandler = mediatorHandler;
            Context = context;
        }

        public async Task<bool> Commit()
        {
            await Context.SaveChangesAsync();
            await _mediatorHandler.PublishEvents(Context);

            return true;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}