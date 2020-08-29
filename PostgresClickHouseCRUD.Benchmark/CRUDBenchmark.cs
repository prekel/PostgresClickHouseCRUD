using System;
using System.Collections.Generic;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

using PostgresClickHouseCRUD.Abstract;
using PostgresClickHouseCRUD.ClickHouse;
using PostgresClickHouseCRUD.Postgres;

namespace PostgresClickHouseCRUD.Benchmark
{
    [SimpleJob(RunStrategy.ColdStart, 1, 0, 1)]
    public class CRUDBenchmark
    {
        [ParamsSource(nameof(ValuesForDb))]
        public IDb Db;

        [Params(1, 10, 100)]
        public int N;

        public IEnumerable<IDb> ValuesForDb => new List<IDb>
        {
            new PostgresDb("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432",
                "CRUDBenchmark"),
            new ClickHouseDb("Host=127.0.0.1;Port=9000;User=default", "CRUDBenchmark")
        };

        [GlobalSetup]
        public void Connect()
        {
            Db.Connect();
            Console.WriteLine("Connected");
        }

        [Benchmark]
        public void Select1() => Db.Select1();
        
        [Benchmark]
        public void CreateTable()
        {
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

        [Benchmark]
        public void DropTable()
        {
            Db.DropTableIfExists();
        }

        [GlobalCleanup]
        public void Disconnect()
        {
            try
            {
                Db.Dispose();
                Console.WriteLine("Disconnected");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
