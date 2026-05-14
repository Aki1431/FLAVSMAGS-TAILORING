using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    /// <summary>
    /// Represents a business expense
    /// </summary>
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ExpenseCategory Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Notes { get; set; } = string.Empty;

        public Expense()
        {
            ExpenseDate = DateTime.Now;
        }
    }

    /// <summary>
    /// Expense category enumeration
    /// </summary>
    public enum ExpenseCategory
    {
        Supplies,
        Equipment,
        Utilities,
        Rent,
        Salaries,
        Marketing,
        Other
    }
}
