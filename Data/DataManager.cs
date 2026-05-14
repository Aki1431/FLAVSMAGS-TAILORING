using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;


namespace FLAVSMAGS_TAILORING.Data
{
    /// <summary>
    /// Singleton Data Manager - Central data repository
    /// In-memory storage (can be upgraded to database later)
    /// </summary>
    public sealed class DataManager
    {
        private static DataManager? _instance;
        private static readonly object _lock = new object();

        // Data collections
        private List<Customer> _customers;
        private List<Order> _orders;
        private List<InventoryItem> _inventory;
        private List<Expense> _expenses;
        private List<Sale> _sales;

        // Auto-increment IDs
        private int _nextCustomerId = 1001;
        private int _nextOrderId = 1001;
        private int _nextExpenseId = 1001;
        private int _nextSaleId = 1001;
        private int _nextItemId = 1;

        private DataManager()
        {
            _customers = new List<Customer>();
            _orders = new List<Order>();
            _inventory = new List<InventoryItem>();
            _expenses = new List<Expense>();
            _sales = new List<Sale>();

            // Initialize with sample data
            InitializeSampleData();
        }

        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataManager();
                        }
                    }
                }
                return _instance;
            }
        }

        #region Customer Operations

        public List<Customer> GetAllCustomers() => _customers.ToList();

        public Customer? GetCustomerById(int id) => _customers.FirstOrDefault(c => c.CustomerId == id);

        public Customer AddCustomer(Customer customer)
        {
            customer.CustomerId = _nextCustomerId++;
            _customers.Add(customer);
            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var existing = GetCustomerById(customer.CustomerId);
            if (existing == null) return false;

            existing.CustomerName = customer.CustomerName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.Address = customer.Address;
            existing.IsActive = customer.IsActive;
            return true;
        }

        #endregion

        #region Order Operations

        public List<Order> GetAllOrders() => _orders.ToList();

        public Order? GetOrderById(int id) => _orders.FirstOrDefault(o => o.OrderId == id);

        public List<Order> GetOrdersByStatus(OrderStatus status) =>
            _orders.Where(o => o.Status == status).ToList();

        public Order AddOrder(Order order)
        {
            order.OrderId = _nextOrderId++;
            _orders.Add(order);
            return order;
        }

        public bool UpdateOrder(Order order)
        {
            var existing = GetOrderById(order.OrderId);
            if (existing == null) return false;

            existing.CustomerName = order.CustomerName;
            existing.Items = order.Items;
            existing.TotalAmount = order.TotalAmount;
            existing.Status = order.Status;
            existing.CompletionDate = order.CompletionDate;
            existing.Notes = order.Notes;
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order == null) return false;
            return _orders.Remove(order);
        }

        #endregion

        #region Inventory Operations

        public List<InventoryItem> GetAllInventory() => _inventory.ToList();

        public InventoryItem? GetInventoryItem(string itemId) =>
            _inventory.FirstOrDefault(i => i.ItemId == itemId);

        public List<InventoryItem> GetLowStockItems() =>
            _inventory.Where(i => i.Status == StockStatus.LowStock ||
                                  i.Status == StockStatus.OutOfStock).ToList();

        public InventoryItem AddInventoryItem(InventoryItem item)
        {
            item.ItemId = $"MAT-{_nextItemId++:D3}";
            item.UpdateStatus();
            _inventory.Add(item);
            return item;
        }

        public bool UpdateInventoryItem(InventoryItem item)
        {
            var existing = GetInventoryItem(item.ItemId);
            if (existing == null) return false;

            existing.MaterialName = item.MaterialName;
            existing.Quantity = item.Quantity;
            existing.Unit = item.Unit;
            existing.UnitPrice = item.UnitPrice;
            existing.ReorderLevel = item.ReorderLevel;
            existing.LastUpdated = DateTime.Now;
            existing.UpdateStatus();
            return true;
        }

        public bool AdjustInventory(string itemId, decimal quantityChange)
        {
            var item = GetInventoryItem(itemId);
            if (item == null) return false;

            item.Quantity += quantityChange;
            if (item.Quantity < 0) item.Quantity = 0;
            item.LastUpdated = DateTime.Now;
            item.UpdateStatus();
            return true;
        }

        #endregion

        #region Expense Operations

        public List<Expense> GetAllExpenses() => _expenses.ToList();

        public Expense? GetExpenseById(int id) => _expenses.FirstOrDefault(e => e.ExpenseId == id);

        public List<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate) =>
            _expenses.Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate).ToList();

        public Expense AddExpense(Expense expense)
        {
            expense.ExpenseId = _nextExpenseId++;
            _expenses.Add(expense);
            return expense;
        }

        public bool UpdateExpense(Expense expense)
        {
            var existing = GetExpenseById(expense.ExpenseId);
            if (existing == null) return false;

            existing.Description = expense.Description;
            existing.Category = expense.Category;
            existing.Amount = expense.Amount;
            existing.ExpenseDate = expense.ExpenseDate;
            existing.Notes = expense.Notes;
            return true;
        }

        public bool DeleteExpense(int expenseId)
        {
            var expense = GetExpenseById(expenseId);
            if (expense == null) return false;
            return _expenses.Remove(expense);
        }

        #endregion

        #region Sale Operations

        public List<Sale> GetAllSales() => _sales.ToList();

        public Sale? GetSaleById(int id) => _sales.FirstOrDefault(s => s.SaleId == id);

        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate) =>
            _sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate).ToList();

        public Sale AddSale(Sale sale)
        {
            sale.SaleId = _nextSaleId++;
            _sales.Add(sale);
            return sale;
        }

        #endregion

        #region Statistics & Analytics

        public decimal GetTotalRevenue(DateTime? startDate = null, DateTime? endDate = null)
        {
            var sales = _sales.AsEnumerable();
            if (startDate.HasValue)
                sales = sales.Where(s => s.SaleDate >= startDate.Value);
            if (endDate.HasValue)
                sales = sales.Where(s => s.SaleDate <= endDate.Value);
            return sales.Sum(s => s.Revenue);
        }

        public decimal GetTotalExpenses(DateTime? startDate = null, DateTime? endDate = null)
        {
            var expenses = _expenses.AsEnumerable();
            if (startDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate >= startDate.Value);
            if (endDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate <= endDate.Value);
            return expenses.Sum(e => e.Amount);
        }

        public decimal GetTotalProfit(DateTime? startDate = null, DateTime? endDate = null)
        {
            return GetTotalRevenue(startDate, endDate) - GetTotalExpenses(startDate, endDate);
        }

        public int GetPendingOrdersCount() => _orders.Count(o => o.Status == OrderStatus.Pending);

        public decimal GetInventoryTotalValue() => _inventory.Sum(i => i.TotalValue);

        #endregion

        #region Sample Data Initialization

        /// <summary>
        /// Initialize with ONE clean sample dataset
        /// </summary>
        private void InitializeSampleData()
        {
            // Sample Customers
            var customer1 = AddCustomer(new Customer
            {
                CustomerName = "John Doe",
                Email = "john@email.com",
                Phone = "123-456-7890"
            });

            // Sample Inventory
            AddInventoryItem(new InventoryItem
            {
                MaterialName = "Fabric - Cotton",
                Quantity = 45,
                Unit = "meters",
                UnitPrice = 5.00m
            });

            AddInventoryItem(new InventoryItem
            {
                MaterialName = "Thread - Red",
                Quantity = 120,
                Unit = "pcs",
                UnitPrice = 2.00m
            });

            // Sample Orders
            AddOrder(new Order
            {
                CustomerId = customer1.CustomerId,
                CustomerName = customer1.CustomerName,
                Items = "2 pcs",
                TotalAmount = 150.00m,
                Status = OrderStatus.Pending
            });

            // Sample Expenses
            AddExpense(new Expense
            {
                Description = "Monthly Rent",
                Category = ExpenseCategory.Rent,
                Amount = 1500.00m
            });

            // Sample Sales
            AddSale(new Sale
            {
                OrderId = 1001,
                ProductCategory = "Shirts",
                UnitsSold = 2,
                Revenue = 150.00m,
                Cost = 50.00m
            });
        }

        #endregion
    }
}
