using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventoServices
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(int eventoId, Evento model);
        Task<bool> DeleteEvento(int eventoId);
        Task<Evento[]> GetAllEventosAsync(bool includeRedesSociais);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
    }
}