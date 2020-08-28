using System.Collections.Generic;

using PostgresClickHouseCRUD.Abstract;

namespace PostgresClickHouseCRUD.App1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //using var db1 = new PostgresDb("test10");
            //using var db2 = new ClickHouseDb("test10");

            //db1.Connect("Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432");
            //db2.Connect("Host=127.0.0.1;Port=9000;User=default");

            var dblist = new List<IDb>();

            foreach (var db in dblist)
            {
                db.CreateTable();
                db.CreateOne(1, 2);
                db.ReadOne(1);
                db.UpdateOne(1, 3);
                db.ReadOne(1);
                db.DeleteOne(1);
                db.DropTableIfExists();
            }
        }
    }
}
