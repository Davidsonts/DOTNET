using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// // Retorna um lote por eventoId e id
        /// </summary>
        /// <param name="eventoId"></param>
        /// <param name="id">id da tabela Lote</param>
        /// <returns>1 Lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}