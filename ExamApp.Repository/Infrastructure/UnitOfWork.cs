using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExamApp.Repository.Infrastructure
{
    public interface IUnitOfWork
    {
        SqlTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        SqlConnection GetConnection();
        SqlTransaction GetTransaction();
        void SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly string _connectionString;

        private SqlTransaction sqlTransaction;
        private SqlConnection sqlConnection;

        public UnitOfWork(IConfiguration configuration, IHostingEnvironment env)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            sqlConnection = new SqlConnection(_connectionString);
        }

        public SqlTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction(isolationLevel);
            }

            return sqlTransaction;
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

        public SqlTransaction GetTransaction()
        {
            return sqlTransaction;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    sqlTransaction = null;
                }
                sqlConnection.Close();
                disposed = true;
            }
        }

        public void SaveChanges()
        {
            sqlTransaction.Commit();
            sqlConnection.Close();
            sqlTransaction = null;
        }

        private static string GetEnvironmentVariable(string name)
        {
            return name + ": " +
                System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }


        ~UnitOfWork() { Dispose(false); }
    }
}
