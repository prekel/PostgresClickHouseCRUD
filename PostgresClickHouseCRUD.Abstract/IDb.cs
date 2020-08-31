using System;

namespace PostgresClickHouseCRUD.Abstract
{
    public interface IDb : IDisposable
    {
        public string TableName { get; set; }
        
        public void Connect();

        public void Disconnect();

        public void CreateTable();

        public void CreateOne(int key, int value);

        public void ReadOne(int key);

        public void UpdateOne(int key, int newValue);

        public void DeleteOne(int key);

        public void DropTableIfExists();
    }
}
