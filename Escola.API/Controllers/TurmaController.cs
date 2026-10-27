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
            return Ok(turma);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTurma(TurmaPutDTO turmaPutDTO)
        {
            var turma = await _turmaService.UpdateAsync(turmaPutDTO);
            //return Ok(new { Message = "Turma atualizada com sucesso." });
            return Ok(turma);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTurma(int id)
        {
            var turma = await _turmaService.DeleteAsync(id);
            return Ok(new { message = "Turma deletada com sucesso." });

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetByIdAsync(id);
            return Ok(turma);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllAsync();
            return Ok(turmas);
        }
    }
}
