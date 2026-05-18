using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using FLAVSMAGS_TAILORING.Models;

namespace FLAVSMAGS_TAILORING.Data
{
    public sealed class DatabaseManager
    {
        private static DatabaseManager? _instance;
        private static readonly object _lock = new object();

        private DatabaseManager() { }

        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new DatabaseManager();
                    }
                }
                return _instance;
            }
        }

        // ==================== CUSTOMER CRUD ====================
        public List<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Customers", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapCustomer(reader));
            return list;
        }

        public Customer? GetCustomerById(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Customers WHERE CustomerId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapCustomer(reader);
            return null;
        }

        public Customer AddCustomer(Customer customer)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"INSERT INTO Customers (CustomerName, Email, Phone, Address, DateRegistered, IsActive)
                           VALUES (@n, @e, @p, @a, @d, @act);
                           SELECT last_insert_rowid();";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@n", customer.CustomerName);
            cmd.Parameters.AddWithValue("@e", (object?)customer.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@p", (object?)customer.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@a", (object?)customer.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@d", customer.DateRegistered.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@act", customer.IsActive ? 1 : 0);
            customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"UPDATE Customers SET CustomerName=@n, Email=@e, Phone=@p, Address=@a, IsActive=@act
                           WHERE CustomerId=@id";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@n", customer.CustomerName);
            cmd.Parameters.AddWithValue("@e", (object?)customer.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@p", (object?)customer.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@a", (object?)customer.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@act", customer.IsActive ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", customer.CustomerId);
            return cmd.ExecuteNonQuery() > 0;
        }

        private Customer MapCustomer(SqliteDataReader reader)
        {
            return new Customer
            {
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                DateRegistered = DateTime.Parse(reader.GetString(reader.GetOrdinal("DateRegistered"))),
                IsActive = reader.GetInt32(reader.GetOrdinal("IsActive")) == 1
            };
        }

        // ==================== ORDER CRUD ====================
        public List<Order> GetAllOrders()
        {
            var list = new List<Order>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Orders", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapOrder(reader));
            return list;
        }

        public Order? GetOrderById(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Orders WHERE OrderId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapOrder(reader);
            return null;
        }

        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            var list = new List<Order>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Orders WHERE Status = @status", conn);
            cmd.Parameters.AddWithValue("@status", status.ToString());
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapOrder(reader));
            return list;
        }

        public Order AddOrder(Order order)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"
                INSERT INTO Orders 
                (CustomerId, CustomerName, ContactNumber, Email, ServiceType, ItemType, Quantity,
                 Items, TotalAmount, Status, OrderDate, CompletionDate, Notes, DeletedDate)
                VALUES 
                (@cid, @cname, @contact, @email, @service, @itemType, @qty,
                 @items, @total, @status, @odate, @cdate, @notes, @deletedDate);
                SELECT last_insert_rowid();";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cid", order.CustomerId);
            cmd.Parameters.AddWithValue("@cname", order.CustomerName);
            cmd.Parameters.AddWithValue("@contact", (object?)order.ContactNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)order.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@service", (object?)order.ServiceType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@itemType", (object?)order.ItemType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@qty", order.Quantity);
            cmd.Parameters.AddWithValue("@items", order.Items);
            cmd.Parameters.AddWithValue("@total", order.TotalAmount);
            cmd.Parameters.AddWithValue("@status", order.Status.ToString());
            cmd.Parameters.AddWithValue("@odate", order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@cdate", order.CompletionDate.HasValue
                ? order.CompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@notes", (object?)order.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@deletedDate", order.DeletedDate.HasValue
                ? order.DeletedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);
            order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());
            return order;
        }

        public bool UpdateOrder(Order order)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"
                UPDATE Orders SET 
                    CustomerName = @cname,
                    ContactNumber = @contact,
                    Email = @email,
                    ServiceType = @service,
                    ItemType = @itemType,
                    Quantity = @qty,
                    Items = @items,
                    TotalAmount = @total,
                    Status = @status,
                    CompletionDate = @cdate,
                    Notes = @notes,
                    DeletedDate = @deletedDate
                WHERE OrderId = @id";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cname", order.CustomerName);
            cmd.Parameters.AddWithValue("@contact", (object?)order.ContactNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)order.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@service", (object?)order.ServiceType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@itemType", (object?)order.ItemType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@qty", order.Quantity);
            cmd.Parameters.AddWithValue("@items", order.Items);
            cmd.Parameters.AddWithValue("@total", order.TotalAmount);
            cmd.Parameters.AddWithValue("@status", order.Status.ToString());
            cmd.Parameters.AddWithValue("@cdate", order.CompletionDate.HasValue
                ? order.CompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@notes", (object?)order.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@deletedDate", order.DeletedDate.HasValue
                ? order.DeletedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", order.OrderId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteOrder(int orderId)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("DELETE FROM Orders WHERE OrderId = @id", conn);
            cmd.Parameters.AddWithValue("@id", orderId);
            return cmd.ExecuteNonQuery() > 0;
        }

        private Order MapOrder(SqliteDataReader reader)
        {
            return new Order
            {
                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("ContactNumber")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                ServiceType = reader.IsDBNull(reader.GetOrdinal("ServiceType")) ? "" : reader.GetString(reader.GetOrdinal("ServiceType")),
                ItemType = reader.IsDBNull(reader.GetOrdinal("ItemType")) ? "" : reader.GetString(reader.GetOrdinal("ItemType")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                Items = reader.GetString(reader.GetOrdinal("Items")),
                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader.GetString(reader.GetOrdinal("Status"))),
                OrderDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("OrderDate"))),
                CompletionDate = reader.IsDBNull(reader.GetOrdinal("CompletionDate")) ? null : DateTime.Parse(reader.GetString(reader.GetOrdinal("CompletionDate"))),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes")),
                DeletedDate = reader.IsDBNull(reader.GetOrdinal("DeletedDate")) ? null : DateTime.Parse(reader.GetString(reader.GetOrdinal("DeletedDate")))
            };
        }

        // ==================== INVENTORY CRUD ====================
        public List<InventoryItem> GetAllInventory()
        {
            var list = new List<InventoryItem>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM InventoryItems", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapInventory(reader));
            return list;
        }

        public InventoryItem? GetInventoryItem(string itemId)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM InventoryItems WHERE ItemId = @id", conn);
            cmd.Parameters.AddWithValue("@id", itemId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapInventory(reader);
            return null;
        }

        public List<InventoryItem> GetLowStockItems()
        {
            var list = new List<InventoryItem>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM InventoryItems WHERE Quantity <= ReorderLevel OR Quantity = 0", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapInventory(reader));
            return list;
        }

        public InventoryItem AddInventoryItem(InventoryItem item)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"INSERT INTO InventoryItems (ItemId, MaterialName, Quantity, Unit, UnitPrice, ReorderLevel, LastUpdated)
                           VALUES (@id, @n, @qty, @u, @price, @reorder, @last);";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", item.ItemId);
            cmd.Parameters.AddWithValue("@n", item.MaterialName);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@u", item.Unit);
            cmd.Parameters.AddWithValue("@price", item.UnitPrice);
            cmd.Parameters.AddWithValue("@reorder", item.ReorderLevel);
            cmd.Parameters.AddWithValue("@last", item.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
            return item;
        }

        public bool UpdateInventoryItem(InventoryItem item)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"UPDATE InventoryItems SET MaterialName=@n, Quantity=@qty, Unit=@u, 
                           UnitPrice=@price, ReorderLevel=@reorder, LastUpdated=@last
                           WHERE ItemId=@id";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@n", item.MaterialName);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@u", item.Unit);
            cmd.Parameters.AddWithValue("@price", item.UnitPrice);
            cmd.Parameters.AddWithValue("@reorder", item.ReorderLevel);
            cmd.Parameters.AddWithValue("@last", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@id", item.ItemId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AdjustInventory(string itemId, decimal quantityChange)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"UPDATE InventoryItems SET Quantity = Quantity + @change, LastUpdated = @now
                           WHERE ItemId = @id";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@change", quantityChange);
            cmd.Parameters.AddWithValue("@now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@id", itemId);
            return cmd.ExecuteNonQuery() > 0;
        }

        private InventoryItem MapInventory(SqliteDataReader reader)
        {
            var item = new InventoryItem
            {
                ItemId = reader.GetString(reader.GetOrdinal("ItemId")),
                MaterialName = reader.GetString(reader.GetOrdinal("MaterialName")),
                Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                Unit = reader.GetString(reader.GetOrdinal("Unit")),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                ReorderLevel = reader.GetDecimal(reader.GetOrdinal("ReorderLevel")),
                LastUpdated = DateTime.Parse(reader.GetString(reader.GetOrdinal("LastUpdated")))
            };
            item.UpdateStatus();
            return item;
        }

        // ==================== EXPENSE CRUD ====================
        public List<Expense> GetAllExpenses()
        {
            var list = new List<Expense>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Expenses", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapExpense(reader));
            return list;
        }

        public Expense? GetExpenseById(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Expenses WHERE ExpenseId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapExpense(reader);
            return null;
        }

        public List<Expense> GetExpensesByDateRange(DateTime start, DateTime end)
        {
            var list = new List<Expense>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Expenses WHERE ExpenseDate BETWEEN @start AND @end", conn);
            cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd HH:mm:ss"));
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapExpense(reader));
            return list;
        }

        public Expense AddExpense(Expense expense)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"INSERT INTO Expenses (Description, Category, Amount, ExpenseDate, Notes)
                           VALUES (@d, @c, @a, @ed, @n);
                           SELECT last_insert_rowid();";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@d", expense.Description);
            cmd.Parameters.AddWithValue("@c", expense.Category.ToString());
            cmd.Parameters.AddWithValue("@a", expense.Amount);
            cmd.Parameters.AddWithValue("@ed", expense.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@n", (object?)expense.Notes ?? DBNull.Value);
            expense.ExpenseId = Convert.ToInt32(cmd.ExecuteScalar());
            return expense;
        }

        public bool UpdateExpense(Expense expense)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"UPDATE Expenses SET Description=@d, Category=@c, Amount=@a, ExpenseDate=@ed, Notes=@n
                           WHERE ExpenseId=@id";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@d", expense.Description);
            cmd.Parameters.AddWithValue("@c", expense.Category.ToString());
            cmd.Parameters.AddWithValue("@a", expense.Amount);
            cmd.Parameters.AddWithValue("@ed", expense.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@n", (object?)expense.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", expense.ExpenseId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteExpense(int expenseId)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("DELETE FROM Expenses WHERE ExpenseId = @id", conn);
            cmd.Parameters.AddWithValue("@id", expenseId);
            return cmd.ExecuteNonQuery() > 0;
        }

        private Expense MapExpense(SqliteDataReader reader)
        {
            return new Expense
            {
                ExpenseId = reader.GetInt32(reader.GetOrdinal("ExpenseId")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Category = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), reader.GetString(reader.GetOrdinal("Category"))),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                ExpenseDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("ExpenseDate"))),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes"))
            };
        }

        // ==================== SALE CRUD ====================
        public List<Sale> GetAllSales()
        {
            var list = new List<Sale>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Sales", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapSale(reader));
            return list;
        }

        public Sale? GetSaleById(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Sales WHERE SaleId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapSale(reader);
            return null;
        }

        public List<Sale> GetSalesByDateRange(DateTime start, DateTime end)
        {
            var list = new List<Sale>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT * FROM Sales WHERE SaleDate BETWEEN @start AND @end", conn);
            cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd HH:mm:ss"));
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapSale(reader));
            return list;
        }

        public Sale AddSale(Sale sale)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"INSERT INTO Sales (OrderId, ProductCategory, UnitsSold, Revenue, Cost, SaleDate)
                           VALUES (@oid, @cat, @units, @rev, @cost, @date);
                           SELECT last_insert_rowid();";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@oid", sale.OrderId);
            cmd.Parameters.AddWithValue("@cat", sale.ProductCategory);
            cmd.Parameters.AddWithValue("@units", sale.UnitsSold);
            cmd.Parameters.AddWithValue("@rev", sale.Revenue);
            cmd.Parameters.AddWithValue("@cost", sale.Cost);
            cmd.Parameters.AddWithValue("@date", sale.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"));
            sale.SaleId = Convert.ToInt32(cmd.ExecuteScalar());
            return sale;
        }

        private Sale MapSale(SqliteDataReader reader)
        {
            return new Sale
            {
                SaleId = reader.GetInt32(reader.GetOrdinal("SaleId")),
                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                ProductCategory = reader.GetString(reader.GetOrdinal("ProductCategory")),
                UnitsSold = reader.GetInt32(reader.GetOrdinal("UnitsSold")),
                Revenue = reader.GetDecimal(reader.GetOrdinal("Revenue")),
                Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                SaleDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("SaleDate")))
            };
        }

        // ==================== STATISTICS ====================
        public decimal GetTotalRevenue(DateTime? startDate = null, DateTime? endDate = null)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = "SELECT COALESCE(SUM(Revenue),0) FROM Sales";
            if (startDate.HasValue || endDate.HasValue)
                sql += " WHERE SaleDate BETWEEN @start AND @end";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@start", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01");
            cmd.Parameters.AddWithValue("@end", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "2099-12-31");
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public decimal GetTotalExpenses(DateTime? startDate = null, DateTime? endDate = null)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = "SELECT COALESCE(SUM(Amount),0) FROM Expenses";
            if (startDate.HasValue || endDate.HasValue)
                sql += " WHERE ExpenseDate BETWEEN @start AND @end";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@start", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01");
            cmd.Parameters.AddWithValue("@end", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "2099-12-31");
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public decimal GetTotalProfit(DateTime? startDate = null, DateTime? endDate = null)
            => GetTotalRevenue(startDate, endDate) - GetTotalExpenses(startDate, endDate);

        public int GetPendingOrdersCount()
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT COUNT(*) FROM Orders WHERE Status = 'Pending'", conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public decimal GetInventoryTotalValue()
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqliteCommand("SELECT COALESCE(SUM(Quantity * UnitPrice),0) FROM InventoryItems", conn);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }
    }
}

