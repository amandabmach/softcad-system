using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiOperacaoCuriosidade.Domain.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set;}
        public string Email { get; set;}
        public string Endereco { get; set;}
        public string? Informacoes { get; set;}
        public string Interesses { get; set;}
        public string Sentimentos { get; set;}
        public string Valores { get; set;}
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimaModificacao { get; set; }
        public int AdministradorId { get; set; }

    }
}
