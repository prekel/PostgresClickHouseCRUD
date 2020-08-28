using System;
using System.Collections.Generic;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

using PostgresClickHouseCRUD.Abstract;
using PostgresClickHouseCRUD.ClickHouse;
using PostgresClickHouseCRUD.Postgres;

namespace PostgresClickHouseCRUD.Benchmark
{
    [SimpleJob(RunStrategy.ColdStart, 2, 0, 1)]
    public class CRUDBenchmark
    {
        [ParamsSource(nameof(ValuesForDb))]
        public IDb Db;

        [Params(1, 2, 3, 4, 5, 10, 100, 1000)]
        public int N;

        public IEnumerable<IDb> ValuesForDb => new List<IDb>
        {
            new PostgresDb("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432",
                "BenchmarkDb"),
            new ClickHouseDb("Host=127.0.0.1;Port=9000;User=default", "BenchmarkDb")
        };

        [GlobalSetup]
        public void ConnectCreateTable()
        {
            Db.Connect();
            Console.WriteLine("Connected");
            Db.DropTableIfExists();
            Db.CreateTable();
        }

        [Benchmark]
        public void CreateBenchmark()
        {
            for (var i = 0; i < N; i++)
            {
                Db.CreateOne(i, i);
            }
        }

        [Benchmark]
        public void ReadBenchmark()
        {
            for (var i = 0; i < N; i++)
            {
                Db.ReadOne(i);
            }
        }

        [Benchmark]
        public void UpdateBenchmark()
        {
            for (var i = 0; i < N; i++)
            {
                Db.UpdateOne(i, N - i);
            }
        }

        [Benchmark]
        public void DeleteBenchmark()
        {
            for (var i = 0; i < N; i++)
            {
                Db.DeleteOne(i);
            }
        }

        [GlobalCleanup]
        public void DropTableDisconnect()
        {
            Db.DropTableIfExists();
            Db.Dispose();
        }
    }
}
