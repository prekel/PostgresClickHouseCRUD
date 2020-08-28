using ClickHouse.Ado;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.ClickHouse
{
    public class ClickHouseDb : AbstractDb<ClickHouseConnection, ClickHouseCommand>
    {
        public ClickHouseDb(string tableName, int n) : base(tableName, n)
        {
        }

        protected override string CreateTableQuery() =>
            $"CREATE TABLE {TableName} (key Int32, value Int32) ENGINE = MergeTree() ORDER BY key PRIMARY KEY key";

        protected override string UpdateOneQuery(int key, int newValue) =>
            $"ALTER TABLE {TableName} UPDATE value = {newValue} WHERE key = {key}";

        protected override string DeleteOneQuery(int key) => $"ALTER TABLE {TableName} DELETE WHERE key = {key}";
    }
}
