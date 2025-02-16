using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventosServices : IEventoServices
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventoPersist eventoPersist;
        public EventosServices(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this.eventoPersist = eventoPersist;
            this.geralPersist = geralPersist;
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                geralPersist.Add<Evento>(model);
                if (await geralPersist.SaveChangesAsync())
                {
                    return await this.eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await this.eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");
                geralPersist.Delete<Evento>(evento);
                return await geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await this.eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para update não encontrado.");
                model.Id = evento.Id;
                geralPersist.Update<Evento>(model);
                if (await geralPersist.SaveChangesAsync())
                {
                    return await this.eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}