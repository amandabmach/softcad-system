﻿using System.Data.SqlClient;
using Dapper;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Repository.Impl
{
    public class DashboardRepository : IDashboardRepository 
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int AmountUsersByStatus(int adminId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(*) FROM Users WHERE Status = 0 AND AdministratorId = @adminId";

                var amount = sqlConnection.Query<int>(sql, new { adminId }).Single();

                return amount;
            }
        }

        public int AmountUsersByMonth(int month, int adminId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(*) FROM Users WHERE MONTH(RegistrationDate) = @month AND AdministratorId = @adminId";

                var amount = sqlConnection.Query<int>(sql, new { month, adminId }).Single();

                return amount;

            }
        }
        public int AmountUsersByAdmin(int adminId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(*) FROM Users WHERE AdministratorId = @adminId";

                var amount = sqlConnection.Query<int>(sql, new { adminId }).Single();

                return amount;

            }
        }

    }
}
