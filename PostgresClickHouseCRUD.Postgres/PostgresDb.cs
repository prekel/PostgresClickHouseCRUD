using System.Data.Common;
using System.Diagnostics;

using Npgsql;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.Postgres
{
    public class PostgresDb : AbstractDb<NpgsqlConnection, NpgsqlCommand>
    {
        public PostgresDb(string tableName, int n) : base(tableName, n)
        {
            
        }

        protected override string CreateTableQuery => $"CREATE TABLE {TableName} (key integer PRIMARY KEY, value integer NOT NULL)";
    }
}
