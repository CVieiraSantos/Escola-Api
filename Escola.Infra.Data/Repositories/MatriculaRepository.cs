using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly Context _context;
        public async Task<Matricula> AddAsync(Matricula matricula)
        {
             _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<Matricula> DeleteAsync(int id)
        {
            var matricula = await _context.Matriculas
                .AsNoTracking()
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();

            if (matricula == null)
            {
                return null;
            }

            matricula.Excluido = true;
            _context.Matriculas.Update(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<List<Matricula>> GetAllAsync()
        {
            var matriculas = await _context.Matriculas
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.Turma)
                .Where(x => x.Excluido == false)
                .ToListAsync();

            return matriculas;
        }

        public async Task<Matricula> GetByIdAsync(int id)
        {
            var matricula = await _context.Matriculas
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.Turma)
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();

            if (matricula == null)
            {
                throw new Exception("Matrícula não encontrada");
            }

            return matricula;
        }

        public async Task<Matricula> UpdateAsync(Matricula matricula)
        {
            var matriculaExistente = _context.Matriculas
                .AsNoTracking()
                .FirstOrDefault(x=> x.Id == matricula.Id);
            
            if(matricula == null)
            {
                throw new Exception("Matrícula não encontrada");
            }

            _context.Matriculas.Update(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }
    }
}
