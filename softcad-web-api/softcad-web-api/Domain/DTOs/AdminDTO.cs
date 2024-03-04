namespace WebApiOperacaoCuriosidade.Domain.DTOs
{
    public class AdminDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
