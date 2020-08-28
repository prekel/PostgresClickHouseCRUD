﻿using System;
using System.Data;

namespace PostgresClickHouseCRUD.Abstract
{
    public abstract class AbstractDb<TConnection, TCommand> : IDisposable, IDb
        where TConnection : IDbConnection, new()
        where TCommand : IDbCommand, new()
    {
        public AbstractDb(string connectionString, string tableName)
        {
            TableName = tableName;
            ConnectionString = connectionString;
        }

        private TConnection Connection { get; } = new TConnection();

        public string TableName { get; }

        private string ConnectionString { get; }

        public void Connect()
        {
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
        }

        public void CreateTable()
        {
            using var cmd = new TCommand {CommandText = CreateTableQuery(), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void CreateOne(int key, int value)
        {
            using var cmd = new TCommand {CommandText = CreateOneQuery(key, value), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void ReadOne(int key)
        {
            using var cmd = new TCommand {CommandText = ReadOneQuery(key), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void UpdateOne(int key, int newValue)
        {
            using var cmd = new TCommand {CommandText = UpdateOneQuery(key, newValue), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void DeleteOne(int key)
        {
            using var cmd = new TCommand {CommandText = DeleteOneQuery(key), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void DropTableIfExists()
        {
            using var cmd = new TCommand {CommandText = DropTableQuery(), Connection = Connection};
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        protected virtual string CreateTableQuery() =>
            $"CREATE TABLE {TableName} (key integer PRIMARY KEY, value integer NOT NULL)";

        protected virtual string CreateOneQuery(int key, int value) =>
            $"INSERT INTO {TableName} VALUES ({key}, {value})";

        protected virtual string ReadOneQuery(int key) => $"SELECT value FROM {TableName} WHERE key = {key}";

        protected virtual string UpdateOneQuery(int key, int newValue) =>
            $"UPDATE {TableName} SET value = {newValue} WHERE key = {key}";

        protected virtual string DeleteOneQuery(int key) => $"DELETE FROM {TableName} WHERE key = {key}";
        protected virtual string DropTableQuery() => $"DROP TABLE IF EXISTS {TableName}";
    }
}
