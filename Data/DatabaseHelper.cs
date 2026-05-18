using System;
using Microsoft.Data.Sqlite;

namespace FLAVSMAGS_TAILORING.Data
{
    /// <summary>
    /// Provides a new, open SQLite connection.
    /// Foreign keys are enabled by default.
    /// </summary>
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
            $"Data Source={AppDomain.CurrentDomain.BaseDirectory}FLAVSMAGS_TAILORING.db;Foreign Keys=True;";

        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            // Enable WAL for better concurrent reads (optional)
            using (var pragma = new SqliteCommand("PRAGMA journal_mode=WAL;", conn))
                pragma.ExecuteNonQuery();
            return conn;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using Microsoft.Data.Sqlite;   // <-- changed

//namespace FLAVSMAGS_TAILORING.Data
//{
//    public static class DatabaseHelper
//    {
//        private static readonly string ConnectionString =
//            $"Data Source={AppDomain.CurrentDomain.BaseDirectory}FLAVSMAGS_TAILORING.db";

//        public static SqliteConnection GetConnection()
//        {
//            var conn = new SqliteConnection(ConnectionString);
//            conn.Open();
//            using (var cmd = new SqliteCommand("PRAGMA foreign_keys = ON;", conn))
//            {
//                cmd.ExecuteNonQuery();
//            }
//            return conn;
//        }
//    }
//}