//using System;
//using System.Collections.Generic;
//using Microsoft.Data.Sqlite;
//using FLAVSMAGS_TAILORING.Models;

//namespace FLAVSMAGS_TAILORING.Data
//{
//    public sealed class DatabaseManager
//    {
//        private static DatabaseManager? _instance;
//        private static readonly object _lock = new object();

//        private DatabaseManager() { }

//        public static DatabaseManager Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    lock (_lock)
//                    {
//                        if (_instance == null)
//                            _instance = new DatabaseManager();
//                    }
//                }
//                return _instance;
//            }
//        }

//        // ---------- CUSTOMER CRUD (unchanged) ----------
//        // (Keep your existing customer methods here)

//        // ---------- ORDER CRUD (UPDATED with DeletedDate) ----------
//        public List<Order> GetAllOrders()
//        {
//            var list = new List<Order>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Orders", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    list.Add(MapOrder(reader));
//            }
//            return list;
//        }

//        public Order? GetOrderById(int id)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Orders WHERE OrderId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", id);
//                using (var reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                        return MapOrder(reader);
//                }
//            }
//            return null;
//        }

//        public List<Order> GetOrdersByStatus(OrderStatus status)
//        {
//            var list = new List<Order>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Orders WHERE Status = @status", conn))
//            {
//                cmd.Parameters.AddWithValue("@status", status.ToString());
//                using (var reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                        list.Add(MapOrder(reader));
//                }
//            }
//            return list;
//        }

