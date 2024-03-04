using Microsoft.AspNetCore.JsonPatch;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Administrator Insert(AdminDTO adminDTO);
        List<Administrator> GetAll();
        public Administrator GetById(int id);
        public bool Delete(int id);
        public bool Update(int id, AdminDTO dto);
        public bool UpdatePartial(int id, JsonPatchDocument<AdminDTO> admin);
        public Administrator Authenticator(Credentials credentials);
        public byte[]? DownloadPhoto(int id);
    }
}
