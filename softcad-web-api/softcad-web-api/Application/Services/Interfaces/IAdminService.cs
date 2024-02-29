using Microsoft.AspNetCore.JsonPatch;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Administrador Insert(AdminDTO adminDTO);
        List<Administrador> GetAll();
        public Administrador GetById(int id);
        public bool Delete(int id);
        public bool Update(int id, AdminDTO dto);
        public bool UpdatePartial(int id, JsonPatchDocument<AdminDTO> admin);
        public Administrador Authenticator(Login login);
        public byte[]? DownloadPhoto(int id);
    }
}
