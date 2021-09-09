using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Rozklad.Core.Data
{
    public class PostgresDatabase : Database
    {
        public PostgresDatabase(string connectionString, ILogger<PostgresDatabase> logger)
        : base(connectionString, logger)
        {

        }
        
        private void ExecuteSqlCommand(string sql, IDataParameter[] parameters, Action<NpgsqlCommand> callback)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            using var command = new NpgsqlCommand(sql, connection);
            
            if (parameters != null && parameters.Any())
            {
                command.Parameters.AddRange(parameters.Select(o => new NpgsqlParameter(o.ParameterName, o.Value)).ToArray());
            }

            try
            {
                connection.Open();
                callback(command);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error in ExecuteMySqlCommand: {sql}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        
        public override void ExecuteNonQuery(string sql, params IDataParameter[] parameters)
        {
            ExecuteSqlCommand(sql, parameters, command =>
            {
                command.ExecuteNonQuery();
            });
        }

        public override DataTable Execute(string sql, params IDataParameter[] parameters)
        {
            var result = new DataTable();

            ExecuteSqlCommand(sql, parameters, command =>
            {
                using (var adapter = new NpgsqlDataAdapter(command))
                {
                    adapter.Fill(result);
                }
            });

            return result;
        }

        public override Object ExecuteScalar(string sql, params IDataParameter[] parameters)
        {
            object result = null;

            ExecuteSqlCommand(sql, parameters, command =>
            {
                result = command.ExecuteScalar();
            });

            return result;
        }

        public override DbConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public override async Task<object> ExecuteScalarAsync(string sql, params IDataParameter[] parameters)
        {
            object result = null;

            ExecuteSqlCommand(sql, parameters, async command =>
            {
                result = await command.ExecuteScalarAsync();
            });

            return result;
        }
    }
}