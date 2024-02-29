using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Services
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> GetAll(int adminId);
        UsuarioDTO GetById(int id, int adminId);
        List<Usuario> GetUsersByAdmin(int adminId);
        bool Insert(UsuarioDTO usuarioDTO, int adminId);
        bool Delete(int id, int adminId);
        bool Update(UsuarioDTO dto, int adminId);
       
    }
}
