using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOperacaoCuriosidade.Domain.Models
{
    public class Administrator
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? Photo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastModification { get; set; }
        public List<User>? Users { get; set; } = new List<User> ();

        public void ChangePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
