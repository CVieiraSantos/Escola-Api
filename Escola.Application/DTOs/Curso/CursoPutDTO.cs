using System.ComponentModel.DataAnnotations;

namespace Escola.Application.DTOs.Curso
{
    public class CursoPutDTO
    {
        [Required(ErrorMessage = "O identificador do curso é obrigatório.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Nome deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo Descricao deve ter no máximo 150 caracteres.")]
        public string Descricao { get; set; }
    }
}
