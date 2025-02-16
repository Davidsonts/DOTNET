using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext context;
        public PalestrantePersist(ProEventosContext context)    
        {
            this.context = context;
             
        } 
        
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes
                .AsNoTracking()
                .Include(p => p.RedesSociais);
                if(includeEventos)
                {
                    query = query.Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.OrderBy(p => p.Id);
                return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes
                .AsNoTracking()
                .Include(p => p.RedesSociais);
                if(includeEventos)
                {
                    query = query.Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(p => p.Id);
                return await query.ToArrayAsync();
        }

        public Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes
                .AsNoTracking()
                .Include(p => p.RedesSociais);
                if(includeEventos)
                {
                    query = query.Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }
                query = query.Where(p => p.Id == palestranteId)
                .OrderBy(p => p.Id);
                return query.FirstOrDefaultAsync();
        }
    }
}