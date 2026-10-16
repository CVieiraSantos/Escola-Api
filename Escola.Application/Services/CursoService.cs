using Escola.Application.DTOs.Curso;
using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

namespace Escola.Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<CursoGetDTO> AddAsync(CursoPostDTO cursoPostDto)
        {
            var curso = new Curso
            {
                Nome = cursoPostDto.Nome,
                Descricao = cursoPostDto.Descricao
            };

            var createdCurso = await _cursoRepository.AddAsync(curso);
            return new CursoGetDTO
            {
                Id = createdCurso.Id,
                Nome = createdCurso.Nome,
                Descricao = createdCurso.Descricao
            };
        }

        public async Task<CursoGetDTO> DeleteAsync(int id)
        {
            var deleteCurso = await _cursoRepository.DeleteAsync(id);

            if(deleteCurso == null)
            {
                return null;
            }

            return new CursoGetDTO
            {
                Id = deleteCurso.Id,
                Nome = deleteCurso.Nome,
                Descricao = deleteCurso.Descricao
            };
        }

        public async Task<List<CursoGetDTO>> GetAllAsync()
        {
            var cursos = await _cursoRepository.GetAllAsync();
            var cursosDto = new List<CursoGetDTO>();

            if(cursos.Count == 0)
            {
                return cursosDto;
            }

            cursosDto.AddRange(cursos.Select(c => new CursoGetDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
            }));
            return cursosDto;
        }

        public async Task<CursoGetDTO> GetByIdAsync(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);

            if(curso == null)
            {
                return null;
            }

            return new CursoGetDTO
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao
            };
        }

        public async Task<CursoGetDTO> UpdateAsync(CursoPutDTO curso)
        {
            var cursoToUpdate = new Curso
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao
            };

            if(cursoToUpdate == null)
            {
                return null;
            }

            return new CursoGetDTO
            {
                Id = cursoToUpdate.Id,
                Nome = cursoToUpdate.Nome,
                Descricao = cursoToUpdate.Descricao
            };
        }
    }
}
