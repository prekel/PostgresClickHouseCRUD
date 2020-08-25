using System;

using PostgresClickHouseCRUD.ClickHouse;

namespace PostgresClickHouseCRUD.App1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var q = new Class1();

            q.Test1();
        }
    }
}
