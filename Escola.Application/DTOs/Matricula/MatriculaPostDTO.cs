using Escola.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Matricula
{
    public class MatriculaPostDTO
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "A turma é obrigatória.")]
        public int TurmaId { get; set; }
        [Required(ErrorMessage = "A data de expiração é obrigatória.")]
        public DateTime DataExpiracao { get; set; }
    }
}
