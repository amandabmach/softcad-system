using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface ILogRepository
    {
        public void RegisterLog(Logs log);
        public List<Logs> GetLogs(int admin);
    }
}
