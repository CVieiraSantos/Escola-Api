using Escola.Application.DTOs.Matricula;
using Escola.Application.DTOs.Turma;
using Escola.Application.DTOs.Usuario;
using Escola.Application.Exceptions;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

namespace Escola.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITurmaRepository _turmaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, IUsuarioRepository usuarioRepository, ITurmaRepository turmaRepository)
        {
            _matriculaRepository = matriculaRepository;
            _usuarioRepository = usuarioRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matriculaPostDTO)
        {
            if(await _usuarioRepository.GetByIdAsync(matriculaPostDTO.UsuarioId) == null)
                throw new NotFoundException($"Usuário com ID {matriculaPostDTO.UsuarioId} não encontrado.");
            if(await _turmaRepository.GetByIdAsync(matriculaPostDTO.TurmaId) == null)
                throw new NotFoundException($"Turma com ID {matriculaPostDTO.TurmaId} não encontrada.");

            var matricula = new Matricula
            {
                UsuarioId = matriculaPostDTO.UsuarioId,
                TurmaId = matriculaPostDTO.TurmaId,
                DataMatricula = DateTime.UtcNow,
                DataExpiracao = matriculaPostDTO.DataExpiracao,
                Ativa = true
            };
            
            var createdMatricula = await _matriculaRepository.AddAsync(matricula);
            return new MatriculaGetDTO
            {
                Id = createdMatricula.Id,
                UsuarioId = createdMatricula.UsuarioId,
                TurmaId = createdMatricula.TurmaId,
                DataMatricula = createdMatricula.DataMatricula,
                DataExpiracao = createdMatricula.DataExpiracao,
                Ativa = createdMatricula.Ativa
            };
        }

        public async Task<MatriculaGetDTO> DeleteAsync(int id)
        {
            var deleteMatricula = await _matriculaRepository.DeleteAsync(id);

            if(deleteMatricula == null)
            {
                throw new NotFoundException($"Matrícula com ID {id} não encontrada.");
            }
            
            return new MatriculaGetDTO
            {
                Id = deleteMatricula.Id,
                UsuarioId = deleteMatricula.UsuarioId,
                TurmaId = deleteMatricula.TurmaId,
                DataMatricula = deleteMatricula.DataMatricula,
                DataExpiracao = deleteMatricula.DataExpiracao,
                Ativa = deleteMatricula.Ativa
            };
        }

        public async Task<List<MatriculaGetDetailDTO>> GetAllAsync()
        {
            var matriculas = await _matriculaRepository.GetAllAsync();
            var matriculaGetDetailDTOs = new List<MatriculaGetDetailDTO>();

            matriculaGetDetailDTOs.AddRange(matriculas.Select(matricula => new MatriculaGetDetailDTO
            {
                Id = matricula.Id,
                DataMatricula = matricula.DataMatricula,
                DataExpiracao = matricula.DataExpiracao,
                Ativa = matricula.Ativa,
                
                Usuario = new UsuarioGetDTO
                {
                    Id = matricula.Usuario.Id,
                    Nome = matricula.Usuario.Nome,
                    Email = matricula.Usuario.Email
                },

                Turma = new TurmaGetDTO
                {
                    Id = matricula.Turma.Id,
                    Nome = matricula.Turma.Nome,
                    Descricao = matricula.Turma.Descricao
                }
            }));
            return matriculaGetDetailDTOs;
        }

        public async Task<MatriculaGetDetailDTO> GetByIdAsync(int id)
        {
            var matricula = await _matriculaRepository.GetByIdAsync(id);
            
            if (matricula == null)
            {
                throw new NotFoundException($"Matrícula com ID {id} não encontrada.");
            }

            return new MatriculaGetDetailDTO
            {
                Id = matricula.Id,
                DataMatricula = matricula.DataMatricula,
                DataExpiracao = matricula.DataExpiracao,
                Ativa = matricula.Ativa,
                
                Usuario = new UsuarioGetDTO
                {
                    Id = matricula.Usuario.Id,
                    Nome = matricula.Usuario.Nome,
                    Email = matricula.Usuario.Email
                },
                Turma = new TurmaGetDTO
                {
                    Id = matricula.Turma.Id,
                    Nome = matricula.Turma.Nome,
                    Descricao = matricula.Turma.Descricao
                }
            };
        }

        public async Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matriculaPutDTO)
        {
           if(await _turmaRepository.GetByIdAsync(matriculaPutDTO.TurmaId) == null)
                throw new NotFoundException($"Turma com ID {matriculaPutDTO.TurmaId} não encontrada.");
            if (await _matriculaRepository.GetByIdAsync(matriculaPutDTO.Id) == null)
                throw new NotFoundException($"Matrícula com ID {matriculaPutDTO.Id} não encontrada.");

            var matricula = new Matricula
            {
                Id = matriculaPutDTO.Id,
                TurmaId = matriculaPutDTO.TurmaId,
                DataExpiracao = matriculaPutDTO.DataExpiracao
            };

            var matriculaAlterada = await _matriculaRepository.UpdateAsync(matricula);

            if (matriculaAlterada == null)
            {
                return null;
            }

            return new MatriculaGetDTO
            {
                Id = matriculaAlterada.Id,
                UsuarioId = matriculaAlterada.UsuarioId,
                TurmaId = matriculaAlterada.TurmaId,
                DataMatricula = matriculaAlterada.DataMatricula,
                DataExpiracao = matriculaAlterada.DataExpiracao,
                Ativa = matriculaAlterada.Ativa
            };
        }
    }
}
