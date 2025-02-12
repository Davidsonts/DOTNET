using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[] {
                new Evento() {
                    EventoId = 1,
                    tema = "NET % Angular",
                    Local = "SP",
                    Lote = "1º Lote",
                    DataEvento =  DateTime.Now.AddDays(2).ToString("dd/MM/YYYY"),
                    QtdPessoas = 250,
                    ImageUrl = "img.png",
                },
                 new Evento() {
                    EventoId = 2,
                    tema = "Angular",
                    Local = "RJ",
                    Lote = "2º Lote",
                    DataEvento =  DateTime.Now.AddDays(2).ToString("dd/MM/YYYY"),
                    QtdPessoas = 300,
                    ImageUrl = "img.png",
                },
            };
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // private readonly ILogger<EventoController> _logger;

        // public EventoController(ILogger<EventoController> logger)
        // {
        //     _logger = logger;
        // }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {   
            return _evento;
        }
        
        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {   
            return _evento.Where(evento => evento.EventoId == id);
        }
    }
}
