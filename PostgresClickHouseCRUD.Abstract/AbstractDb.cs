using System;
using System.Data.Common;
using System.Diagnostics;

namespace PostgresClickHouseCRUD.Abstract
{
    public abstract class AbstractDb<TConnection, TCommand> : IDisposable
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
    {
        public AbstractDb(string tableName = "CRUDBenchmark", int n = 100)
        {
            TableName = tableName;
            N = n;
        }

        protected TConnection Connection { get; } = new TConnection();

        public string TableName { get; }

        public int N { get; }

        protected string CreateTableQuery => $"CREATE TABLE {TableName} (id integer)";
        protected string CreateOneQuery => $"INSERT INTO {TableName} VALUES (1)";
        protected string ReadOneQuery => $"SELECT * FROM {TableName} WHERE id = 1";
        protected string UpdateOneQuery => $"UPDATE {TableName} SET id = 2 WHERE id = 1";
        protected string DeleteOneQuery => $"DELETE FROM {TableName} WHERE id = 2";
        protected string DropTableQuery => $"DROP TABLE {TableName}";

        public void Connect(string cstr)
        {
            Connection.ConnectionString = cstr;
            Connection.Open();
        }

        public void CreateTable()
        {
            using var cmd = new TCommand {CommandText = CreateTableQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == -1);
        }

        public void CreateOne()
        {
            using var cmd = new TCommand {CommandText = CreateOneQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == 1);
        }

        public void ReadOne()
        {
            using var cmd = new TCommand {CommandText = ReadOneQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == -1);
        }

        public void UpdateOne()
        {
            using var cmd = new TCommand {CommandText = UpdateOneQuery, Connection = Connection};
            var aff = cmd.ExecuteNonQuery();
            Debug.Assert(aff == 1);
        }

        public void DeleteOne()
        {
            using var cmd = new TCommand {CommandText = DeleteOneQuery, Connection = Connection};
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
