using Dapper;
using System.Data.SqlClient;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly string _connectionString;

        public AdministradorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Administrador> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Administradores";
                var listAdmins = sqlConnection.Query<Administrador>(sql).ToList();

                return listAdmins;
            }

        }
        public Administrador GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Administradores WHERE Id = @Id";
                var administrador = sqlConnection.QueryFirstOrDefault<Administrador>(sql, new { id });

                if (administrador == null)
                    return null;

                const string consultaUsers = @"
                    SELECT Usuarios.* 
                    FROM Usuarios 
                    JOIN Administradores ON usuarios.AdministradorId = administradores.Id 
                    WHERE Administradores.Id = @Id;
                ";

                var listUsers = sqlConnection.Query<Usuario>(consultaUsers, new { id }).ToList();

                administrador.Usuarios.AddRange(listUsers);

                return administrador;
            }
        }

        public Administrador Create(Administrador administrador)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parametros = new
                {
                    administrador.Nome,
                    administrador.Email,
                    administrador.PasswordHash,
                    administrador.PasswordSalt,
                    administrador.DataCadastro,
                    administrador.Foto
                };

                string sql = @"
                    INSERT INTO Administradores 
                    (Nome, Email, PasswordHash, PasswordSalt, DataCadastro, Foto) 
                    VALUES 
                    (@Nome, @Email, @PasswordHash, @PasswordSalt, @DataCadastro, @Foto);
                    SELECT CAST(scope_identity() AS INT)
                ";
                var id = sqlConnection.ExecuteScalar<int>(sql, parametros);

                if (id == 0)
                    return null;

                Administrador adminExists = GetById(id);
                return adminExists;
            }
        }

        public bool Update(Administrador administrador)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parametros = new
                {
                    administrador.Id,
                    administrador.Nome,
                    administrador.Email,
                    administrador.PasswordHash,
                    administrador.PasswordSalt,
                    administrador.DataCadastro,
                    administrador.UltimaModificacao,
                    administrador.Foto
                };

                const string sql = @"
                    UPDATE Administradores 
                    SET Nome = @Nome, 
                        Email = @Email, 
                        PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        UltimaModificacao = @UltimaModificacao,
                        Foto = @Foto
                    WHERE Id = @Id";

                var linhasAfetadas = sqlConnection.Execute(sql, parametros);
                if (linhasAfetadas == 0)
                    return false;

                return true;
            }

        }

        public bool Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Administradores WHERE Id = @Id";
                var linhasAfetadas = sqlConnection.Execute(sql, new { id });

                if (linhasAfetadas == 0)
                    return false;

                return true;
            }
        }

    }
}
