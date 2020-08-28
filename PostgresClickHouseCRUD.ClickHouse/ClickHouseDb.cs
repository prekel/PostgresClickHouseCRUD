using ClickHouse.Ado;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.ClickHouse
{
    public class ClickHouseDb : AbstractDb<ClickHouseConnection, ClickHouseCommand>
    {
        public ClickHouseDb(string tableName, int n) : base(tableName, n)
        {
            
        }
        protected override string CreateTableQuery =>
            $"CREATE TABLE {TableName} (key Int32, value Int32) ENGINE = MergeTree() ORDER BY key";

        protected new string UpdateOneQuery => $"ALTER TABLE {TableName} UPDATE value = @val1 WHERE key = @val2";

        protected new string DeleteOneQuery => $"ALTER TABLE {TableName} DELETE WHERE key = @val1";
    }
}
