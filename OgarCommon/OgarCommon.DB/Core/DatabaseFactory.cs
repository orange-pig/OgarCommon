using System;

namespace OgarCommon.DB.Core
{
    public static class DatabaseFactory
    {
        public static IDatabase CreateDatabase(DatabaseType dbType, string connectionString = null)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return CreateDatabase(typeof(SqlServerDatabase), connectionString);
                case DatabaseType.MySQL:
                    return CreateDatabase(typeof(MySqlDatabase), connectionString);
                case DatabaseType.Oracle:
                    return CreateDatabase(typeof(OracleDatabase), connectionString);
                case DatabaseType.Ole:
                    return CreateDatabase(typeof(OleDatabase),connectionString);
                case DatabaseType.SQLite:
                    return CreateDatabase(typeof(SqliteDatabase),connectionString);
                default:
                    throw new Exception("Unsupported database type!");
            }
        }

        private static IDatabase CreateDatabase(Type dbType, string connectionString = null)
        {
            System.Reflection.ConstructorInfo constructor = dbType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                return null;
            }

            var database = constructor.Invoke(null) as IDatabase;
            if (database != null && !string.IsNullOrEmpty(connectionString))
                database.ConnectionString = connectionString;
            return database;
        }
    }
}
