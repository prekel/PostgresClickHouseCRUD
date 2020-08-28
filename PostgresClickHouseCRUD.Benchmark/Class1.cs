using System;
using System.Security.Cryptography;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace PostgresClickHouseCRUD.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter]
    public class Md5VsSha256
    {
        private byte[] data;
        private readonly MD5 md5 = MD5.Create();

        [Params(1000, 10000)]
        public int N;

        private readonly SHA256 sha256 = SHA256.Create();

        [GlobalSetup]
        public void Setup()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }
}
