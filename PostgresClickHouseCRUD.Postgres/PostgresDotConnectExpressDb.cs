using Devart.Data.PostgreSql;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.Postgres
{
    public class PostgresDotConnectExpressDb : AbstractDb<PgSqlConnection, PgSqlCommand>
    {
        public PostgresDotConnectExpressDb(string connectionString, string tableName) : base(connectionString,
            tableName)
        {
        }

        protected override string CreateTableQuery() => Queries.CreateTableQuery(TableName);

        protected override string CreateOneQuery(int key, int value) => Queries.CreateOneQuery(TableName, key, value);

        protected override string ReadOneQuery(int key) => Queries.ReadOneQuery(TableName, key);

        protected override string UpdateOneQuery(int key, int newValue) =>
            Queries.UpdateOneQuery(TableName, key, newValue);

        protected override string DeleteOneQuery(int key) => Queries.DeleteOneQuery(TableName, key);

        protected override string DropTableQuery() => Queries.DropTableQuery(TableName);

        public override string ToString() => "Postgres (dotConnect.Express.for.PostgreSQL)";
    }
}
