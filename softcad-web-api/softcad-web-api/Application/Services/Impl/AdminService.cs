using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Cryptography;
using System.Text;
using WebApiOperacaoCuriosidade.Application.Services.Exceptions;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IAdministradorRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdministradorRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public Administrador Insert(AdminDTO adminDTO)
        {
            var adminExists = _adminRepository.GetAll().Where(a => a.Email.Trim().ToUpper() == adminDTO.Email.Trim().ToUpper()).FirstOrDefault();
            if (adminExists != null)
                throw new RegraNegocioException("Administrador já registrado!");

            var administrador = _mapper.Map<Administrador>(adminDTO);

            if (adminDTO.Senha != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminDTO.Senha));
                byte[] passwordSalt = hmac.Key;

                administrador.AlterarSenha(passwordHash, passwordSalt);
            }
            administrador.DataCadastro = DateTime.Now;

            var adminCadastrado = _adminRepository.Create(administrador);
            if (adminCadastrado == null)
                throw new RegraNegocioException("Erro na criação do Administrador!");

            return adminCadastrado;
        }

        public List<Administrador> GetAll()
        {
            var listAdmins = _adminRepository.GetAll().ToList();
            return listAdmins;
        }

        public Administrador GetById(int id)
        {
            var administrador = _adminRepository.GetById(id);
            if (administrador == null)
                throw new RegraNegocioException("Administrador não encontrado");

            return administrador;
        }

        public bool Delete(int id)
        {
            var administrador = _adminRepository.GetById(id);
            if (administrador == null)
                throw new RegraNegocioException("Administrador não encontrado");

            if (!_adminRepository.Delete(id))
                return false;

            return true;
        }

        public bool Update(int id, AdminDTO dto)
        {
            var administrador = _adminRepository.GetById(id);
            if (administrador == null)
                throw new RegraNegocioException("Administrador não encontrado.");

            var admin = _mapper.Map<Administrador>(dto);

            admin.UltimaModificacao = DateTime.Now;

            if (dto.Senha != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha));
                byte[] passwordSalt = hmac.Key;

                admin.AlterarSenha(passwordHash, passwordSalt);
            }

            if (!_adminRepository.Update(admin))
                return false;

            return true;

        }

        public bool UpdatePartial(int id, JsonPatchDocument<AdminDTO> admin)
        {
            var adminExists = _adminRepository.GetById(id);
            if (adminExists == null)
                throw new RegraNegocioException("Administrador não encontrado.");

            var adminToPatch = _mapper.Map<AdminDTO>(adminExists);
            admin.ApplyTo(adminToPatch);

            _mapper.Map(adminToPatch, adminExists);
            adminExists.UltimaModificacao = DateTime.Now;

            if (!_adminRepository.Update(adminExists))
                return false;

            return true;
        }

        public Administrador Authenticator(Login login)
        {
            var administrador = _adminRepository.GetAll().FirstOrDefault(a => a.Email == login.Email);

            if (administrador == null)
                throw new RegraNegocioException("Administrador não encontrado.");

            using (var hmac = new HMACSHA512(administrador.PasswordSalt))
            {
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Senha));

                if (!passwordHash.SequenceEqual(administrador.PasswordHash))
                    throw new RegraNegocioException("Senha incorreta!");
            }
            return administrador;
        }

        public byte[]? DownloadPhoto(int id)
        {
            var admin = _adminRepository.GetById(id);
            var fotoPath = admin.Foto;

            if (File.Exists(fotoPath))
            {
                var imageBytes = File.ReadAllBytes(fotoPath);
                return imageBytes;
            }
            return null;
        }

    }
}
