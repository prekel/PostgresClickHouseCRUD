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
            using var q3 = new PostgresDb("test5", 1);
            
            q3.Connect("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432");
            
            q3.CreateTable();
            q3.CreateOne();
            q3.ReadOne();
            q3.UpdateOne();
            q3.DeleteOne();
            q3.DropTable();
        }
    }
}
