using System;
using System.Collections.Generic;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Data;

namespace FLAVSMAGS_TAILORING.Services
{
    /// <summary>
    /// Service layer for Expense management
    /// Handles expense tracking, categorization, and analysis
    /// </summary>
    public class ExpenseService : IBaseService
    {
        private readonly DataManager _dataManager;

        public event EventHandler? DataChanged;

        public ExpenseService()
        {
            _dataManager = DataManager.Instance;
        }

        #region Expense CRUD Operations

        /// <summary>
        /// Create new expense with validation
        /// </summary>
        public Result<Expense> CreateExpense(string description, ExpenseCategory category,
            decimal amount, DateTime expenseDate, string notes = "")
        {
            // Validation
            if (string.IsNullOrWhiteSpace(description))
                return Result<Expense>.Failure("Description is required");

            if (amount <= 0)
                return Result<Expense>.Failure("Amount must be greater than zero");

            // Business rule: Cannot create expenses for future dates
            if (expenseDate > DateTime.Now)
                return Result<Expense>.Failure("Cannot create expenses for future dates");

            try
            {
                var expense = new Expense
                {
                    Description = description.Trim(),
                    Category = category,
                    Amount = amount,
                    ExpenseDate = expenseDate,
                    Notes = notes.Trim()
                };

                var createdExpense = _dataManager.AddExpense(expense);
                OnDataChanged();

                return Result<Expense>.Success(createdExpense);
            }
            catch (Exception ex)
            {
                return Result<Expense>.Failure($"Failed to create expense: {ex.Message}");
            }
        }

        /// <summary>
        /// Update existing expense
        /// </summary>
        public Result<Expense> UpdateExpense(int expenseId, string description,
            ExpenseCategory category, decimal amount, DateTime expenseDate, string notes = "")
        {
            var existing = _dataManager.GetExpenseById(expenseId);
            if (existing == null)
                return Result<Expense>.Failure($"Expense {expenseId} not found");

            // Validation
            if (string.IsNullOrWhiteSpace(description))
                return Result<Expense>.Failure("Description is required");

            if (amount <= 0)
                return Result<Expense>.Failure("Amount must be greater than zero");

            try
            {
                existing.Description = description.Trim();
                existing.Category = category;
                existing.Amount = amount;
                existing.ExpenseDate = expenseDate;
                existing.Notes = notes.Trim();

                _dataManager.UpdateExpense(existing);
                OnDataChanged();

                return Result<Expense>.Success(existing);
            }
            catch (Exception ex)
            {
                return Result<Expense>.Failure($"Failed to update expense: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete expense
        /// </summary>
        public Result<bool> DeleteExpense(int expenseId)
        {
            var expense = _dataManager.GetExpenseById(expenseId);
            if (expense == null)
                return Result<bool>.Failure($"Expense {expenseId} not found");

            try
            {
                var success = _dataManager.DeleteExpense(expenseId);
                if (success)
                {
                    OnDataChanged();
                    return Result<bool>.Success(true, "Expense deleted successfully");
                }

                return Result<bool>.Failure("Failed to delete expense");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting expense: {ex.Message}");
            }
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Get all expenses
        /// </summary>
        public List<Expense> GetAllExpenses()
        {
            return _dataManager.GetAllExpenses();
        }

        /// <summary>
        /// Get expenses by date range
        /// </summary>
        public List<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dataManager.GetExpensesByDateRange(startDate, endDate);
        }

        /// <summary>
        /// Get expenses for current month
        /// </summary>
        public List<Expense> GetCurrentMonthExpenses()
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            return GetExpensesByDateRange(startOfMonth, endOfMonth);
        }

        /// <summary>
        /// Get expense statistics
        /// </summary>
        public ExpenseStatistics GetStatistics()
        {
            var currentMonth = GetCurrentMonthExpenses();
            var lastMonth = GetLastMonthExpenses();

            var currentTotal = currentMonth.Sum(e => e.Amount);
            var lastTotal = lastMonth.Sum(e => e.Amount);

            decimal percentChange = 0;
            if (lastTotal > 0)
            {
                percentChange = ((currentTotal - lastTotal) / lastTotal) * 100;
            }

            return new ExpenseStatistics
            {
                TotalExpenses = currentTotal,
                ExpenseCount = currentMonth.Count,
                PercentChange = percentChange,
                ExpensesByCategory = GetExpensesByCategory(currentMonth)
            };
        }

        /// <summary>
        /// Get last month's expenses
        /// </summary>
        private List<Expense> GetLastMonthExpenses()
        {
            var now = DateTime.Now;
            var startOfLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            var endOfLastMonth = startOfLastMonth.AddMonths(1).AddDays(-1);

            return GetExpensesByDateRange(startOfLastMonth, endOfLastMonth);
        }

        /// <summary>
        /// Group expenses by category
        /// </summary>
        private Dictionary<ExpenseCategory, decimal> GetExpensesByCategory(List<Expense> expenses)
        {
            return expenses.GroupBy(e => e.Category)
                          .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        #endregion

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Expense statistics DTO
    /// </summary>
    public class ExpenseStatistics
    {
        public decimal TotalExpenses { get; set; }
        public int ExpenseCount { get; set; }
        public decimal PercentChange { get; set; }
        public Dictionary<ExpenseCategory, decimal> ExpensesByCategory { get; set; }
            = new Dictionary<ExpenseCategory, decimal>();
    }
}