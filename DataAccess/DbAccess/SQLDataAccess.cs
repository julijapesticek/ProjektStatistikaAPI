using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;
using Dapper;
using System.Data;

namespace DataAccess.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;

        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadAll<T>(string sqlString, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            return await connection.QueryAsync<T>(sqlString, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> LoadOne<T, U>(string sqlString, U parameter, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            return await connection.QueryAsync<T>(sqlString, parameter, commandType: CommandType.Text);
        }

        public async Task SaveOne<T>(string sqlString, T parameter, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            await connection.ExecuteAsync(sqlString, parameter, commandType: CommandType.Text);
        }

    }
}
