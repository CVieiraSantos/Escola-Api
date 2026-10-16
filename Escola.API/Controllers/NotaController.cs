using Escola.Application.DTOs.Nota;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : Controller
    {
        public readonly INotaService _notaService;

        public NotaController(INotaService notaService)
        {
            _notaService = notaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllNotas()
        {
            var notas = await _notaService.GetAllAsync();
            if (notas is null || notas.Count == 0)
            {
                return NotFound("Nenhuma nota encontrada.");
            }
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetNotaById(int id)
        {
            var nota = await _notaService.GetByIdAsync(id);
            if (nota is null)
            {
                return NotFound("Nota não encontrada.");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNota(NotaPostDTO notaPostDTO)
        {
            var nota = await _notaService.AddAsync(notaPostDTO);
            if (nota is null)
            {
                return BadRequest("Não foi possível criar a nota.");
            }
            return Ok(new { message = "Nota incluída com sucesso." });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNota(NotaPutDTO notaPutDTO)
        {
            var nota = await _notaService.UpdateAsync(notaPutDTO);
            if (nota is null)
            {
                return NotFound("Nota não encontrada.");
            }
            return Ok(new { message = "Nota atualizada com sucesso." });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteNota(int id)
        {
            var nota = await _notaService.DeleteAsync(id);
            if (nota is null)
            {
                return NotFound("Nota não encontrada.");
            }
            return Ok(new { message = "Nota deletada com sucesso." });
        }
    }
}
