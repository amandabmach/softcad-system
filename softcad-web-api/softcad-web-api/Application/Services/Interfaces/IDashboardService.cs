namespace WebApiOperacaoCuriosidade.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        int AmountUsersByMonth(int adminId);

        int AmountUsersByStatus(int adminId);

        int AmountUsersByAdmin(int adminId);
     
    }
}
