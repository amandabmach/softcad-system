using Dapper;
using System.Data.SqlClient;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class AdministratorRepository : IAdministradorRepository
    {
        private readonly string _connectionString;

        public AdministratorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Administrator> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Administrators";
                var listAdmins = sqlConnection.Query<Administrator>(sql).ToList();

                return listAdmins;
            }

        }
        public Administrator GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Administrators WHERE Id = @Id";
                var administrator = sqlConnection.QueryFirstOrDefault<Administrator>(sql, new { id });

                if (administrator == null)
                {
                    return null;
                }

                const string consultUsers = @"
                    SELECT Users.* 
                    FROM Users
                    JOIN Administrators ON users.AdministratorId = administrators.Id 
                    WHERE Administrators.Id = @Id;
                ";

                var listUsers = sqlConnection.Query<User>(consultUsers, new { id }).ToList();

                administrator.Users.AddRange(listUsers);

                return administrator;
            }
        }

        public Administrator Create(Administrator admin)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    admin.Name,
                    admin.Email,
                    admin.PasswordHash,
                    admin.PasswordSalt,
                    admin.RegistrationDate,
                    admin.Photo
                };

                string sql = @"
                    INSERT INTO Administrators
                    (Name, Email, PasswordHash, PasswordSalt, RegistrationDate, Photo) 
                    VALUES 
                    (@Name, @Email, @PasswordHash, @PasswordSalt, @RegistrationDate, @Photo);
                    SELECT CAST(scope_identity() AS INT)
                ";
                var id = sqlConnection.ExecuteScalar<int>(sql, parameters);

                if (id == 0)
                {
                    return null;
                }

                var adminExists = GetById(id);

                return adminExists;
            }
        }

        public bool Update(Administrator administrador)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    administrador.Id,
                    administrador.Name,
                    administrador.Email,
                    administrador.PasswordHash,
                    administrador.PasswordSalt,
                    administrador.RegistrationDate,
                    administrador.LastModification,
                    administrador.Photo
                };

                const string sql = @"
                    UPDATE Administrators 
                    SET Name = @Name, 
                        Email = @Email, 
                        PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        LastModification = @LastModification,
                        Photo = @Photo
                    WHERE Id = @Id";

                var affectedRows = sqlConnection.Execute(sql, parameters);

                if (affectedRows == 0)
                {
                    return false;
                }
                return true;
            }

        }

        public bool Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Administrators WHERE Id = @Id";

                var affectedRows = sqlConnection.Execute(sql, new { id });

                if (affectedRows == 0)
                {
                    return false;
                }

                return true;
            }
        }

    }
}
