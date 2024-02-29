using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario GetById(int id);
        List<Usuario> GetAll();
        bool Create(Usuario usuario);
        bool Update(Usuario usuario);
        bool Delete(int? id);
    }
}
