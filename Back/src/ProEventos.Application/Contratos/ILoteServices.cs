using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface ILoteServices
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
        Task<bool> DeleteLote(int eventoId, int loteId);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);
    }   
}