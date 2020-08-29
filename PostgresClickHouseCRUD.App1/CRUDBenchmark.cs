using System;
using System.Diagnostics;
using System.Text.Json;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.App1
{
    public class CRUDBenchmark
    {
        public CRUDBenchmark(IDb db, int recordCount)
        {
            Db = db;
            RecordCount = recordCount;
        }

        public IDb Db { get; }

        public int RecordCount { get; }

        public RunResult Run()
        {
            CreateTable();
            var r = new RunResult(this)
            {
                Create = Create().TotalMilliseconds,
                Read = Read().TotalMilliseconds,
                Update = Update().TotalMilliseconds,
                Delete = Delete().TotalMilliseconds
            };
            DropTable();

            Debug.WriteLine(DateTime.Now + " " + JsonSerializer.Serialize(r));

            return r;
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

            for (var i = 0; i < RecordCount; i++)
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
            for (var i = 0; i < RecordCount; i++)
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
            for (var i = 0; i < RecordCount; i++)
            {
                Db.UpdateOne(i, RecordCount - i);
            }

            sw.Stop();

            return sw.Elapsed;
        }

        public TimeSpan Delete()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < RecordCount; i++)
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

        public class RunResult
        {
            public RunResult(CRUDBenchmark bench)
            {
                DbName = bench.Db.ToString();
                RecordCount = bench.RecordCount;
            }

            public string DbName { get; }
            public int RecordCount { get; }

            public double Create { get; protected internal set; }

            public double Read { get; protected internal set; }

            public double Update { get; protected internal set; }

            public double Delete { get; protected internal set; }
        }
    }
}
