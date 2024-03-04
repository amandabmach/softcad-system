using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        User GetById(int id);
        List<User> GetAll();
        bool Create(User user);
        bool Update(User user);
        bool Delete(int? id);
    }
}
