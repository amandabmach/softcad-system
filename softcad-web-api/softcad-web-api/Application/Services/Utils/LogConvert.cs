using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Enum;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Utils
{
    public class LogConvert
    {
        private readonly IAdministradorRepository _adminRepository;
        private readonly IUsuarioRepository _userRepository;

        public LogConvert(IAdministradorRepository adminRepository, IUsuarioRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public List<LogUtil> ConvertLogs(List<Logs> logs)
        {
            List<LogUtil> newList = new List<LogUtil>();

            foreach (Logs log in logs)
            {
                LogUtil newLog = new LogUtil();

                if (log.ExecutorId.HasValue)
                {
                    string? emailAdmin = _adminRepository.GetById((int)log.ExecutorId)?.Email;
                    string? emailUser = _userRepository.GetById((int)log.ExecutorId)?.Email;

                    if (emailAdmin != null)
                    {
                        newLog.ExecutorEmail = emailAdmin;

                    } else if (emailUser != null)
                    {
                        newLog.ExecutorEmail = emailUser;
                    } else
                    {
                        newLog.ExecutorEmail = "Não informado";
                    }
                }
         
                newLog.AffectedUser = log.AffectedUser.HasValue
                    ? _userRepository.GetById((int)log.AffectedUser)?.Name
                    : "Não informado";

                newLog.Operation = log.Operation.HasValue
                    ? Enum.GetName(typeof(Operation), log.Operation)
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

        public string MessageLogUpdateUser(User user)
        {
            return "Id: " + user.Id + "\n" +
                   "Name: " + user.Name + "\n" +
                   "Email: " + user.Email + "\n" +
                   "Age: " + user.Age + "\n" +
                   "Address: " + user.Address + "\n" +
                   "Status: " + user.Status + "\n" +
                   "Feelings: " + user.Feelings + "\n" +
                   "Values: " + user.Principles + "\n" +
                   "Interests: " + user.Interests + "\n" +
                   "Registration Date: " + user.RegistrationDate + "\n" +
                   "Last Modification: " + user.LastModification + "\n" +
                   "Administrator ID: " + user.AdministratorId;
        }

        public string MessageLogUpdateAdmin(AdminDTO admin)
        {
            return "Id: " + admin.Id + "\n" +
                   "Name: " + admin.Name + "\n" +
                   "Email: " + admin.Email + "\n" +
                   "Photo: " + admin.Photo;
        }
    }
}
