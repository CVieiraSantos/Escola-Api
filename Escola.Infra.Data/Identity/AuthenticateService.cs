using Escola.Domain.Account;
using Escola.Domain.Entities;
using Escola.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Escola.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios
             .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Excluido == false);
            
            if(usuario == null || usuario.Excluido)
            {
                return false;
            }

            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computerdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for (int i = 0; i < computerdHash.Length; i++)
            {
                if (computerdHash[i] != usuario.PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public string GenerateToken(int id, string email, string role)
        {
            var clains = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email.ToLower()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: clains,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
             return await _context.Usuarios
             .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Excluido == false);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Usuarios
             .AnyAsync(u => u.Email.ToLower() == email.ToLower() && u.Excluido == false);
        }
    }
}
