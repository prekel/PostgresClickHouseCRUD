using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CsvHelper;

using PostgresClickHouseCRUD.Abstract;
using PostgresClickHouseCRUD.ClickHouse;
using PostgresClickHouseCRUD.Postgres;

namespace PostgresClickHouseCRUD.Benchmark
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
            using var npgsql =
                new PostgresNpgsqlDb("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432",
                    "CRUDBenchmark");
            using var dotConnect = new PostgresDotConnectExpressDb(
                "Host=localhost;User=postgres;Password=qwerty;Database=postgres;Port=15432", "CRUDBenchmark");
            using var chAdo = new ClickHouseAdoDb("Host=127.0.0.1;Port=9000;User=default", "CRUDBenchmark");
            using var chClient = new ClickHouseClientDb("Host=127.0.0.1;Port=8123;User=default", "CRUDBenchmark");
            using var chOctonica =
                new ClickHouseOctonicaClientDb("Host=127.0.0.1;Port=9000;User=default", "CRUDBenchmark");

            var dblist = new List<IDb> {npgsql, dotConnect, chAdo, chOctonica};
            foreach (var i in dblist)
            {
                i.Connect();
                i.Disconnect();
            }

            var r = new Random();
            var benchlist = GetBenchmarks(dblist, 50, new List<int> {100})
                .Concat(GetBenchmarks(dblist, 10, new List<int> {1000}))
                .Concat(GetBenchmarks(dblist, 3, new List<int> {5000}))
                .Concat(GetBenchmarks(dblist, 10, new List<int> {1000}))
                .Concat(GetBenchmarks(dblist, 3, new List<int> {5000}))
                .Concat(GetBenchmarks(dblist, 500, new List<int> {100}))
                .OrderBy(o => r.Next());

            //benchlist = GetBenchmarks(dblist, 10, new List<int> {2})
            //    .OrderBy(o => r.Next());

            //var results = benchlist.Select(b => b.Run());

            //using var stream = File.Open($"{DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}.csv", FileMode.Append);
            using var stream = File.Open("file12311.csv", FileMode.Append);
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("ru-ru"));

            csv.Configuration.HasHeaderRecord = false;

            foreach (var b in benchlist)
            {
                var result = b.Run();
                csv.WriteRecords(new List<CRUDBenchmark.RunResult> {result});
                //break;
            }
        }
    }
}
