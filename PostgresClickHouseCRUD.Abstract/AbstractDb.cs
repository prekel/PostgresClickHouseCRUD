using System;

namespace PostgresClickHouseCRUD.Abstract
{
    public abstract class AbstractDb
    {
        public abstract void CreateTable();

        public abstract void CreateBenchmark();

        public abstract void ReadBenchmark();

        public abstract void UpdateBenchmark();

        public abstract void DeleteTable();
    }
}
