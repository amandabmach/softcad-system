using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiOperacaoCuriosidade.Application.Services.Exceptions;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Application.Services.Utils;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Enum;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAdministradorRepository _administradorRepository;
        private readonly ILogService _logService;
        private readonly LogConvert _convert;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IAdministradorRepository administradorRepository, IMapper mapper, ILogService logService, LogConvert convert)
        {
            _usuarioRepository = usuarioRepository;
            _administradorRepository = administradorRepository;
            _mapper = mapper;
            _logService = logService;
            _convert = convert;
        }

        public bool Insert(UsuarioDTO usuarioDTO, int adminId)
        {
            var userExists = _usuarioRepository.GetAll().Where(u => u.Email.Trim().ToUpper() == usuarioDTO.Email.Trim().ToUpper()).FirstOrDefault();

            if (userExists != null)
                throw new RegraNegocioException(adminId, Operacao.CRIAÇÃO, "Endereço de E-mail já registrado!");

            var newUser = _mapper.Map<Usuario>(usuarioDTO);
            var administrador = _administradorRepository.GetById(adminId);

            if (administrador == null)
                throw new RegraNegocioException(adminId, Operacao.CRIAÇÃO, "Administrador não encontrado para o cadastro do usuário!");

            newUser.AdministradorId = adminId;
            newUser.DataCadastro = DateTime.Now;
            if (!_usuarioRepository.Create(newUser))
                throw new RegraNegocioException(adminId, Operacao.CRIAÇÃO, "Ocorreu um erro inesperado na criação da conta.");
                    
            _logService.RegisterLog(new Logs(adminId, Operacao.CRIAÇÃO, newUser.Id));
            return true;
        }

        public List<UsuarioDTO> GetAll(int adminId)
        {
            var list = _mapper.Map<List<UsuarioDTO>>(_usuarioRepository.GetAll());
            return list;
        }

        public UsuarioDTO GetById(int userId, int adminId)
        {
            var usuario = _mapper.Map<UsuarioDTO>(_usuarioRepository.GetById(userId));

            if (usuario == null)
                throw new RegraNegocioException(adminId, Operacao.CONSULTA, "Usuario não encontrado.");

            _logService.RegisterLog(new Logs(adminId, Operacao.CONSULTA, usuario.Id));
            return usuario;
        }

        public bool Delete(int userId, int adminId)
        {
            var usuario = _usuarioRepository.GetById(userId);

            if (usuario == null)
                throw new RegraNegocioException(adminId, Operacao.REMOÇÃO, "Usuario não encontrado.");

            if (!_usuarioRepository.Delete(usuario.Id))
                throw new RegraNegocioException(adminId, Operacao.REMOÇÃO, "Ocorreu um erro inesperado na remoção da conta.");

            _logService.RegisterLog(new Logs(adminId, Operacao.REMOÇÃO));
            return true;
        }

        public bool Update(UsuarioDTO dto, int adminId)
        {
            var userExists = _usuarioRepository.GetById(dto.Id);

            if (userExists == null)
                throw new RegraNegocioException(adminId, Operacao.ALTERAÇÃO, "Usuario não encontrado.");

            string dadosAntes = _convert.MessageLogUpdate(userExists);

            var user = _mapper.Map<Usuario>(dto);
            user.UltimaModificacao = DateTime.Now;
            user.AdministradorId = adminId;

            if (!_usuarioRepository.Update(user))
                throw new RegraNegocioException(adminId, Operacao.ALTERAÇÃO, "Ocorreu um erro inesperado na atualização da conta.");

            string dadosDepois = _convert.MessageLogUpdate(user);

            _logService.RegisterLog(new Logs(adminId, Operacao.ALTERAÇÃO, user.Id, dadosAntes, dadosDepois));
            return true;
        }

        public List<Usuario> GetUsersByAdmin(int adminId)
        {
            var administrador = _administradorRepository.GetById(adminId);
            if (administrador == null)
                throw new RegraNegocioException(adminId, Operacao.CONSULTA, "Administrador não encontrado.");

            var usuarios = _usuarioRepository.GetAll().Where(u => u.AdministradorId == administrador.Id).ToList();

            _logService.RegisterLog(new Logs(adminId, Operacao.CONSULTA));
            return usuarios;
        }

    }
}
