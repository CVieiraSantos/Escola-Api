namespace Escola.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Perfil { get; set; }
        public ICollection<Matricula> Matriculas { get; set; }
        public bool Excluido { get; set; }
    }
}
