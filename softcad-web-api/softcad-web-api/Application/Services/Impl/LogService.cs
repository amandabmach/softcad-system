using WebApiOperacaoCuriosidade.Application.Services.Utils;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class LogService: ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly LogConvert _convert;

        public LogService(ILogRepository logRepository, LogConvert convert)
        {
            _logRepository = logRepository;
            _convert = convert;
        }
      
        public void RegisterLog(Logs logs)
        {
            _logRepository.RegisterLog(logs);
        }

        public List<LogUtil> GetLogs(int admin)
        {

            var list = _logRepository.GetLogs(admin);

            var newlist = _convert.ConvertLogs(list);

            return newlist;

        }
    }
}