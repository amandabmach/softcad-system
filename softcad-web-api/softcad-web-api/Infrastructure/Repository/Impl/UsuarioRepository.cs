using Dapper;
using System.Data.SqlClient;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Usuario> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Usuarios";
                var list = sqlConnection.Query<Usuario>(sql).ToList();

                return list;
            }
        }

        public Usuario GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Usuarios WHERE Id = @Id";
                var user = sqlConnection.QueryFirstOrDefault<Usuario>(sql, new { id });

                return user;
            }
        }

        public bool Create(Usuario usuario)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parametros = new
                {
                    usuario.Nome,
                    usuario.Idade,
                    usuario.Email,
                    usuario.Endereco,
                    usuario.Informacoes,
                    usuario.Interesses,
                    usuario.Sentimentos,
                    usuario.Valores,
                    usuario.Status,
                    usuario.DataCadastro,
                    usuario.AdministradorId
                };

                string sql = @"
                    INSERT INTO Usuarios 
                    (
                        Nome, 
                        Idade, 
                        Email, 
                        Endereco, 
                        Informacoes, 
                        Interesses, 
                        Sentimentos, 
                        Valores, 
                        DataCadastro, 
                        Status, 
                        AdministradorId
                    ) 
                    VALUES 
                    (
                        @Nome, 
                        @Idade, 
                        @Email, 
                        @Endereco, 
                        @Informacoes, 
                        @Interesses, 
                        @Sentimentos, 
                        @Valores, 
                        @DataCadastro, 
                        @Status, 
                        @AdministradorId
                    )
                ";

                var linhasAfetadas = sqlConnection.Execute(sql, parametros);
                if (linhasAfetadas == 0)
                    return false;

                return true;
            }
        }

        public bool Delete(int? id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Usuarios WHERE Id = @Id";
                var linhasAfetadas = sqlConnection.Execute(sql, new { id });

                if (linhasAfetadas == 0)
                    return false;

                return true;
            }
        }

        public bool Update(Usuario usuario)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parametros = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Idade,
                    usuario.Endereco,
                    usuario.Informacoes,
                    usuario.Interesses,
                    usuario.Sentimentos,
                    usuario.Valores,
                    usuario.Status,
                    usuario.UltimaModificacao
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

                var linhasAfetadas = sqlConnection.Execute(sql, parametros);
                if (linhasAfetadas == 0)
                    return false;

                return true;
            }
        }
    }
}
