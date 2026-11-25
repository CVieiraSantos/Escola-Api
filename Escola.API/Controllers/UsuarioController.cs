using Escola.API.Models;
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
            var usuarioExistente = await _authenticate.UserExists(usuarioPostDTO.Email);
            if(usuarioExistente)
            {
                return BadRequest(new { message = "Já existe um usuário com este email" });
            }
            UsuarioGetDTO usuario = await _usuarioService.AddAsync(usuarioPostDTO);
            var token = _authenticate.GenerateToken(usuario.Id, usuario.Email.ToLower(), usuario.Perfil);
            return Ok(new { Nome = usuario.Nome, Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenUsuario(UserLogin userLogin)
        {
            var usuario = await _authenticate.GetUsuarioByEmail(userLogin.Email);
            if(usuario == null)
            {
                return BadRequest(new { message = "Usuário ou senha inválidos" });
            }

           var usuarioValido =  await _authenticate.AuthenticateAsync(userLogin.Email, userLogin.Senha);
           
            if(!usuarioValido)
            {
                return BadRequest(new { message = "Usuário ou senha inválidos" });
            }

            var token = _authenticate.GenerateToken(usuario.Id, usuario.Email.ToLower(), usuario.Perfil);
            return Ok(new { Nome = usuario.Nome, Token = token });
        }
    }
}
