using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext context;
        public EventoPersist(ProEventosContext context)    
        {
            this.context = context;
            // this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = this.context.Eventos
                .AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
                if(includePalestrantes)
                {
                    query = query.Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.OrderBy(e => e.Id);
                return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
               IQueryable<Evento> query = this.context.Eventos
                .AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
                if(includePalestrantes)
                {
                    query = query.Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                .OrderBy(e => e.Id);
                return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = this.context.Eventos
                .AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
                if(includePalestrantes)
                {
                    query = query.Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }
                query = query.OrderBy(e => e.Id)
                    .Where(e => e.Id == eventoId);
                return await query.FirstOrDefaultAsync();
        }
    }
}