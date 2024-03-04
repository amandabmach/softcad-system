using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface IAdministradorRepository
    {
        Administrator GetById(int id);
        List<Administrator> GetAll();
        Administrator Create(Administrator administrator);
        bool Update(Administrator administrator);
        bool Delete(int id);
    }
}
