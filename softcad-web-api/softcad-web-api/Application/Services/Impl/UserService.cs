using AutoMapper;
using WebApiOperacaoCuriosidade.Application.Services.Exceptions;
using WebApiOperacaoCuriosidade.Application.Services.Utils;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Enum;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAdministradorRepository _administradorRepository;
        private readonly ILogService _logService;
        private readonly LogConvert _convert;
        private readonly IMapper _mapper;

        public UserService(IUsuarioRepository usuarioRepository, IAdministradorRepository administradorRepository, IMapper mapper, ILogService logService, LogConvert convert)
        {
            _usuarioRepository = usuarioRepository;
            _administradorRepository = administradorRepository;
            _mapper = mapper;
            _logService = logService;
            _convert = convert;
        }

        public bool Insert(UserDTO userDTO, int adminId)
        {
            var userExists = _usuarioRepository.GetAll().Where(u => u.Email.Trim().ToUpper() == userDTO.Email.Trim().ToUpper()).FirstOrDefault();

            if (userExists != null)
            {
                throw new BusinessRuleException(adminId, Operation.CREATION, "Endereço de E-mail já registrado!");
            }

            var newUser = _mapper.Map<User>(userDTO);
            var adminExists = _administradorRepository.GetById(adminId);

            if (adminExists == null)
            {
                throw new BusinessRuleException(adminId, Operation.CREATION, "Administrador não encontrado para o cadastro do usuário!");
            }

            newUser.AdministratorId = adminId;
            newUser.RegistrationDate = DateTime.Now;

            if (!_usuarioRepository.Create(newUser))
            {
                throw new BusinessRuleException(adminId, Operation.CREATION, "Ocorreu um erro inesperado na criação da conta.");
            }

            _logService.RegisterLog(new Logs(adminId, Operation.CREATION, newUser.Id));

            return true;
        }

        public List<UserDTO> GetAll(int adminId)
        {
            var list = _mapper.Map<List<UserDTO>>(_usuarioRepository.GetAll());

            return list;
        }

        public UserDTO GetById(int userId, int adminId)
        {
            var user = _mapper.Map<UserDTO>(_usuarioRepository.GetById(userId));

            if (user == null)
            {
                throw new BusinessRuleException(adminId, Operation.CONSULT, "Usuario não encontrado.");
            }

            _logService.RegisterLog(new Logs(adminId, Operation.CONSULT, user.Id));

            return user;
        }

        public bool Delete(int userId, int adminId)
        {
            var userExists = _usuarioRepository.GetById(userId);

            if (userExists == null)
            {
                throw new BusinessRuleException(adminId, Operation.ELIMINATION, "Usuario não encontrado.");
            }

            if (!_usuarioRepository.Delete(userExists.Id))
            {
                throw new BusinessRuleException(adminId, Operation.ELIMINATION, "Ocorreu um erro inesperado na remoção da conta.");
            }

            _logService.RegisterLog(new Logs(adminId, Operation.ELIMINATION));

            return true;
        }

        public bool Update(UserDTO dto, int adminId)
        {
            var userExists = _usuarioRepository.GetById(dto.Id);

            if (userExists == null)
            {
                throw new BusinessRuleException(adminId, Operation.CHANGE, "Usuario não encontrado.");
            }

            string beforeData = _convert.MessageLogUpdateUser(userExists);

            var user = _mapper.Map<User>(dto);
            user.LastModification = DateTime.Now;
            user.AdministratorId = adminId;

            if (!_usuarioRepository.Update(user))
            {
                throw new BusinessRuleException(adminId, Operation.CHANGE, "Ocorreu um erro inesperado na atualização da conta.");
            }

            string lastData = _convert.MessageLogUpdateUser(user);

            _logService.RegisterLog(new Logs(adminId, Operation.CHANGE, user.Id, beforeData, lastData));

            return true;
        }

        public List<User> GetUsersByAdmin(int adminId)
        {
            var adminExists = _administradorRepository.GetById(adminId);

            if (adminExists == null)
            {
                throw new BusinessRuleException(adminId, Operation.CONSULT, "Administrador não encontrado.");
            }

            var usuarios = _usuarioRepository.GetAll().Where(u => u.AdministratorId == adminExists.Id).ToList();

            _logService.RegisterLog(new Logs(adminId, Operation.CONSULT));

            return usuarios;
        }

    }
}