//        public Order AddOrder(Order order)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"
//                    INSERT INTO Orders 
//                    (CustomerId, CustomerName, ContactNumber, Email, ServiceType, ItemType, Quantity,
//                     Items, TotalAmount, Status, OrderDate, CompletionDate, Notes, DeletedDate)
//                    VALUES 
//                    (@cid, @cname, @contact, @email, @service, @itemType, @qty,
//                     @items, @total, @status, @odate, @cdate, @notes, @deletedDate);
//                    SELECT last_insert_rowid();";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@cid", order.CustomerId);
//                    cmd.Parameters.AddWithValue("@cname", order.CustomerName);
//                    cmd.Parameters.AddWithValue("@contact", (object?)order.ContactNumber ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@email", (object?)order.Email ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@service", (object?)order.ServiceType ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@itemType", (object?)order.ItemType ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@qty", order.Quantity);
//                    cmd.Parameters.AddWithValue("@items", order.Items);
//                    cmd.Parameters.AddWithValue("@total", order.TotalAmount);
//                    cmd.Parameters.AddWithValue("@status", order.Status.ToString());
//                    cmd.Parameters.AddWithValue("@odate", order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.Parameters.AddWithValue("@cdate", order.CompletionDate.HasValue
//                        ? order.CompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
//                        : (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@notes", (object?)order.Notes ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@deletedDate", order.DeletedDate.HasValue
//                        ? order.DeletedDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
//                        : (object)DBNull.Value);
//                    order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());
//                }
//            }
//            return order;
//        }

