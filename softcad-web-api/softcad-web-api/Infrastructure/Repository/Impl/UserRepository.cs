using Dapper;
using System.Data.SqlClient;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class UserRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<User> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Users";

                var list = sqlConnection.Query<User>(sql).ToList();

                return list;
            }
        }

        public User GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Users WHERE Id = @Id";

                var user = sqlConnection.QueryFirstOrDefault<User>(sql, new { id });

                return user;
            }
        }

        public bool Create(User user)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    user.Name,
                    user.Age,
                    user.Email,
                    user.Address,
                    user.Information,
                    user.Interests,
                    user.Feelings,
                    user.Principles,
                    user.Status,
                    user.RegistrationDate,
                    user.AdministratorId
                };

                string sql = @"
                    INSERT INTO Users
                    (
                        Name, 
                        Age, 
                        Email, 
                        Address, 
                        Information, 
                        Interests, 
                        Feelings, 
                        Principles, 
                        RegistrationDate, 
                        Status, 
                        AdministratorId
                    ) 
                    VALUES 
                    (
                        @Name, 
                        @Age, 
                        @Email, 
                        @Address, 
                        @Information, 
                        @Interests, 
                        @Feelings, 
                        @Principles, 
                        @RegistrationDate, 
                        @Status, 
                        @AdministratorId
                    )
                ";

                var affectedRows = sqlConnection.Execute(sql, parameters);
                if (affectedRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Delete(int? id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Users WHERE Id = @Id";
                var affectedRows = sqlConnection.Execute(sql, new { id });

                if (affectedRows == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Update(User user)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    user.Name,
                    user.Age,
                    user.Email,
                    user.Address,
                    user.Information,
                    user.Interests,
                    user.Feelings,
                    user.Principles,
                    user.Status,
                    user.LastModification
                };

                const string sql = @"
                    UPDATE Usuarios
                    SET 
                        Nome = @Nome, 
                        Email = @Email, 
                        Idade = @Idade, 
                        Endereco = @Endereco, 
                        Informacoes = @Informacoes, 
                        Interesses = @Interesses, 
                        Sentimentos = @Sentimentos, 
                        Valores = @Valores,
                        UltimaModificacao = @UltimaModificacao
                    WHERE 
                        Id = @Id
                ";

                var affectedRows = sqlConnection.Execute(sql, parameters);
                if (affectedRows == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
