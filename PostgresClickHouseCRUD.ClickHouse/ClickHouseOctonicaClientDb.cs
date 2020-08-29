using System.IO;

using Octonica.ClickHouseClient;

using PostgresClickHouseCRUD.Abstract;

using ClickHouseConnection = ClickHouse.Client.ADO.ClickHouseConnection;

namespace PostgresClickHouseCRUD.ClickHouse
{
    public class ClickHouseOctonicaClientDb : IDb
    {
        protected string TableName { get; }

        protected ClickHouseConnection Connection { get; }

        public ClickHouseOctonicaClientDb(string connectionString, string tableName)
        {
            TableName = tableName;
            Connection = new ClickHouseConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public void Connect()
        {
            Connection.Open();
        }

        public void CreateTable()
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.CreateTableClickHouseQuery(TableName);
            cmd.ExecuteNonQuery();
        }

        public void CreateOne(int key, int value)
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.CreateOneQuery(TableName, key, value);
            cmd.ExecuteNonQuery();
        }

        public void ReadOne(int key)
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.ReadOneQuery(TableName, key);
            cmd.ExecuteNonQuery();
        }

        public void UpdateOne(int key, int newValue)
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.UpdateOneClickHouseQuery(TableName, key, newValue);
            cmd.ExecuteNonQuery();
        }

        public void DeleteOne(int key)
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.DeleteOneClickHouseQuery(TableName, key);
            cmd.ExecuteNonQuery();
        }

        public void DropTableIfExists()
        {
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = Queries.DropTableQuery(TableName);
            cmd.ExecuteNonQuery();
        }

        public override string ToString() => "ClickHouse (Octonica.ClickHouseClient)";
    }
}