//        public bool UpdateOrder(Order order)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"
//                    UPDATE Orders SET 
//                        CustomerName = @cname,
//                        ContactNumber = @contact,
//                        Email = @email,
//                        ServiceType = @service,
//                        ItemType = @itemType,
//                        Quantity = @qty,
//                        Items = @items,
//                        TotalAmount = @total,
//                        Status = @status,
//                        CompletionDate = @cdate,
//                        Notes = @notes,
//                        DeletedDate = @deletedDate
//                    WHERE OrderId = @id";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@cname", order.CustomerName);
//                    cmd.Parameters.AddWithValue("@contact", (object?)order.ContactNumber ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@email", (object?)order.Email ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@service", (object?)order.ServiceType ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@itemType", (object?)order.ItemType ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@qty", order.Quantity);
//                    cmd.Parameters.AddWithValue("@items", order.Items);
//                    cmd.Parameters.AddWithValue("@total", order.TotalAmount);
//                    cmd.Parameters.AddWithValue("@status", order.Status.ToString());
//                    cmd.Parameters.AddWithValue("@cdate", order.CompletionDate.HasValue
//                        ? order.CompletionDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
//                        : (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@notes", (object?)order.Notes ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@deletedDate", order.DeletedDate.HasValue
//                        ? order.DeletedDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
//                        : (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@id", order.OrderId);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        // Physical delete (for permanent removal) – remains available
//        public bool DeleteOrder(int orderId)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("DELETE FROM Orders WHERE OrderId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", orderId);
//                return cmd.ExecuteNonQuery() > 0;
//            }
//        }

