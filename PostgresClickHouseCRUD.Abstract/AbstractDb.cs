using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace PostgresClickHouseCRUD.Abstract
{
    public abstract class AbstractDb<TConnection, TCommand> : IDisposable, IDb
        where TConnection : IDbConnection, new()
        where TCommand : IDbCommand, new()
    {
        public AbstractDb(string tableName = "CRUDBenchmark", int n = 100)
        {
            TableName = tableName;
            N = n;
        }

        private TConnection Connection { get; } = new TConnection();

        public string TableName { get; }

        public int N { get; }

        protected abstract string CreateTableQuery { get; }
        protected string CreateOneQuery => $"INSERT INTO {TableName} VALUES (@val1, @val2)";
        protected string ReadOneQuery => $"SELECT value FROM {TableName} WHERE key = @val1";
        protected string UpdateOneQuery => $"UPDATE {TableName} SET value = @val1 WHERE key = @val2";
        protected string DeleteOneQuery => $"DELETE FROM {TableName} WHERE key = @val1";
        protected string DropTableQuery => $"DROP TABLE IF EXISTS {TableName}";

        public void Connect(string cstr)
        {
            Connection.ConnectionString = cstr;
            Connection.Open();
        }

        public void CreateTable()
        {
            using var cmd = new TCommand {CommandText = CreateTableQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            //Debug.Assert(aff == -1);
        }

        public void CreateOne(int key, int value)
        {
            using var cmd = new TCommand {CommandText = CreateOneQuery, Connection = Connection};
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "val1";
            p1.Value = key;
            cmd.Parameters.Add(p1);
            var p2 = cmd.CreateParameter();
            p2.ParameterName = "val2";
            p2.Value = value;
            cmd.Parameters.Add(p2);
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == 1);
        }

        public void ReadOne(int key, int expectedValue)
        {
            using var cmd = new TCommand {CommandText = ReadOneQuery, Connection = Connection};
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "val1";
            p1.Value = key;
            cmd.Parameters.Add(p1);
            var read = (int) cmd.ExecuteScalar();
            Debug.Assert(read == expectedValue);
        }

        public void UpdateOne(int key, int newValue)
        {
            using var cmd = new TCommand {CommandText = UpdateOneQuery, Connection = Connection};
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "val1";
            p1.Value = newValue;
            cmd.Parameters.Add(p1);
            var p2 = cmd.CreateParameter();
            p2.ParameterName = "val2";
            p2.Value = key;
            cmd.Parameters.Add(p2);
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == 1);
        }

        public void DeleteOne(int key)
        {
            using var cmd = new TCommand {CommandText = DeleteOneQuery, Connection = Connection};
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "val1";
            p1.Value = key;
            cmd.Parameters.Add(p1);
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == 1);
        }

        public void DropTable()
        {
            using var cmd = new TCommand {CommandText = DropTableQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == -1);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
