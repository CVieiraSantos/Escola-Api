using Escola.Application.DTOs.Matricula;
using Escola.Application.DTOs.Turma;
using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;

namespace Escola.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public async Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matriculaPostDTO)
        {
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
                return null;
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
                return null;
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
