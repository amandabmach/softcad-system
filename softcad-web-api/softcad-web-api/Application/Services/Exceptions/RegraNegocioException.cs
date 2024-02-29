using WebApiOperacaoCuriosidade.Domain.Enum;
namespace WebApiOperacaoCuriosidade.Application.Services.Exceptions
{
    public class RegraNegocioException : Exception
    {
        public int? ExecutorId { get; set; }
        public Operacao? Operation { get; set; }
        public DateTime? Timestamp { get; set; }
        public Result Result { get; set; }
        public int? AffectedUser { get; set; }
        public RegraNegocioException() : base() { }

        public RegraNegocioException(string message) : base(message) { }

        public RegraNegocioException(string message, Exception innerException) : base(message, innerException) { }

        public RegraNegocioException(int? executorId, Operacao? operation, string message) : base(message)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            Result = Result.Falhou;
        }
        public RegraNegocioException(int? executorId, Operacao? operation, string message, int affectedUser) : base(message)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            Result = Result.Falhou;
            AffectedUser = affectedUser;
        }
    }
}
