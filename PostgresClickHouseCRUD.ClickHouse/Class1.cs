using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using ClickHouse.Ado;

namespace PostgresClickHouseCRUD.ClickHouse
{
    public class Class1
    {
        public static ClickHouseConnection GetConnection(string cstr)
        {
            var settings = new ClickHouseConnectionSettings(cstr);
            var cnn = new ClickHouseConnection(settings);
            cnn.Open();
            return cnn;
        }

        public void Test1()
        {
            var connection = GetConnection("Host=127.0.0.1;Port=9000;User=default;");

            var list = new List<MyPersistableObject>();

            list.Add(new MyPersistableObject
                {MyIntField = 123, MyStringField = DateTime.Now.ToString(CultureInfo.InvariantCulture)});

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO test (int,str) VALUES @bulk";
            command.Parameters.Add(new ClickHouseParameter
            {
                ParameterName = "bulk",
                Value = list
            });
            command.ExecuteNonQuery();
        }

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
    }
}
