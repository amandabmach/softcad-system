using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface IAdministradorRepository
    {
        Administrador GetById(int id);
        List<Administrador> GetAll();
        Administrador Create(Administrador administrador);
        bool Update(Administrador administrador);
        bool Delete(int id);
    }
}
