using WebApiOperacaoCuriosidade.Domain.Enum;
namespace WebApiOperacaoCuriosidade.Application.Services.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public int? ExecutorId { get; set; }
        public Operation? Operation { get; set; }
        public DateTime? Timestamp { get; set; }
        public Result Result { get; set; }
        public int? AffectedUser { get; set; }
        public BusinessRuleException() : base() { }

        public BusinessRuleException(string message) : base(message) { }

        public BusinessRuleException(string message, Exception innerException) : base(message, innerException) { }

        public BusinessRuleException(int? executorId, Operation? operation, string message) : base(message)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            Result = Result.FAILED;
        }
        public BusinessRuleException(int? executorId, Operation? operation, string message, int affectedUser) : base(message)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            Result = Result.FAILED;
            AffectedUser = affectedUser;
        }
    }
}
