using WebApiOperacaoCuriosidade.Domain.Enum;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Utils
{
    public class LogConvert
    {
        private readonly IAdministradorRepository _adminRepository;
        private readonly IUsuarioRepository _userRepository;

        public LogConvert() { }

        public LogConvert(IAdministradorRepository administradorRepository, IUsuarioRepository usuarioRepository)
        {
            _adminRepository = administradorRepository;
            _userRepository = usuarioRepository;
        }

        public List<LogUtil> ConvertLogs(List<Logs> logs)
        {
            List<LogUtil> newList = new List<LogUtil>();

            foreach (Logs log in logs)
            {
                LogUtil newLog = new LogUtil();
                newLog.ExecutorEmail = log.ExecutorId.HasValue
                    ? _adminRepository.GetById((int)log.ExecutorId)?.Email
                    : "Não informado";
                newLog.AffectedUser = log.AffectedUser.HasValue
                    ? _userRepository.GetById((int)log.AffectedUser)?.Nome
                    : "Não informado";

                newLog.Operation = log.Operation.HasValue
                    ? Enum.GetName(typeof(Operacao), log.Operation)
                    : "Não informado";

                newLog.Result = log.Result == 0 ? "FALHOU" : "SUCESSO";

                newLog.Message = log.Message == null ? "Mensagem disponível em casos de erro." : log.Message;
                newLog.Timestamp = log.Timestamp;
                newLog.CurrentState = log.CurrentState == null ? "Mensagem disponível em casos de atualização." : log.CurrentState;
                newLog.PreviousState = log.PreviousState == null ? "Mensagem disponível em casos de atualização." : log.PreviousState;
                newLog.Id = log.Id;

                newList.Add(newLog);

            }
            return newList;
        }

        public string MessageLogUpdate(Usuario user)
        {
            return "Id: " + user.Id +"\n"+ "Nome: " + user.Nome + "\n" + "Email: " + user.Email + "\n" + "Idade: " + user.Idade + "\n" +
                    "Endereço: " + user.Endereco + "\n" + "Status: " + user.Status + "\n" + "Sentimentos: " + user.Sentimentos + "\n" +
                    "Valores: " + user.Valores + "\n" + "Interesses: " + user.Interesses + "\n" + "Data de Cadastro: " + user.DataCadastro + "\n" +
                    "Ultima Modificação: " + user.UltimaModificacao + "\n" + "AdministradorID: " + user.AdministradorId;
        }
    }
}
