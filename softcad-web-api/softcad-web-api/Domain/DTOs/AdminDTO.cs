namespace WebApiOperacaoCuriosidade.Domain.DTOs
{
    public class AdminDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public IFormFile? Foto { get; set; }

    }
}
