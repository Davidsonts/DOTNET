using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment hostEnvironment;

        public EventosController(IEventoServices  eventoServices, IWebHostEnvironment hostEnvironment)
        {
            this.eventoServices = eventoServices;
            this.hostEnvironment = hostEnvironment;
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

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {   
            try
            {
                var evento = await this.eventoServices.GetEventoByIdAsync(eventoId, true);
                if(evento == null) return NoContent();
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if(file.Length > 0)
                {
                    DeleteImage(evento.ImageUrl);
                    evento.ImageUrl = await SaveImage(file);
                }
                var eventoRetorno = await this.eventoServices.UpdateEvento(eventoId, evento);
                return Ok(eventoRetorno);
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
                var evento = await this.eventoServices.GetEventoByIdAsync(id, true);
                if(evento == null) return NoContent();

                if(await this.eventoServices.DeleteEvento(id)){
                    DeleteImage(evento?.ImageUrl);
                    return Ok(new { message = "Deletado" });
                } else {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar Evento.");
                } 
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile image)
        {
            var imageName = new String(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = $"{imageName}{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(image.FileName)}";
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var path = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
