using Escola.Domain.Entities;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Context _context;

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(x => x.Excluido == false && x.Id == id)
                .FirstOrDefaultAsync();
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            usuario.Excluido = true;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .ToListAsync();
            
            return usuarios;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Excluido)
                .FirstOrDefaultAsync();
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            return usuario;
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == usuario.Id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
