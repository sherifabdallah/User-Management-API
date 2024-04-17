using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace API.Data
{
    class DataContextDapper
    {
        public string connectionString = "Server=SherifAbdullah\\MSSQLSERVER01;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool Execute(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection.Execute(sql);
        }
    }
}