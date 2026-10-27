using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Escola.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuarioPostDTO)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(usuarioPostDTO.Senha));
            byte[] passwordSalt = hmac.Key;

            var existeUsuario = await _usuarioRepository.ExistsUserAsync();

            var usuario = new Usuario
            {
                Nome = usuarioPostDTO.Nome,
                Email = usuarioPostDTO.Email,
                Excluido = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Perfil = existeUsuario ? "Aluno" : "Administrador"
            };

            var createdUsuario = await _usuarioRepository.AddAsync(usuario);
            return new UsuarioGetDTO
            {
                Id = createdUsuario.Id,
                Nome = createdUsuario.Nome,
                Email = createdUsuario.Email,
                Perfil = createdUsuario.Perfil
            };
        }

        public async Task<UsuarioGetDTO> DeleteAsync(int id)
        {
            var usuarioDeletado = await _usuarioRepository.DeleteAsync(id);

            if(usuarioDeletado == null)
            {
                return null;
            }

            return new UsuarioGetDTO
            {
                Id = usuarioDeletado.Id,
                Nome = usuarioDeletado.Nome,
                Email = usuarioDeletado.Email,
                Perfil = usuarioDeletado.Perfil
            };
        }

        public async Task<bool> ExistsUserAsync()
        {
            return await _usuarioRepository.ExistsUserAsync();
        }

        public async Task<List<UsuarioGetDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuariosGetDTO = new List<UsuarioGetDTO>();

            if (usuarios.Count == 0)
            {
                return usuariosGetDTO;
            }

            usuariosGetDTO.AddRange(usuarios.Select(u => new UsuarioGetDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }));
            
            return usuariosGetDTO;
        }

        public async Task<UsuarioGetDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if(usuario == null)
            {
                return null;
            }

            return new UsuarioGetDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil
            };
        }

        public async Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuarioPutDTO)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);

            if(usuario == null)
            {
                return null;
            }

            usuario.Id = usuarioId;
            usuario.Nome = usuarioPutDTO.Nome;
            usuario.Email = usuarioPutDTO.Email;

            var usuarioAtualizado = await _usuarioRepository.UpdateAsync(usuario);

            return new UsuarioGetDTO
            {
                Id = usuarioAtualizado.Id,
                Nome = usuarioAtualizado.Nome,
                Email = usuarioAtualizado.Email,
                Perfil = usuarioAtualizado.Perfil
            };
        }
    }
}
