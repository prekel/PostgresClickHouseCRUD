using Npgsql;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.Postgres
{
    public class PostgresDb : AbstractDb<NpgsqlConnection, NpgsqlCommand>
    {
        public PostgresDb(string connectionString, string tableName) : base(connectionString, tableName)
        {
        }

        public override string ToString() => "Postgres";
    }
}
