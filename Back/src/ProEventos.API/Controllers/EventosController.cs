using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        // private readonly ILogger<EventoController> _logger;
        private readonly IEventoServices eventoServices;

        public EventosController(IEventoServices  eventoServices)
        {
            this.eventoServices = eventoServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            try
            {
                var eventos = await this.eventoServices.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrado.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById(int id)
        {   
            try
            {
                var evento = await this.eventoServices.GetEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Nenhum evento encontrado.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {   
            try
            {
                var evento = await this.eventoServices.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) return NotFound("Nenhum evento por tema não encontrado.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {   
            try
            {
                var evento = await this.eventoServices.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {   
            try
            {
                var evento = await this.eventoServices.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar alterar evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {   
            try
            {
                return await this.eventoServices.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evento não deletado.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
