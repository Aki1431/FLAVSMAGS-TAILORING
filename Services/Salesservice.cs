using System;
using System.Collections.Generic;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Data;

namespace FLAVSMAGS_TAILORING.Services
{
    /// <summary>
    /// Service layer for Sales and Revenue management
    /// Handles sales tracking, profit calculation, and analytics
    /// </summary>
    public class SalesService : IBaseService
    {
        private readonly DataManager _dataManager;

        public event EventHandler? DataChanged;

        public SalesService()
        {
            _dataManager = DataManager.Instance;
        }

        #region Sales Operations

        /// <summary>
        /// Record a new sale
        /// </summary>
        public Result<Sale> RecordSale(int orderId, string productCategory, int unitsSold,
            decimal revenue, decimal cost)
        {
            // Validation
            if (unitsSold <= 0)
                return Result<Sale>.Failure("Units sold must be greater than zero");

            if (revenue <= 0)
                return Result<Sale>.Failure("Revenue must be greater than zero");

            if (cost < 0)
                return Result<Sale>.Failure("Cost cannot be negative");

            if (string.IsNullOrWhiteSpace(productCategory))
                return Result<Sale>.Failure("Product category is required");

            try
            {
                var sale = new Sale
                {
                    OrderId = orderId,
                    ProductCategory = productCategory.Trim(),
                    UnitsSold = unitsSold,
                    Revenue = revenue,
                    Cost = cost,
                    SaleDate = DateTime.Now
                };

                var createdSale = _dataManager.AddSale(sale);
                OnDataChanged();

                return Result<Sale>.Success(createdSale);
            }
            catch (Exception ex)
            {
                return Result<Sale>.Failure($"Failed to record sale: {ex.Message}");
            }
        }

        /// <summary>
        /// Record sale from completed order automatically
        /// </summary>
        public Result<Sale> RecordSaleFromOrder(Order order, string productCategory,
            int unitsSold, decimal cost)
        {
            if (order.Status != OrderStatus.Completed)
                return Result<Sale>.Failure("Can only record sales for completed orders");

            return RecordSale(order.OrderId, productCategory, unitsSold, order.TotalAmount, cost);
        }

        #endregion

        #region Query & Analytics

        /// <summary>
        /// Get all sales
        /// </summary>
        public List<Sale> GetAllSales()
        {
            return _dataManager.GetAllSales();
        }

        /// <summary>
        /// Get sales by date range
        /// </summary>
        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dataManager.GetSalesByDateRange(startDate, endDate);
        }

        /// <summary>
        /// Get comprehensive sales statistics
        /// </summary>
        public SalesStatistics GetStatistics()
        {
            var allSales = _dataManager.GetAllSales();

            // Current period (last 30 days)
            var last30Days = DateTime.Now.AddDays(-30);
            var currentSales = allSales.Where(s => s.SaleDate >= last30Days).ToList();

            // Previous period (30 days before that)
            var last60Days = DateTime.Now.AddDays(-60);
            var previousSales = allSales.Where(s => s.SaleDate >= last60Days && s.SaleDate < last30Days).ToList();

            var currentRevenue = currentSales.Sum(s => s.Revenue);
            var previousRevenue = previousSales.Sum(s => s.Revenue);
            var currentProfit = currentSales.Sum(s => s.Profit);

            // Calculate growth percentages
            decimal revenueGrowth = CalculatePercentChange(previousRevenue, currentRevenue);

            return new SalesStatistics
            {
                TotalSales = currentRevenue,
                TotalProfit = currentProfit,
                RevenueGrowth = revenueGrowth,
                AverageOrderValue = currentSales.Any() ? currentRevenue / currentSales.Count : 0,
                ConversionRate = CalculateConversionRate(),
                SalesByCategory = GetSalesByCategory(currentSales),
                TopProducts = GetTopProducts(currentSales)
            };
        }

        /// <summary>
        /// Get revenue trend data for charts (last 6 months)
        /// </summary>
        public Dictionary<string, decimal> GetRevenueTrend()
        {
            var result = new Dictionary<string, decimal>();
            var sales = _dataManager.GetAllSales();

            for (int i = 5; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var monthName = date.ToString("MMM");

                var monthRevenue = sales.Where(s =>
                    s.SaleDate.Year == date.Year &&
                    s.SaleDate.Month == date.Month)
                    .Sum(s => s.Revenue);

                result[monthName] = monthRevenue;
            }

            return result;
        }

        /// <summary>
        /// Get profit trend data for charts (last 6 months)
        /// </summary>
        public Dictionary<string, decimal> GetProfitTrend()
        {
            var result = new Dictionary<string, decimal>();
            var sales = _dataManager.GetAllSales();

            for (int i = 5; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var monthName = date.ToString("MMM");

                var monthProfit = sales.Where(s =>
                    s.SaleDate.Year == date.Year &&
                    s.SaleDate.Month == date.Month)
                    .Sum(s => s.Profit);

                result[monthName] = monthProfit;
            }

            return result;
        }

        /// <summary>
        /// Get sales distribution by category (for pie chart)
        /// </summary>
        private Dictionary<string, decimal> GetSalesByCategory(List<Sale> sales)
        {
            return sales.GroupBy(s => s.ProductCategory)
                       .ToDictionary(g => g.Key, g => g.Sum(s => s.Revenue));
        }

        /// <summary>
        /// Get top performing products
        /// </summary>
        private List<TopProduct> GetTopProducts(List<Sale> sales)
        {
            return sales.GroupBy(s => s.ProductCategory)
                       .Select(g => new TopProduct
                       {
                           ProductName = g.Key,
                           UnitsSold = g.Sum(s => s.UnitsSold),
                           Revenue = g.Sum(s => s.Revenue)
                       })
                       .OrderByDescending(p => p.Revenue)
                       .Take(5)
                       .ToList();
        }

        /// <summary>
        /// Calculate conversion rate (placeholder - needs order tracking)
        /// </summary>
        private decimal CalculateConversionRate()
        {
            // In real app, track visits/inquiries vs completed orders
            // For now, return a realistic estimate
            return 24.8m;
        }

        /// <summary>
        /// Calculate percentage change between two values
        /// </summary>
        private decimal CalculatePercentChange(decimal oldValue, decimal newValue)
        {
            if (oldValue == 0) return 0;
            return ((newValue - oldValue) / oldValue) * 100;
        }

        #endregion

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Sales statistics DTO
    /// </summary>
    public class SalesStatistics
    {
        public decimal TotalSales { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal RevenueGrowth { get; set; }
        public decimal AverageOrderValue { get; set; }
        public decimal ConversionRate { get; set; }
        public Dictionary<string, decimal> SalesByCategory { get; set; }
            = new Dictionary<string, decimal>();
        public List<TopProduct> TopProducts { get; set; } = new List<TopProduct>();
    }

    /// <summary>
    /// Top product DTO
    /// </summary>
    public class TopProduct
    {
        public string ProductName { get; set; } = string.Empty;
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
    }
}