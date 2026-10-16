using Escola.Application.DTOs.Curso;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCursos()
        {
            var cursos = await _cursoService.GetAllAsync();

            if(cursos is null || cursos.Count == 0)
            {
                return NotFound("Nenhum curso encontrado.");
            }
            return Ok(cursos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCursoById(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso is null)
            {
                return NotFound("Curso não encontrado.");
            }

            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCurso(CursoPostDTO cursoPostDTO)
        {
            var curso = await _cursoService.AddAsync(cursoPostDTO);
            if (curso is null)
            {
                return BadRequest("Não foi possível criar o curso.");
            }
            return Ok(curso);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCurso(CursoPutDTO cursoPutDTO)
        {
            var curso = await _cursoService.UpdateAsync(cursoPutDTO);
            if (curso is null)
            {
                return NotFound("Curso não encontrado.");
            }
            return Ok(curso);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCurso(int id)
        {
            var curso = await _cursoService.DeleteAsync(id);
            if (curso is null)
            {
                return NotFound("Curso não encontrado.");
            }
            return Ok(new { Message = "Curso deletado com sucesso." });
        }
    }
}
