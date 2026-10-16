using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTurma(TurmaPostDTO turmaPostDTO)
        {
            var turma = await _turmaService.AddAsync(turmaPostDTO);

            if (turma == null)
            {
                return BadRequest("Não foi possível criar a turma.");
            }
            //return Ok(new { message = "Turma incluida com sucesso." });
            return Ok(turma);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTurma(TurmaPutDTO turmaPutDTO)
        {
            var turma = await _turmaService.UpdateAsync(turmaPutDTO);
            
            if (turma == null)
            {
                return BadRequest("Não foi possível atualizar a turma.");
            }
            return Ok(new { Message = "Turma atualizada com sucesso." });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTurma(int id)
        {
            var turma = await _turmaService.DeleteAsync(id);

            if (turma is null)
            {
                return NotFound("Turma não encontrada.");
            }
            return Ok(new { Message = "Turma deletada com sucesso." });

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetByIdAsync(id);
            if (turma is null)
            {
                return NotFound("Turma não encontrada.");
            }

            return Ok(turma);
            //return Ok(new { Message = "Turma encontrada com sucesso." });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllAsync();
            if (turmas is null || turmas.Count == 0)
            {
                return NotFound("Nenhuma turma encontrada.");
            }
            return Ok(turmas);
        }
    }
}