//        // Updated MapOrder: reads all 15 columns (0‑14) including DeletedDate
//        private Order MapOrder(SqliteDataReader reader)
//        {
//            return new Order
//            {
//                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
//                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
//                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
//                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber"))
//                    ? string.Empty
//                    : reader.GetString(reader.GetOrdinal("ContactNumber")),
//                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
//                    ? string.Empty
//                    : reader.GetString(reader.GetOrdinal("Email")),
//                ServiceType = reader.IsDBNull(reader.GetOrdinal("ServiceType"))
//                    ? string.Empty
//                    : reader.GetString(reader.GetOrdinal("ServiceType")),
//                ItemType = reader.IsDBNull(reader.GetOrdinal("ItemType"))
//                    ? string.Empty
//                    : reader.GetString(reader.GetOrdinal("ItemType")),
//                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
//                Items = reader.GetString(reader.GetOrdinal("Items")),
//                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
//                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
//                    reader.GetString(reader.GetOrdinal("Status"))),
//                OrderDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("OrderDate"))),
//                CompletionDate = reader.IsDBNull(reader.GetOrdinal("CompletionDate"))
//                    ? (DateTime?)null
//                    : DateTime.Parse(reader.GetString(reader.GetOrdinal("CompletionDate"))),
//                Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
//                    ? string.Empty
//                    : reader.GetString(reader.GetOrdinal("Notes")),
//                DeletedDate = reader.IsDBNull(reader.GetOrdinal("DeletedDate"))
//                    ? (DateTime?)null
//                    : DateTime.Parse(reader.GetString(reader.GetOrdinal("DeletedDate")))
//            };
//        }

