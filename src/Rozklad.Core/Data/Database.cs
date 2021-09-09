using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rozklad.Core.Data
{
    public interface IDatabase
    {
        string ConnectionString { get; }
        void ExecuteNonQuery(string sql, params IDataParameter[] parameters);
        DataRow GetRecord(string sql, params IDataParameter[] parameters);
        DataTable Execute(string sql, params IDataParameter[] parameters);
        object ExecuteScalar(string sql, params IDataParameter[] parameters);
        DbConnection GetConnection();
        Task<object> ExecuteScalarAsync(string sql, params IDataParameter[] parameters);
    }

    public abstract class Database : IDatabase
    {
        protected readonly ILogger Logger;

        public string ConnectionString { get; protected set; }

        protected Database(string connectionString, ILogger logger)
        {
            Logger = logger;
            ConnectionString = connectionString;
        }

        public abstract void ExecuteNonQuery(string sql, params IDataParameter[] parameters);


        public virtual DataRow GetRecord(string sql, params IDataParameter[] parameters)
        {
            var dataTable = Execute(sql, parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }

            return null;
        }

        public abstract DataTable Execute(string sql, params IDataParameter[] parameters);

        public abstract object ExecuteScalar(string sql, params IDataParameter[] parameters);

        public abstract DbConnection GetConnection();

        public abstract Task<object> ExecuteScalarAsync(string sql, params IDataParameter[] parameters);
    }
}