using WebApiOperacaoCuriosidade.Domain.Models;
using System.Data.SqlClient;
using Dapper;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class LogRepository: ILogRepository
    {
        private readonly string _connectionString;

        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void RegisterLog(Logs log)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parametros = new
                {
                    log.ExecutorId,
                    log.Operation,
                    log.Timestamp,
                    log.AffectedUser,
                    log.Result,
                    log.Message,
                    log.PreviousState,
                    log.CurrentState
                };

                string sql = @"
                    INSERT INTO Logs 
                    (ExecutorId, Operation, Timestamp, AffectedUser, Result, Message, PreviousState, CurrentState) 
                    VALUES 
                    (@ExecutorId, @Operation, @Timestamp, @AffectedUser, @Result, @Message, @PreviousState, @CurrentState);
                    SELECT CAST(scope_identity() AS INT)
                ";

                sqlConnection.Execute(sql, parametros);

            }

        }
        public List<Logs> GetLogs(int id)
        {

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Logs WHERE ExecutorId = @Id";
                var list = sqlConnection.Query<Logs>(sql, new { id }).OrderByDescending(x => x.Timestamp).ToList();

                return list;
            }
            
        }
  
    }
        
}
