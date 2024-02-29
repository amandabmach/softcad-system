using WebApiOperacaoCuriosidade.Application.Services.Utils;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Services
{
    public interface ILogService
    {
        public void RegisterLog(Logs log);
        public List<LogUtil> GetLogs(int admin);
        
    }
}
