using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Security.Cryptography;
using System.Text;
using WebApiOperacaoCuriosidade.Application.Services.Exceptions;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Application.Services.Utils;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Enum;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IAdministradorRepository _repository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly LogConvert _convert;
        public AdminService(IAdministradorRepository repository, IMapper mapper, ILogService logService, LogConvert convert)
        {
            _repository = repository;
            _mapper = mapper;
            _logService = logService;
            _convert = convert;
        }
        public Administrator Insert(AdminDTO adminDTO)
        {
            var adminExists = _repository.GetAll().Where(a => a.Email.Trim().ToUpper() == adminDTO.Email.Trim().ToUpper()).FirstOrDefault();
            
            if (adminExists != null)
            {
                throw new BusinessRuleException("Administrador já registrado!");
            }

            var admin = _mapper.Map<Administrator>(adminDTO);

            if (adminDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDTO.Password));
                byte[] passwordSalt = hmac.Key;

                admin.ChangePassword(passwordHash, passwordSalt);
            }

            admin.RegistrationDate = DateTime.Now;
            var newAdmin = _repository.Create(admin);

            if (newAdmin == null)
            {
                throw new BusinessRuleException("Erro na criação do Administrador!");
            }
            _logService.RegisterLog(new Logs(newAdmin.Id,Operation.CREATION));

            return newAdmin;
        }

        public List<Administrator> GetAll()
        {
            var list = _repository.GetAll().ToList();

            return list;
        }

        public Administrator GetById(int id)
        {
            var admin = _repository.GetById(id);

            if (admin == null)
            {
                throw new BusinessRuleException("Administrador não encontrado");
            }
            _logService.RegisterLog(new Logs(admin.Id, Operation.CONSULT, admin.Id));
            return admin;
        }

        public bool Delete(int id)
        {
            var admin = _repository.GetById(id);

            if (admin == null)
            {
                throw new BusinessRuleException("Administrador não encontrado");
            }

            if (!_repository.Delete(id))
            {
                return false;
            }

            _logService.RegisterLog(new Logs(admin.Id, Operation.ELIMINATION, admin.Id));
            return true;
        }

        public bool Update(int id, AdminDTO adminDTO)
        {
            var adminExists = _repository.GetById(id);

            if (adminExists == null)
            {
                throw new BusinessRuleException("Administrador não encontrado.");
            }

            string beforeData = _convert.MessageLogUpdateAdmin(adminDTO);

            var admin = _mapper.Map<Administrator>(adminDTO);
            admin.LastModification = DateTime.Now;

            if (adminDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDTO.Password));
                byte[] passwordSalt = hmac.Key;

                admin.ChangePassword(passwordHash, passwordSalt);
            }

            if (!_repository.Update(admin))
            {
                return false;
            }
            string lastData = _convert.MessageLogUpdateAdmin(_mapper.Map<AdminDTO>(admin));

            _logService.RegisterLog(new Logs(admin.Id, Operation.CHANGE, admin.Id, beforeData, lastData));
            return true;
        }

        public bool UpdatePartial(int id, JsonPatchDocument<AdminDTO> admin)
        {
            var adminExists = _repository.GetById(id);
            if (adminExists == null)
            {
                throw new BusinessRuleException("Administrador não encontrado.");
            }

            string beforeData = _convert.MessageLogUpdateAdmin(_mapper.Map<AdminDTO>(admin));

            var adminToPatch = _mapper.Map<AdminDTO>(adminExists);
            admin.ApplyTo(adminToPatch);

            _mapper.Map(adminToPatch, adminExists);
            adminExists.LastModification = DateTime.Now;

            if (!_repository.Update(adminExists))
            {
                return false;
            }
            string lastData = _convert.MessageLogUpdateAdmin(_mapper.Map<AdminDTO>(adminExists));

            _logService.RegisterLog(new Logs(adminExists.Id, Operation.CHANGE, adminExists.Id, beforeData, lastData));
            return true;
        }

        public Administrator Authenticator(Credentials credentials)
        {
            var administrador = _repository.GetAll().FirstOrDefault(a => a.Email == credentials.Email);

            if (administrador == null)
            {
                throw new BusinessRuleException("Administrador não encontrado.");
            }

            using (var hmac = new HMACSHA512(administrador.PasswordSalt))
            {
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(credentials.Password));

                if (!passwordHash.SequenceEqual(administrador.PasswordHash))
                {
                    throw new BusinessRuleException("Senha incorreta!");
                }
            }
            return administrador;
        }

        public byte[]? DownloadPhoto(int id)
        {
            var admin = _repository.GetById(id);
            var photoPath = admin.Photo;

            if (File.Exists(photoPath))
            {
                var imageBytes = File.ReadAllBytes(photoPath);
                return imageBytes;
            }

            return null;
        }

    }
}
