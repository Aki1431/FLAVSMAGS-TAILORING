using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Data;

namespace FLAVSMAGS_TAILORING.Services
{
    /// <summary>
    /// Service layer for Report generation
    /// Aggregates data from all services for comprehensive business reports
    /// </summary>
    public class ReportService
    {
        private readonly DataManager _dataManager;
        private readonly OrderService _orderService;
        private readonly SalesService _salesService;
        private readonly InventoryService _inventoryService;
        private readonly ExpenseService _expenseService;

        public ReportService()
        {
            _dataManager = DataManager.Instance;
            _orderService = new OrderService();
            _salesService = new SalesService();
            _inventoryService = new InventoryService();
            _expenseService = new ExpenseService();
        }

        #region Report Generation

        /// <summary>
        /// Generate Orders Report
        /// </summary>
        public DataTable GenerateOrdersReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            var orders = _orderService.GetAllOrders();

            // Apply date filter if provided
            if (startDate.HasValue)
                orders = orders.Where(o => o.OrderDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                orders = orders.Where(o => o.OrderDate <= endDate.Value).ToList();

            // Create DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Order ID", typeof(string));
            dt.Columns.Add("Customer Name", typeof(string));
            dt.Columns.Add("Items", typeof(string));
            dt.Columns.Add("Total Amount", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Order Date", typeof(string));

            foreach (var order in orders.OrderByDescending(o => o.OrderDate))
            {
                dt.Rows.Add(
                    $"ORD-{order.OrderId}",
                    order.CustomerName,
                    order.Items,
                    $"${order.TotalAmount:F2}",
                    order.Status.ToString(),
                    order.OrderDate.ToString("MM/dd/yyyy")
                );
            }

            return dt;
        }

        /// <summary>
        /// Generate Sales Report
        /// </summary>
        public DataTable GenerateSalesReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            var sales = _salesService.GetAllSales();
            var orders = _orderService.GetAllOrders();

            // Apply date filter
            if (startDate.HasValue)
                sales = sales.Where(s => s.SaleDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                sales = sales.Where(s => s.SaleDate <= endDate.Value).ToList();

            // Create DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Order ID", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Units", typeof(string));
            dt.Columns.Add("Revenue", typeof(string));
            dt.Columns.Add("Profit", typeof(string));

            foreach (var sale in sales.OrderByDescending(s => s.SaleDate))
            {
                dt.Rows.Add(
                    sale.SaleDate.ToString("MM/dd/yyyy"),
                    $"ORD-{sale.OrderId}",
                    sale.ProductCategory,
                    sale.UnitsSold.ToString(),
                    $"${sale.Revenue:F2}",
                    $"${sale.Profit:F2}"
                );
            }

            return dt;
        }

        /// <summary>
        /// Generate Inventory Report
        /// </summary>
        public DataTable GenerateInventoryReport()
        {
            var inventory = _inventoryService.GetAllInventory();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Material Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Unit Price", typeof(string));
            dt.Columns.Add("Total Value", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            foreach (var item in inventory.OrderBy(i => i.MaterialName))
            {
                string status = item.Status switch
                {
                    StockStatus.InStock => "In Stock",
                    StockStatus.LowStock => "Low Stock",
                    StockStatus.OutOfStock => "Out of Stock",
                    _ => "Unknown"
                };

                dt.Rows.Add(
                    item.ItemId,
                    item.MaterialName,
                    $"{item.Quantity} {item.Unit}",
                    $"${item.UnitPrice:F2}",
                    $"${item.TotalValue:F2}",
                    status
                );
            }

            return dt;
        }

        /// <summary>
        /// Generate Expenses Report
        /// </summary>
        public DataTable GenerateExpensesReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            var expenses = _expenseService.GetAllExpenses();

            // Apply date filter
            if (startDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                expenses = expenses.Where(e => e.ExpenseDate <= endDate.Value).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Amount", typeof(string));

            foreach (var expense in expenses.OrderByDescending(e => e.ExpenseDate))
            {
                dt.Rows.Add(
                    expense.ExpenseDate.ToString("MM/dd/yyyy"),
                    expense.Description,
                    expense.Category.ToString(),
                    $"${expense.Amount:F2}"
                );
            }

            return dt;
        }

        /// <summary>
        /// Generate Financial Summary Report
        /// </summary>
        public FinancialSummary GenerateFinancialSummary(DateTime? startDate = null, DateTime? endDate = null)
        {
            var start = startDate ?? DateTime.Now.AddMonths(-1);
            var end = endDate ?? DateTime.Now;

            var totalRevenue = _dataManager.GetTotalRevenue(start, end);
            var totalExpenses = _dataManager.GetTotalExpenses(start, end);
            var totalProfit = totalRevenue - totalExpenses;

            var orders = _orderService.GetAllOrders()
                .Where(o => o.OrderDate >= start && o.OrderDate <= end).ToList();

            var inventory = _inventoryService.GetAllInventory();

            return new FinancialSummary
            {
                StartDate = start,
                EndDate = end,
                TotalRevenue = totalRevenue,
                TotalExpenses = totalExpenses,
                NetProfit = totalProfit,
                OrdersCompleted = orders.Count(o => o.Status == OrderStatus.Completed),
                InventoryValue = inventory.Sum(i => i.TotalValue),
                ProfitMargin = totalRevenue > 0 ? (totalProfit / totalRevenue) * 100 : 0
            };
        }

        #endregion

        #region Dashboard Metrics

        /// <summary>
        /// Get all metrics for dashboard display
        /// </summary>
        public DashboardMetrics GetDashboardMetrics()
        {
            var orderStats = _orderService.GetStatistics();
            var salesStats = _salesService.GetStatistics();
            var inventoryStats = _inventoryService.GetStatistics();
            var expenseStats = _expenseService.GetStatistics();

            return new DashboardMetrics
            {
                TodaysSales = CalculateTodaysSales(),
                MonthlySales = salesStats.TotalSales,
                TotalCustomers = CalculateTotalCustomers(),
                PendingOrders = orderStats.PendingOrders,
                TotalRevenue = salesStats.TotalSales,
                TotalExpenses = expenseStats.TotalExpenses,
                NetProfit = salesStats.TotalSales - expenseStats.TotalExpenses,
                InventoryValue = inventoryStats.TotalValue,
                LowStockCount = inventoryStats.LowStockCount
            };
        }

        /// <summary>
        /// Calculate today's sales total
        /// </summary>
        private decimal CalculateTodaysSales()
        {
            var today = DateTime.Now.Date;
            var sales = _salesService.GetSalesByDateRange(today, today.AddDays(1));
            return sales.Sum(s => s.Revenue);
        }

        /// <summary>
        /// Calculate total unique customers
        /// </summary>
        private int CalculateTotalCustomers()
        {
            var orders = _orderService.GetAllOrders();
            return orders.Select(o => o.CustomerName).Distinct().Count();
        }

        #endregion
    }

    #region Helper Classes

    /// <summary>
    /// Financial summary DTO
    /// </summary>
    public class FinancialSummary
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public int OrdersCompleted { get; set; }
        public decimal InventoryValue { get; set; }
        public decimal ProfitMargin { get; set; }
    }

    /// <summary>
    /// Dashboard metrics DTO
    /// </summary>
    public class DashboardMetrics
    {
        public decimal TodaysSales { get; set; }
        public decimal MonthlySales { get; set; }
        public int TotalCustomers { get; set; }
        public int PendingOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public decimal InventoryValue { get; set; }
        public int LowStockCount { get; set; }
    }

    #endregion
}