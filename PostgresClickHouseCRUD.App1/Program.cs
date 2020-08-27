using System;

using ClickHouse.Ado;

using PostgresClickHouseCRUD.ClickHouse;
using PostgresClickHouseCRUD.Postgres;

namespace PostgresClickHouseCRUD.App1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var q = new ClickHouse.Class1();

            //q.Test1();
            
            
            var q1 = new Postgres.Class1();
            q1.Test2();
        }
    }
}
