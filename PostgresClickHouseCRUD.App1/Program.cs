using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using PostgresClickHouseCRUD.Abstract;
using PostgresClickHouseCRUD.ClickHouse;
using PostgresClickHouseCRUD.Postgres;

namespace PostgresClickHouseCRUD.App1
{
    internal class Program
    {
        private static IEnumerable<CRUDBenchmark> GetBenchmarks(IEnumerable<IDb> dbs, int launchCount,
            IEnumerable<int> recordsCounts) =>
            Enumerable.Range(0, launchCount)
                .Join(dbs, i => true, db => true, (i, db) => (i, db))
                .Join(recordsCounts, tuple => true, n => true, (tuple, n) => new {tuple.db, tuple.i, n})
                .Select(t => new CRUDBenchmark(t.db, t.n));

        private static void Main(string[] args)
        {
            using var db1 =
                new PostgresDb("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432",
                    "test10");
            using var db2 = new ClickHouseDb("Host=127.0.0.1;Port=9000;User=default", "test10");

            var benchlist = GetBenchmarks(new List<IDb> {db1, db2}, 2, new List<int> {1, 10, 100});

            db1.Connect();
            db2.Connect();

            var results = benchlist.Select(b => b.RunRunResult());

            foreach (var d in results)
            {
                Console.WriteLine(JsonSerializer.Serialize(d));
            }
        }
    }
}
