using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly ProEventosContext context;
        public LotePersist(ProEventosContext context)    
        {
            this.context = context;
            // this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = this.context.Lotes;
            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId && lote.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
           IQueryable<Lote> query = this.context.Lotes;
            query = query.AsNoTracking()
                        .Where(lote => lote.EventoId == eventoId)
                        .OrderBy(lote => lote.Id);
            return await query.ToArrayAsync();
        }
    }
}