//        // ---------- INVENTORY CRUD (unchanged) ----------
//        public List<InventoryItem> GetAllInventory()
//        {
//            var list = new List<InventoryItem>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM InventoryItems", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    list.Add(MapInventory(reader));
//            }
//            return list;
//        }

//        public InventoryItem? GetInventoryItem(string itemId)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM InventoryItems WHERE ItemId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", itemId);
//                using (var reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                        return MapInventory(reader);
//                }
//            }
//            return null;
//        }

//        public List<InventoryItem> GetLowStockItems()
//        {
//            var list = new List<InventoryItem>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM InventoryItems WHERE Quantity <= ReorderLevel OR Quantity = 0", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    list.Add(MapInventory(reader));
//            }
//            return list;
//        }

//        public InventoryItem AddInventoryItem(InventoryItem item)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"INSERT INTO InventoryItems (ItemId, MaterialName, Quantity, Unit, UnitPrice, ReorderLevel, LastUpdated)
//                               VALUES (@id, @name, @qty, @unit, @price, @reorder, @lastUpd)";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@id", item.ItemId);
//                    cmd.Parameters.AddWithValue("@name", item.MaterialName);
//                    cmd.Parameters.AddWithValue("@qty", item.Quantity);
//                    cmd.Parameters.AddWithValue("@unit", item.Unit);
//                    cmd.Parameters.AddWithValue("@price", item.UnitPrice);
//                    cmd.Parameters.AddWithValue("@reorder", item.ReorderLevel);
//                    cmd.Parameters.AddWithValue("@lastUpd", item.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            return item;
//        }

//        public bool UpdateInventoryItem(InventoryItem item)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"UPDATE InventoryItems SET MaterialName=@n, Quantity=@qty, Unit=@unit, 
//                               UnitPrice=@price, ReorderLevel=@reorder, LastUpdated=@last
//                               WHERE ItemId=@id";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@n", item.MaterialName);
//                    cmd.Parameters.AddWithValue("@qty", item.Quantity);
//                    cmd.Parameters.AddWithValue("@unit", item.Unit);
//                    cmd.Parameters.AddWithValue("@price", item.UnitPrice);
//                    cmd.Parameters.AddWithValue("@reorder", item.ReorderLevel);
//                    cmd.Parameters.AddWithValue("@last", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.Parameters.AddWithValue("@id", item.ItemId);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        public bool AdjustInventory(string itemId, decimal quantityChange)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"UPDATE InventoryItems SET Quantity = Quantity + @change, LastUpdated = @now
//                               WHERE ItemId = @id";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@change", quantityChange);
//                    cmd.Parameters.AddWithValue("@now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.Parameters.AddWithValue("@id", itemId);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        private InventoryItem MapInventory(SqliteDataReader reader)
//        {
//            var item = new InventoryItem
//            {
//                ItemId = reader.GetString(0),
//                MaterialName = reader.GetString(1),
//                Quantity = reader.GetDecimal(2),
//                Unit = reader.GetString(3),
//                UnitPrice = reader.GetDecimal(4),
//                ReorderLevel = reader.GetDecimal(5),
//                LastUpdated = DateTime.Parse(reader.GetString(6))
//            };
//            item.UpdateStatus();
//            return item;
//        }

