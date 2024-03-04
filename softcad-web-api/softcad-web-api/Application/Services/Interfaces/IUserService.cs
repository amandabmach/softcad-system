using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Services
{
    public interface IUserService
    {
        List<UserDTO> GetAll(int adminId);
        UserDTO GetById(int id, int adminId);
        List<User> GetUsersByAdmin(int adminId);
        bool Insert(UserDTO usuarioDTO, int adminId);
        bool Delete(int id, int adminId);
        bool Update(UserDTO dto, int adminId);
       
    }
}
