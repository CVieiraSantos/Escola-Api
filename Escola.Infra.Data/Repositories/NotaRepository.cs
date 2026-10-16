using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly Context _context;
        public async Task<Nota> AddAsync(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        public async Task<Nota> DeleteAsync(int id)
        {
            var nota = _context.Notas
                .AsNoTracking()
                .FirstOrDefault(x=> x.Excluido == false && x.Id == id);
            
            if(nota == null)
            {
                throw new Exception("Nota não encontrada");
            }

            nota.Excluido = true;
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        public async Task<List<Nota>> GetAllAsync()
        {
            return await _context.Notas
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .ToListAsync();
        }

        public async Task<Nota> GetByIdAsync(int id)
        {
            var nota = await _context.Notas
                .AsNoTracking()
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();

            if (nota == null)
            {
                throw new Exception("Nota não encontrada");
            }

            return nota;
        }

        public async Task<Nota> UpdateAsync(Nota nota)
        {
            var notaExistente = await _context.Notas
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == nota.Id);

            if (notaExistente == null)
            {
                throw new Exception("Nota não encontrada");
            }

            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
            return nota;
        }
    }
}
