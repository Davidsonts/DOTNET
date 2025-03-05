using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class LoteServices : ILoteServices
    {
        private readonly IGeralPersist geralPersist;
        private readonly ILotePersist lotePersist;
        private readonly IMapper maper;
        public LoteServices(IGeralPersist geralPersist, 
                                ILotePersist lotePersist,
                                IMapper maper)
        {
            this.lotePersist = lotePersist;
            this.geralPersist = geralPersist;
            this.maper = maper;
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = this.maper.Map<Lote>(model);

                this.geralPersist.Add<Lote>(lote);

                await this.geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await this.lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if(model.Id == 0)
                    {
                        await this.AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;
                        this.maper.Map(model, lote);
                        this.geralPersist.Update<Lote>(lote);
                        await this.geralPersist.SaveChangesAsync();
                    }
                }

                var lotesRetorno = await this.lotePersist.GetLotesByEventoIdAsync(eventoId);
                return this.maper.Map<LoteDto[]>(lotesRetorno);

              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await this.lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não encontrado.");
                this.geralPersist.Delete<Lote>(lote);
                return await this.geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await this.lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;
                
                var resultado = this.maper.Map<LoteDto[]>(lotes);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await this.lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = this.maper.Map<LoteDto>(lote);
                return resultado;
                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}