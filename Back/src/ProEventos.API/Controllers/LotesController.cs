using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        // private readonly ILogger<LoteController> _logger;
        private readonly ILoteServices loteServices;

        public LotesController(ILoteServices  loteServices)
        {
            this.loteServices = loteServices;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetByEventoId(int eventoId)
        {   
            try
            {
                var lotes = await this.loteServices.GetLotesByEventoIdAsync(eventoId);
                if(lotes == null) return NoContent();
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
        {   
            try
            {
                var lote = await this.loteServices.SaveLotes(eventoId, models);
                if(lote == null) return NoContent();
                return Ok(lote);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {   
            try
            {
                var lote = await this.loteServices.GetLoteByIdsAsync(eventoId, loteId);
                if(lote == null) return NoContent();

                return await this.loteServices.DeleteLote(lote.EventoId, lote.Id) 
                            ? Ok(new { message = "Lote Deletado" }) 
                            : BadRequest("Ocorreu um problema não esperado ao tentar deletar o lote");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lotes. Erro: {ex.Message}");
            }
        }
    }
}
