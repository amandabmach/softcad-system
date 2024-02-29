namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces
{
    public interface IDashboardRepository
    {
        public int AmountUsersByStatus(int adminId);
        public int AmountUsersByMonth(int mes, int adminId);
        public int AmountUsersByAdmin(int adminId);

    }
}
