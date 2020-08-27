using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Npgsql;

namespace PostgresClickHouseCRUD.Postgres
{
    public class MyPersistableObject : IEnumerable
    {
        public int MyIntField;
        public string MyStringField;

        public IEnumerator GetEnumerator()
        {
            yield return MyIntField;
            yield return MyStringField;
        }
    }

    public class Class1
    {
        public void Test2()
        {
            var connString = "Host=localhost;Username=postgres;Password=qwerty;Database=postgres;Port=15432";

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            
            using (var cmd = new NpgsqlCommand("INSERT INTO test (int, str) VALUES (@p, @p1)", conn))
            {
                cmd.Parameters.AddWithValue("p", 123);
                cmd.Parameters.AddWithValue("p1", "some_value");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
