using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Nota
{
    public class NotaPostDTO
    {
        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        public int MatriculaId { get; set; }
        [Required(ErrorMessage = "O campo valor da nota é obrigatório.")]
        [Range(0, 100, ErrorMessage = "O valor da nota deve estar entre 0 e 100.")]
        public int ValorNota { get; set; }
    }
}
