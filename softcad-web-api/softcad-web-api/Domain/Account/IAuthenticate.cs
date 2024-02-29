namespace WebApiOperacaoCuriosidade.Domain.Account
{
    public interface IAuthenticate
    {
        bool Authenticate(string email, string senha);
        bool AdminExists(string email);
        string GenerateToken(int id, string email);
    }
}
