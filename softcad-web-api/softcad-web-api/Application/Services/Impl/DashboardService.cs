using AutoMapper;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Application.Services.Impl
{
    public class DashboardService: IDashboardService
    {
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository, IMapper mapper)
        {
            _repository =  repository;
        }

        public int AmountUsersByMonth(int adminId)
        {
            DateTime date = DateTime.Now;
            int mesAtual = date.Month;

            int amount = _repository.AmountUsersByMonth(mesAtual - 1, adminId);

            return amount;
        }

        public int AmountUsersByStatus(int adminId)
        {
            var amount = _repository.AmountUsersByStatus(adminId);

            return amount;
        }

        public int AmountUsersByAdmin(int adminId)
        {
            var amount = _repository.AmountUsersByAdmin(adminId);

            return amount;
        }
    }
}