//        // ---------- EXPENSE CRUD (unchanged) ----------
//        public List<Expense> GetAllExpenses()
//        {
//            var list = new List<Expense>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Expenses", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    list.Add(MapExpense(reader));
//            }
//            return list;
//        }

//        public Expense? GetExpenseById(int id)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Expenses WHERE ExpenseId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", id);
//                using (var reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                        return MapExpense(reader);
//                }
//            }
//            return null;
//        }

//        public List<Expense> GetExpensesByDateRange(DateTime start, DateTime end)
//        {
//            var list = new List<Expense>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Expenses WHERE ExpenseDate BETWEEN @start AND @end", conn))
//            {
//                cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd HH:mm:ss"));
//                cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd HH:mm:ss"));
//                using (var reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                        list.Add(MapExpense(reader));
//                }
//            }
//            return list;
//        }

//        public Expense AddExpense(Expense expense)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"INSERT INTO Expenses (Description, Category, Amount, ExpenseDate, Notes)
//                               VALUES (@desc, @cat, @amt, @date, @notes);
//                               SELECT last_insert_rowid();";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@desc", expense.Description);
//                    cmd.Parameters.AddWithValue("@cat", expense.Category.ToString());
//                    cmd.Parameters.AddWithValue("@amt", expense.Amount);
//                    cmd.Parameters.AddWithValue("@date", expense.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrEmpty(expense.Notes) ? (object)DBNull.Value : expense.Notes);
//                    expense.ExpenseId = Convert.ToInt32(cmd.ExecuteScalar());
//                }
//            }
//            return expense;
//        }

//        public bool UpdateExpense(Expense expense)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"UPDATE Expenses SET Description=@d, Category=@c, Amount=@a, ExpenseDate=@ed, Notes=@n
//                               WHERE ExpenseId=@id";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@d", expense.Description);
//                    cmd.Parameters.AddWithValue("@c", expense.Category.ToString());
//                    cmd.Parameters.AddWithValue("@a", expense.Amount);
//                    cmd.Parameters.AddWithValue("@ed", expense.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
//                    cmd.Parameters.AddWithValue("@n", string.IsNullOrEmpty(expense.Notes) ? (object)DBNull.Value : expense.Notes);
//                    cmd.Parameters.AddWithValue("@id", expense.ExpenseId);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        public bool DeleteExpense(int expenseId)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("DELETE FROM Expenses WHERE ExpenseId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", expenseId);
//                return cmd.ExecuteNonQuery() > 0;
//            }
//        }

//        private Expense MapExpense(SqliteDataReader reader)
//        {
//            return new Expense
//            {
//                ExpenseId = reader.GetInt32(0),
//                Description = reader.GetString(1),
//                Category = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), reader.GetString(2)),
//                Amount = reader.GetDecimal(3),
//                ExpenseDate = DateTime.Parse(reader.GetString(4)),
//                Notes = reader.IsDBNull(5) ? "" : reader.GetString(5)
//            };
//        }

//        // ---------- SALE CRUD (unchanged) ----------
//        public List<Sale> GetAllSales()
//        {
//            var list = new List<Sale>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Sales", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    list.Add(MapSale(reader));
//            }
//            return list;
//        }

//        public Sale? GetSaleById(int id)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Sales WHERE SaleId = @id", conn))
//            {
//                cmd.Parameters.AddWithValue("@id", id);
//                using (var reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                        return MapSale(reader);
//                }
//            }
//            return null;
//        }

