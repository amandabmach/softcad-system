using WebApiOperacaoCuriosidade.Domain.Enum;

namespace WebApiOperacaoCuriosidade.Domain.Models
{
    public class Logs
    {
        public int? Id { get; set; }
        public int? ExecutorId { get; set; }
        public Operacao? Operation { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? AffectedUser { get; set; }
        public Result? Result { get; set; }
        public string Message { get; set; }
        public string? PreviousState { get; set; }
        public string? CurrentState { get; set; }

        public Logs() { }


        public Logs(int? executorId, Operacao? operation)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            Result = Enum.Result.Sucesso;
        }
        public Logs(int? executorId, Operacao? operation, int? affectedUser)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            AffectedUser = affectedUser;
            Result = Enum.Result.Sucesso;
        }
        public Logs(int? executorId, Operacao? operation, int? affectedUser, string? previousState, string? currentState)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = DateTime.Now;
            AffectedUser = affectedUser;
            PreviousState = previousState;
            CurrentState = currentState;
            Result = Enum.Result.Sucesso;
        }

        //ErrorController
        public Logs(int? executorId, Operacao? operation, DateTime? timestamp, string message, Result? result)
        {
            ExecutorId = executorId;
            Operation = operation;
            Timestamp = timestamp;
            Message = message;
            Result = result;
        }
    }
}
