using System.Data.Common;

using Npgsql;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.Postgres
{
    public class PostgresDb : AbstractDb<NpgsqlConnection, NpgsqlCommand>
    {
        public PostgresDb(string tableName, int n) : base(tableName, n)
        {
            
        }
    }
}
