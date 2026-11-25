using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Escola.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateTurma(TurmaPostDTO turmaPostDTO)
        {
            var turma = await _turmaService.AddAsync(turmaPostDTO);
            return Ok(turma);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> UpdateTurma(TurmaPutDTO turmaPutDTO)
        {
            var turma = await _turmaService.UpdateAsync(turmaPutDTO);
            //return Ok(new { Message = "Turma atualizada com sucesso." });
            return Ok(turma);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteTurma(int id)
        {
            var turma = await _turmaService.DeleteAsync(id);
            return Ok(new { message = "Turma deletada com sucesso." });

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetByIdAsync(id);
            return Ok(turma);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllAsync();
            return Ok(turmas);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Administrador, Aluno")] // Permite acesso a usuários com os perfis "Administrador", "Aluno"
        public async Task<ActionResult> GetAllTurmasByUsuario()
        {
            var userId = User.GetUserId(); // Obtém o ID do usuário autenticado a partir do token JWT
            var turmas = await _turmaService.GetTurmasByUsuario(userId);
            return Ok(turmas);
        }

        [HttpGet("UserAdmin")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> GetAllTurmasByAdmin()
        {
            var turmas = await _turmaService.GetAllAsync();
            return Ok(turmas);
        }
    }
}
