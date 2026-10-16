using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly Context _context;

        public CursoRepository(Context context)
        {
            _context = context;
        }

        public async Task<Curso> AddAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso> DeleteAsync(int id)
        {
            var curso = await _context.Cursos
                .AsNoTracking()
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();

            if (curso == null)
            {
                return null;
            }

            curso.Excluido = true;
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            return await _context.Cursos
                .AsNoTracking()
                .Where(x => x.Excluido == false)
                .ToListAsync();
        }

        public async Task<Curso> GetByIdAsync(int id)
        {
            var curso = await _context.Cursos
                .AsNoTracking()
                .Where(x=> x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();
            
            if (curso == null)
            {
                return null;
            }
            
            return curso;
        }

        public async Task<Curso> UpdateAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }
    }
}
