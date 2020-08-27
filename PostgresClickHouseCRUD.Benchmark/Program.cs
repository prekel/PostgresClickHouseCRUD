using BenchmarkDotNet.Running;

namespace PostgresClickHouseCRUD.Benchmark
{
    public class Program
    {
        //public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        public static void Main(string[] args) => BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}
