using Escola.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Matricula
{
    public class MatriculaPutDTO
    {
        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "A turma é obrigatória.")]
        public int TurmaId { get; set; }
        [Required(ErrorMessage = "A data de expiração é obrigatória.")]
        public DateTime DataExpiracao { get; set; }
    }
}
