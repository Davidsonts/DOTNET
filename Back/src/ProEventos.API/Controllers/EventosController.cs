﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

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
                if(eventos == null) return NoContent();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDto>> GetById(int id)
        {   
            try
            {
                var evento = await this.eventoServices.GetEventoByIdAsync(id, true);
                if(evento == null)  return NoContent();
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
                if(evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {   
            try
            {
                var evento = await this.eventoServices.AddEvento(model);
                if(evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {   
            try
            {
                var evento = await this.eventoServices.UpdateEvento(id, model);
                if(evento == null) return NoContent();
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
                var eventos = await this.eventoServices.GetAllEventosAsync(true);
                if(eventos == null) return NoContent();

                return await this.eventoServices.DeleteEvento(id) ? Ok(new { message = "Deletado" }) : BadRequest("Evento não deletado.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
