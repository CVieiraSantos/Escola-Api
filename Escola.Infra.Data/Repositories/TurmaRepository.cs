using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly Context _context;

        public TurmaRepository(Context context)
        {
            _context = context;
        }

        public async Task<Turma> AddAsync(Turma turma)
        {
            await _context.Turmas.AddAsync(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<Turma> DeleteAsync(int id)
        {
            var turma = await _context.Turmas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (turma == null)
            {
                throw new Exception("Turma não encontrada");
            }

            turma.Excluido = true;
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<List<Turma>> GetAllAsync()
        {
            var turma =  await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Curso)
                .Where(x => x.Excluido == false)
                .ToListAsync();   
            return turma;
        }

        public async Task<Turma> GetByIdAsync(int id)
        {
            var turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Curso)
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();

            if (turma == null)
            {
                throw new Exception("Turma não encontrada");
            }

            return turma;
        }

        public async Task<Turma> UpdateAsync(Turma turma)
        {
            var turmaExistente = await _context.Turmas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == turma.Id);

            if (turmaExistente == null)
            {
                throw new Exception("Turma não encontrada");
            }

            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
            return turmaExistente;
        }
    }
}
