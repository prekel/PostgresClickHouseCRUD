using ClickHouse.Ado;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.ClickHouse
{
    public class ClickHouseAdoDb : AbstractDb<ClickHouseConnection, ClickHouseCommand>
    {
        public ClickHouseAdoDb(string connectionString, string tableName) : base(connectionString, tableName)
        {
        }

        protected override string CreateTableQuery() => Queries.CreateTableClickHouseQuery(TableName);

        protected override string CreateOneQuery(int key, int value) => Queries.CreateOneQuery(TableName, key, value);

        protected override string ReadOneQuery(int key) => Queries.ReadOneQuery(TableName, key);

        protected override string UpdateOneQuery(int key, int newValue) =>
            Queries.UpdateOneClickHouseQuery(TableName, key, newValue);

        protected override string DeleteOneQuery(int key) => Queries.DeleteOneClickHouseQuery(TableName, key);

        protected override string DropTableQuery() => Queries.DropTableQuery(TableName);

        public override string ToString() => "ClickHouse (ClickHouse.Ado)";
    }
}
