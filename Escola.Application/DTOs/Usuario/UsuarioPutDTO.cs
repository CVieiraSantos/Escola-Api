using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Usuario
{
    public class UsuarioPutDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MaxLength(250, ErrorMessage = "O campo Nome deve ter no máximo 250 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [MaxLength(250, ErrorMessage = "O campo Email deve ter no máximo 250 caracteres")]
        [EmailAddress(ErrorMessage = "O E-mail é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O senha é obrigatória")]
        [MinLength(8, ErrorMessage = "O senha deve ter no mínimo 8 caracteres")]
        [MaxLength(250, ErrorMessage = "O senha deve ter no máximo 250 caracteres")]
        public string Senha { get; set; }
    }
}
