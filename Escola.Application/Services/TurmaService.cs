using Escola.Application.DTOs.Curso;
using Escola.Application.DTOs.Turma;
using Escola.Application.Exceptions;
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
                throw new NotFoundException($"Curso com ID {turmaPostDTO.CursoId} não encontrado.");
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
                throw new NotFoundException($"Turma com ID {id} não encontrada.");
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

            if (turmas.Count == 0 || turmas == null)
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
            var turma = await _turmaRepository.GetByIdAsync(id);

            if(turma == null)
            {
                throw new NotFoundException($"Turma com ID {id} não encontrada.");
            }

            return new TurmaGetDetailDTO
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
            };
        }

        public async Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turmaPutDTO)
        {
            var turmaExistente = await _turmaRepository.GetByIdAsync(turmaPutDTO.Id);
            if (turmaExistente == null)
            {
                throw new NotFoundException($"Turma com ID {turmaPutDTO.Id} não encontrada.");
            }

            var curso = await _cursoRepository.GetByIdAsync(turmaPutDTO.CursoId);
            if (curso == null)
            {
                throw new NotFoundException($"Curso com ID {turmaPutDTO.CursoId} não encontrado.");
            }

            turmaExistente.Id = turmaPutDTO.Id;
            turmaExistente.Nome = turmaPutDTO.Nome;
            turmaExistente.Descricao = turmaPutDTO.Descricao;
            turmaExistente.CursoId = turmaPutDTO.CursoId;

            var turmaAtualizada = await _turmaRepository.UpdateAsync(turmaExistente);

            if(turmaAtualizada == null)
            {
                return null;
            }

            return new TurmaGetDTO
            {
                Id = turmaAtualizada.Id,
                Nome = turmaAtualizada.Nome,
                Descricao = turmaAtualizada.Descricao,
                CursoId = turmaAtualizada.CursoId
            };
        }
    }
}
