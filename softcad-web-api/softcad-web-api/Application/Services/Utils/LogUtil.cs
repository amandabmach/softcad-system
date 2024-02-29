namespace WebApiOperacaoCuriosidade.Application.Services.Utils
{
    public class LogUtil
    {
        public int? Id { get; set; }
        public string? ExecutorEmail { get; set; }
        public string? Operation { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? AffectedUser { get; set; }
        public string? Result { get; set; }
        public string? Message { get; set; }
        public string? PreviousState { get; set; }
        public string? CurrentState { get; set; }
    }
}
