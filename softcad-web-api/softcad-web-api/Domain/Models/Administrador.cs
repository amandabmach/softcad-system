using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOperacaoCuriosidade.Domain.Models
{
    [Table("Administradores")]
    public class Administrador
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? Foto { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimaModificacao { get; set; }
        public List<Usuario>? Usuarios { get; set; } = new List<Usuario> ();


        public void AlterarSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
