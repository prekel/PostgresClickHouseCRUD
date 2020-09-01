using System.Data;

namespace PostgresClickHouseCRUD.Abstract
{
    public abstract class AbstractDb<TConnection, TCommand> : IDb
        where TConnection : class, IDbConnection, new()
        where TCommand : IDbCommand, new()
    {
        protected AbstractDb(string connectionString, string tableName)
        {
            TableName = tableName;
            Connection.ConnectionString = connectionString;
        }

        private TConnection Connection { get; } = new TConnection();

        public string TableName { get; set; }

        public void Connect()
        {
            Connection.Open();
        }

        public void Disconnect()
        {
            Connection.Close();
        }

        public void CreateTable()
        {
            using var cmd = new TCommand {CommandText = CreateTableQuery(), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void CreateOne(int key, int value)
        {
            using var cmd = new TCommand {CommandText = CreateOneQuery(key, value), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void ReadOne(int key)
        {
            using var cmd = new TCommand {CommandText = ReadOneQuery(key), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void UpdateOne(int key, int newValue)
        {
            using var cmd = new TCommand {CommandText = UpdateOneQuery(key, newValue), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void DeleteOne(int key)
        {
            using var cmd = new TCommand {CommandText = DeleteOneQuery(key), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void DropTableIfExists()
        {
            using var cmd = new TCommand {CommandText = DropTableQuery(), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        protected abstract string CreateTableQuery();

        protected abstract string CreateOneQuery(int key, int value);

        protected abstract string ReadOneQuery(int key);

        protected abstract string UpdateOneQuery(int key, int newValue);

        protected abstract string DeleteOneQuery(int key);
        protected abstract string DropTableQuery();
    }
}
