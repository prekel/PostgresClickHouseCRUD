namespace PostgresClickHouseCRUD.Abstract
{
    public interface IDb
    {
        public void Connect(string cstr);

        public void CreateTable();

        public void CreateOne(int key, int value);

        public bool ReadOne(int key, int expectedValue);

        public void UpdateOne(int key, int newValue);

        public void DeleteOne(int key);

        public void DropTable();
    }
}
