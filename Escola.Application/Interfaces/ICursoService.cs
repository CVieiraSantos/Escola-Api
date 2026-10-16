using Escola.Application.DTOs.Curso;

namespace Escola.Application.Interfaces
{
    public interface ICursoService
    {
        Task<CursoGetDTO> GetByIdAsync(int id);
        Task<List<CursoGetDTO>> GetAllAsync();
        Task<CursoGetDTO> AddAsync(CursoPostDTO curso);
        Task<CursoGetDTO> UpdateAsync(CursoPutDTO curso);
        Task<CursoGetDTO> DeleteAsync(int id);
    }
}
