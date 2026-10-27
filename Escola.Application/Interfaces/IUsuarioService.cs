using Escola.Application.DTOs.Usuario;
using Escola.Domain.Entities;

namespace Escola.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioGetDTO> GetByIdAsync(int id);
        Task<List<UsuarioGetDTO>> GetAllAsync();
        Task<UsuarioGetDTO> AddAsync(UsuarioPostDTO usuarioPostDTO);
        Task<UsuarioGetDTO> UpdateAsync(int usuarioId, UsuarioPutDTO usuarioPutDTO);
        Task<UsuarioGetDTO> DeleteAsync(int id);
    }
}