//        public List<Sale> GetSalesByDateRange(DateTime start, DateTime end)
//        {
//            var list = new List<Sale>();
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT * FROM Sales WHERE SaleDate BETWEEN @start AND @end", conn))
//            {
//                cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd HH:mm:ss"));
//                cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd HH:mm:ss"));
//                using (var reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                        list.Add(MapSale(reader));
//                }
//            }
//            return list;
//        }

//        public Sale AddSale(Sale sale)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = @"INSERT INTO Sales (OrderId, ProductCategory, UnitsSold, Revenue, Cost, SaleDate)
//                               VALUES (@oid, @cat, @units, @rev, @cost, @date);
//                               SELECT last_insert_rowid();";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@oid", sale.OrderId);
//                    cmd.Parameters.AddWithValue("@cat", sale.ProductCategory);
//                    cmd.Parameters.AddWithValue("@units", sale.UnitsSold);
//                    cmd.Parameters.AddWithValue("@rev", sale.Revenue);
//                    cmd.Parameters.AddWithValue("@cost", sale.Cost);
//                    cmd.Parameters.AddWithValue("@date", sale.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"));
//                    sale.SaleId = Convert.ToInt32(cmd.ExecuteScalar());
//                }
//            }
//            return sale;
//        }

//        private Sale MapSale(SqliteDataReader reader)
//        {
//            return new Sale
//            {
//                SaleId = reader.GetInt32(0),
//                OrderId = reader.GetInt32(1),
//                ProductCategory = reader.GetString(2),
//                UnitsSold = reader.GetInt32(3),
//                Revenue = reader.GetDecimal(4),
//                Cost = reader.GetDecimal(5),
//                SaleDate = DateTime.Parse(reader.GetString(6))
//            };
//        }

//        // ---------- STATISTICS (unchanged) ----------
//        public decimal GetTotalRevenue(DateTime? startDate = null, DateTime? endDate = null)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = "SELECT COALESCE(SUM(Revenue),0) FROM Sales";
//                if (startDate.HasValue || endDate.HasValue)
//                {
//                    sql += " WHERE SaleDate BETWEEN @start AND @end";
//                }
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    if (startDate.HasValue)
//                        cmd.Parameters.AddWithValue("@start", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
//                    else
//                        cmd.Parameters.AddWithValue("@start", "1900-01-01");
//                    if (endDate.HasValue)
//                        cmd.Parameters.AddWithValue("@end", endDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
//                    else
//                        cmd.Parameters.AddWithValue("@end", "2099-12-31");
//                    return Convert.ToDecimal(cmd.ExecuteScalar());
//                }
//            }
//        }

//        public decimal GetTotalExpenses(DateTime? startDate = null, DateTime? endDate = null)
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            {
//                string sql = "SELECT COALESCE(SUM(Amount),0) FROM Expenses";
//                if (startDate.HasValue || endDate.HasValue)
//                    sql += " WHERE ExpenseDate BETWEEN @start AND @end";
//                using (var cmd = new SqliteCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@start", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01");
//                    cmd.Parameters.AddWithValue("@end", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "2099-12-31");
//                    return Convert.ToDecimal(cmd.ExecuteScalar());
//                }
//            }
//        }

//        public decimal GetTotalProfit(DateTime? startDate = null, DateTime? endDate = null)
//            => GetTotalRevenue(startDate, endDate) - GetTotalExpenses(startDate, endDate);

//        public int GetPendingOrdersCount()
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT COUNT(*) FROM Orders WHERE Status = 'Pending'", conn))
//                return Convert.ToInt32(cmd.ExecuteScalar());
//        }

//        public decimal GetInventoryTotalValue()
//        {
//            using (var conn = DatabaseHelper.GetConnection())
//            using (var cmd = new SqliteCommand("SELECT COALESCE(SUM(Quantity * UnitPrice),0) FROM InventoryItems", conn))
//                return Convert.ToDecimal(cmd.ExecuteScalar());
//        }
//    }
//}