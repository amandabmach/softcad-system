namespace WebApiOperacaoCuriosidade.Domain.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string? Informacoes { get; set; }
        public string Interesses { get; set; }
        public string Sentimentos { get; set; }
        public string Valores { get; set; }
        public bool Status { get; set; }
        public int AdministradorId { get; set; }

    }
}
