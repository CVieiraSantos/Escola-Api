using Escola.Application.DTOs.Usuario;
using Escola.Application.Interfaces;
using Escola.Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthenticate _authenticate;

        public UsuarioController(IUsuarioService usuarioService, IAuthenticate authenticate)
        {
            _usuarioService = usuarioService;
            _authenticate = authenticate;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UsuarioPostDTO usuarioPostDTO)
        {
            UsuarioGetDTO usuario = await _usuarioService.AddAsync(usuarioPostDTO);
            var token = _authenticate.GenerateToken(usuario.Id, usuario.Email.ToLower(), usuario.Perfil);
            return Ok(new { Nome = usuario.Nome, Token = token });
        }
    }
}
