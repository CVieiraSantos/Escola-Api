using Escola.Application.DTOs.Curso;
using Escola.Application.DTOs.Turma;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

namespace Escola.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly ICursoRepository _cursoRepository;

        public TurmaService(ITurmaRepository turmaRepository, ICursoRepository cursoRepository)
        {
            _turmaRepository = turmaRepository ?? throw new ArgumentNullException(nameof(turmaRepository));
            _cursoRepository = cursoRepository ?? throw new ArgumentNullException(nameof(cursoRepository));
        }

        public async Task<TurmaGetDTO> AddAsync(TurmaPostDTO turmaPostDTO)
        {
            var curso = await _cursoRepository.GetByIdAsync(turmaPostDTO.CursoId);

            if(curso == null)
            {
                throw new ArgumentException($"Curso com ID {turmaPostDTO.CursoId} não encontrado.");
            }

            var turma = new Turma
            {
                Nome = turmaPostDTO.Nome,
                CursoId = turmaPostDTO.CursoId,
                Descricao = turmaPostDTO.Descricao,
            };

            var turmaAdicionada = await _turmaRepository.AddAsync(turma);

            return new TurmaGetDTO
            {
                Id = turmaAdicionada.Id,
                Nome = turmaAdicionada.Nome,
                CursoId = turmaAdicionada.CursoId,
                Descricao = turmaAdicionada.Descricao
            };
        }

        public async Task<TurmaGetDTO> DeleteAsync(int id)
        {
            var turmaDeletada = await _turmaRepository.DeleteAsync(id);

            if(turmaDeletada == null)
            {
                return null;
            }

            return new TurmaGetDTO
            {
                Id = turmaDeletada.Id,
                Nome = turmaDeletada.Nome,
                CursoId = turmaDeletada.CursoId
            };
        }

        public async Task<List<TurmaGetDetailDTO>> GetAllAsync()
        {
            var turmas = await _turmaRepository.GetAllAsync();
            var turmaGetDetailDTOs = new List<TurmaGetDetailDTO>();

            if (turmas.Count == 0)
            {
                return turmaGetDetailDTOs;
            }

            turmaGetDetailDTOs.AddRange(turmas.Select(turma => new TurmaGetDetailDTO
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Descricao = turma.Descricao,
                
                Curso = new CursoGetDTO
                {
                    Id = turma.Curso.Id,
                    Nome = turma.Curso.Nome,
                    Descricao = turma.Curso.Descricao
                }
            }));

            return turmaGetDetailDTOs;
        }

        public async Task<TurmaGetDetailDTO> GetByIdAsync(int id)
        {
            var turmas = await _turmaRepository.GetByIdAsync(id);

            if(turmas == null)
            {
                return null;
            }

            return new TurmaGetDetailDTO
            {
                Id = turmas.Id,
                Nome = turmas.Nome,
                Descricao = turmas.Descricao,
                Curso = new CursoGetDTO
                {
                    Id = turmas.Curso.Id,
                    Nome = turmas.Curso.Nome,
                    Descricao = turmas.Curso.Descricao
                }
            };
        }

        public async Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turmaPutDTO)
        {
            var turma = new Turma
            {
                Id = turmaPutDTO.Id,
                Nome = turmaPutDTO.Nome,
                CursoId = turmaPutDTO.CursoId
            };

            var turmaAtualizada = await _turmaRepository.UpdateAsync(turma);

            return new TurmaGetDTO
            {
                Id = turmaAtualizada.Id,
                Nome = turmaAtualizada.Nome,
                CursoId = turmaAtualizada.CursoId
            };
        }
    }
}
