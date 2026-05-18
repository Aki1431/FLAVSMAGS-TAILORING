using System;
using Microsoft.Data.Sqlite;

namespace FLAVSMAGS_TAILORING.Data
{
    /// <summary>
    /// Creates the database tables if they don't exist and seeds default data.
    /// </summary>
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                CreateTables(conn);
                SeedData(conn);
            }
        }

        private static void CreateTables(SqliteConnection conn)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    FullName TEXT
                );

                CREATE TABLE IF NOT EXISTS Customers (
                    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerName TEXT NOT NULL,
                    Email TEXT,
                    Phone TEXT,
                    Address TEXT,
                    DateRegistered TEXT NOT NULL,
                    IsActive INTEGER NOT NULL DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS Orders (
                    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerId INTEGER,
                    CustomerName TEXT NOT NULL,
                    ContactNumber TEXT,
                    Email TEXT,
                    ServiceType TEXT,
                    ItemType TEXT,
                    Quantity INTEGER DEFAULT 1,
                    Items TEXT NOT NULL DEFAULT '',
                    TotalAmount REAL NOT NULL,
                    Status TEXT NOT NULL,
                    OrderDate TEXT NOT NULL,
                    CompletionDate TEXT,
                    Notes TEXT,
                    DeletedDate TEXT
                );

                CREATE TABLE IF NOT EXISTS InventoryItems (
                    ItemId TEXT PRIMARY KEY,
                    MaterialName TEXT NOT NULL,
                    Quantity REAL NOT NULL DEFAULT 0,
                    Unit TEXT NOT NULL,
                    UnitPrice REAL NOT NULL,
                    ReorderLevel REAL NOT NULL DEFAULT 10,
                    LastUpdated TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Expenses (
                    ExpenseId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Description TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    Amount REAL NOT NULL,
                    ExpenseDate TEXT NOT NULL,
                    Notes TEXT
                );

                CREATE TABLE IF NOT EXISTS Sales (
                    SaleId INTEGER PRIMARY KEY AUTOINCREMENT,
                    OrderId INTEGER NOT NULL,
                    ProductCategory TEXT NOT NULL,
                    UnitsSold INTEGER NOT NULL,
                    Revenue REAL NOT NULL,
                    Cost REAL NOT NULL,
                    SaleDate TEXT NOT NULL,
                    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
                );
            ";

            using (var cmd = new SqliteCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        private static void SeedData(SqliteConnection conn)
        {
            // Default admin user (password: 1234)
            if (IsTableEmpty(conn, "Users"))
            {
                string sql = "INSERT INTO Users (Username, PasswordHash, FullName) VALUES (@u, @p, @f)";
                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@u", "admin");
                    cmd.Parameters.AddWithValue("@p", "1234");
                    cmd.Parameters.AddWithValue("@f", "Administrator");
                    cmd.ExecuteNonQuery();
                }
            }

            // Sample customer
            if (IsTableEmpty(conn, "Customers"))
            {
                string sql = @"INSERT INTO Customers (CustomerName, Email, Phone, Address, DateRegistered, IsActive) 
                               VALUES (@n, @e, @p, @a, @d, 1)";
                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", "John Doe");
                    cmd.Parameters.AddWithValue("@e", "john@email.com");
                    cmd.Parameters.AddWithValue("@p", "123-456-7890");
                    cmd.Parameters.AddWithValue("@a", "123 Main St");
                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static bool IsTableEmpty(SqliteConnection conn, string tableName)
        {
            using (var cmd = new SqliteCommand($"SELECT COUNT(*) FROM {tableName}", conn))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return true;
                return Convert.ToInt64(result) == 0;
            }
        }
    }
}

//using System;
//using Microsoft.Data.Sqlite;
//using FLAVSMAGS_TAILORING.Data;

//namespace FLAVSMAGS_TAILORING.Data
//{
//    public static class DatabaseInitializer
//    {
//        public static void Initialize()
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                CreateTables(conn);
//                SeedData(conn);
//            }
//        }

//        private static void CreateTables(SqliteConnection conn)
//        {
//            string sql = @"
//                CREATE TABLE IF NOT EXISTS Users (
//                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
//                    Username TEXT NOT NULL UNIQUE,
//                    PasswordHash TEXT NOT NULL,
//                    FullName TEXT
//                );

//                CREATE TABLE IF NOT EXISTS Customers (
//                    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
//                    CustomerName TEXT NOT NULL,
//                    Email TEXT,
//                    Phone TEXT,
//                    Address TEXT,
//                    DateRegistered TEXT NOT NULL,
//                    IsActive INTEGER NOT NULL DEFAULT 1
//                );

//                -- UPDATED Orders table with new tailoring fields AND DeletedDate
//                CREATE TABLE IF NOT EXISTS Orders (
//                    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
//                    CustomerId INTEGER,
//                    CustomerName TEXT NOT NULL,
//                    ContactNumber TEXT,
//                    Email TEXT,
//                    ServiceType TEXT,
//                    ItemType TEXT,
//                    Quantity INTEGER DEFAULT 1,
//                    Items TEXT NOT NULL DEFAULT '',
//                    TotalAmount REAL NOT NULL,
//                    Status TEXT NOT NULL,
//                    OrderDate TEXT NOT NULL,
//                    CompletionDate TEXT,
//                    Notes TEXT,
//                    DeletedDate TEXT
//                );

//                CREATE TABLE IF NOT EXISTS InventoryItems (
//                    ItemId TEXT PRIMARY KEY,
//                    MaterialName TEXT NOT NULL,
//                    Quantity REAL NOT NULL DEFAULT 0,
//                    Unit TEXT NOT NULL,
//                    UnitPrice REAL NOT NULL,
//                    ReorderLevel REAL NOT NULL DEFAULT 10,
//                    LastUpdated TEXT NOT NULL
//                );

//                CREATE TABLE IF NOT EXISTS Expenses (
//                    ExpenseId INTEGER PRIMARY KEY AUTOINCREMENT,
//                    Description TEXT NOT NULL,
//                    Category TEXT NOT NULL,
//                    Amount REAL NOT NULL,
//                    ExpenseDate TEXT NOT NULL,
//                    Notes TEXT
//                );

//                CREATE TABLE IF NOT EXISTS Sales (
//                    SaleId INTEGER PRIMARY KEY AUTOINCREMENT,
//                    OrderId INTEGER NOT NULL,
//                    ProductCategory TEXT NOT NULL,
//                    UnitsSold INTEGER NOT NULL,
//                    Revenue REAL NOT NULL,
//                    Cost REAL NOT NULL,
//                    SaleDate TEXT NOT NULL,
//                    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
//                );
//            ";

//            using (var cmd = new SqliteCommand(sql, conn))
//            {
//                cmd.ExecuteNonQuery();
//            }
//        }

//        private static void SeedData(SqliteConnection conn)
//        {
//            if (IsTableEmpty(conn, "Users"))
//            {
//                string insertUser = "INSERT INTO Users (Username, PasswordHash, FullName) VALUES (@u, @p, @f)";
//                using (var cmd = new SqliteCommand(insertUser, conn))
//                {
//                    cmd.Parameters.AddWithValue("@u", "admin");
//                    cmd.Parameters.AddWithValue("@p", "1234");
//                    cmd.Parameters.AddWithValue("@f", "Administrator");
//                    cmd.ExecuteNonQuery();
//                }
//            }

//            if (IsTableEmpty(conn, "Customers"))
//            {
//                string insertCust = @"INSERT INTO Customers (CustomerName, Email, Phone, Address, DateRegistered, IsActive) 
//                                      VALUES (@n, @e, @p, @a, @d, 1)";
//                using (var cmd = new SqliteCommand(insertCust, conn))
//                {
//                    cmd.Parameters.AddWithValue("@n", "John Doe");
//                    cmd.Parameters.AddWithValue("@e", "john@email.com");
//                    cmd.Parameters.AddWithValue("@p", "123-456-7890");
//                    cmd.Parameters.AddWithValue("@a", "123 Main St");
//                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        private static bool IsTableEmpty(SqliteConnection conn, string tableName)
//        {
//            using (var cmd = new SqliteCommand($"SELECT COUNT(*) FROM {tableName}", conn))
//            {
//                object? result = cmd.ExecuteScalar();
//                if (result == null || result == DBNull.Value)
//                    return true;
//                long count = Convert.ToInt64(result);
//                return count == 0;
//            }
//        }
//    }
//}