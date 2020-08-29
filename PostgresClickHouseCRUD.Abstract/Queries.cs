namespace PostgresClickHouseCRUD.Abstract
{
    public static class Queries
    {
        public static string CreateTableQuery(string table) =>
            $"CREATE TABLE {table} (key integer PRIMARY KEY, value integer NOT NULL)";

        public static string CreateTableClickHouseQuery(string table) =>
            $"CREATE TABLE {table} (key Int32, value Int32) ENGINE = MergeTree() ORDER BY key PRIMARY KEY key";

        public static string CreateOneQuery(string table, int key, int value) =>
            $"INSERT INTO {table} VALUES ({key}, {value})";

        public static string ReadOneQuery(string table, int key) =>
            $"SELECT value FROM {table} WHERE key = {key}";

        public static string UpdateOneQuery(string table, int key, int newValue) =>
            $"UPDATE {table} SET value = {newValue} WHERE key = {key}";

        public static string UpdateOneClickHouseQuery(string table, int key, int newValue) =>
            $"ALTER TABLE {table} UPDATE value = {newValue} WHERE key = {key}";

        public static string DeleteOneQuery(string table, int key) => $"DELETE FROM {table} WHERE key = {key}";

        public static string DeleteOneClickHouseQuery(string table, int key) =>
            $"ALTER TABLE {table} DELETE WHERE key = {key}";

        public static string DropTableQuery(string table) => $"DROP TABLE IF EXISTS {table}";
    }
}
