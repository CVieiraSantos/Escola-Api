using Escola.Application.DTOs.Matricula;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : Controller
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMatriculas()
        {
            var matriculas = await _matriculaService.GetAllAsync();
            if (matriculas is null || matriculas.Count == 0)
            {
                return NotFound("Nenhuma matrícula encontrada.");
            }
            return Ok(matriculas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetMatriculaById(int id)
        {
            var matricula = await _matriculaService.GetByIdAsync(id);
            if (matricula is null)
            {
                return NotFound("Matrícula não encontrada.");
            }
            return Ok(matricula);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMatricula(MatriculaPostDTO matriculaPostDTO)
        {
            var matricula = await _matriculaService.AddAsync(matriculaPostDTO);
            if (matricula is null)
            {
                return BadRequest("Não foi possível criar a matrícula.");
            }
            return Ok(new {message = "Matrícula incluída com sucesso."});
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMatricula(MatriculaPutDTO matriculaPutDTO)
        {
            var matricula = await _matriculaService.UpdateAsync(matriculaPutDTO);
            if (matricula is null)
            {
                return NotFound("Matrícula não encontrada.");
            }
            return Ok(new {message = "Matrícula alterada com sucesso."});
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMatricula(int id)
        {
            var matricula = await _matriculaService.DeleteAsync(id);
            if (matricula is null)
            {
                return NotFound("Matrícula não encontrada.");
            }
            return Ok(new { Message = "Matrícula excluída com sucesso." });
        }
    }
}
