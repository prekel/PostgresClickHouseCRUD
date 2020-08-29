using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CsvHelper;

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
                    "CRUDBenchmark");
            using var db2 = new ClickHouseDb("Host=127.0.0.1;Port=9000;User=default", "CRUDBenchmark");

            var benchlist = GetBenchmarks(new List<IDb> {db1, db2}, 50, new List<int> {100})
                .Concat(GetBenchmarks(new List<IDb> {db1, db2}, 10, new List<int> {1000}))
                .Concat(GetBenchmarks(new List<IDb> {db1, db2}, 2, new List<int> {5000}))
                .OrderBy(o => o.Db.ToString());

            db1.Connect();
            db2.Connect();

            var results = benchlist.Select(b => b.Run()).ToList();

            //foreach (var d in results)
            //{
            //    Console.WriteLine(JsonSerializer.Serialize(d));
            //}

            using var writer = new StreamWriter("file1.csv");
            using var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("ru-ru"));
            csv.WriteRecords((IEnumerable) results);
        }
    }
}
