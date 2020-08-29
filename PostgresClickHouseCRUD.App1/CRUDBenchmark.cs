using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.App1
{
    public class CRUDBenchmark
    {
        public CRUDBenchmark(IDb db, int n)
        {
            Db = db;
            N = n;
        }

        public IDb Db { get; }

        public int N { get; }

        public class RunResult
        {
            public string DbName { get; }
            public int N { get; }
            public Dictionary<string, double> Results { get; }

            public RunResult(CRUDBenchmark bench, IDictionary<string, TimeSpan> result)
            {
                DbName = bench.Db.ToString();
                N = bench.N;
                Results = result
                    .Select(kv => (kv.Key, kv.Value.TotalMilliseconds))
                    .ToDictionary(t => t.Key, t => t.TotalMilliseconds);
            }
        }

        public RunResult RunRunResult() => new RunResult(this, Run());

        public IDictionary<string, TimeSpan> Run()
        {
            var d = new Dictionary<string, TimeSpan>();

            d.Add("CreateTable", CreateTable());
            d.Add("Create", Create());
            d.Add("Read", Read());
            d.Add("Update", Update());
            d.Add("Delete", Delete());
            d.Add("DropTable", DropTable());

            return d;
        }
        
        public TimeSpan CreateTable()
        {
            var sw = new Stopwatch();
            sw.Start();
            Db.DropTableIfExists();
            Db.CreateTable();
            sw.Stop();

            return sw.Elapsed;
        }

        public TimeSpan Create()
        {
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < N; i++)
            {
                Db.CreateOne(i, i);
            }

            sw.Stop();

            return sw.Elapsed;
        }


        public TimeSpan Read()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < N; i++)
            {
                Db.ReadOne(i);
            }

            sw.Stop();

            return sw.Elapsed;
        }

        public TimeSpan Update()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < N; i++)
            {
                Db.UpdateOne(i, N - i);
            }

            sw.Stop();

            return sw.Elapsed;
        }

        public TimeSpan Delete()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < N; i++)
            {
                Db.DeleteOne(i);
            }

            sw.Stop();

            return sw.Elapsed;
        }

        public TimeSpan DropTable()
        {
            var sw = new Stopwatch();
            sw.Start();

            Db.DropTableIfExists();

            sw.Stop();

            return sw.Elapsed;
        }
    }
}
