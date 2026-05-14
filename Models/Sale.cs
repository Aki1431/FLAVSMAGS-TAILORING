using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    /// <summary>
    /// Represents a sales transaction
    /// </summary>
    public class Sale
    {
        public int SaleId { get; set; }
        public int OrderId { get; set; }
        public string ProductCategory { get; set; } = string.Empty;
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit => Revenue - Cost;
        public DateTime SaleDate { get; set; }

        public Sale()
        {
            SaleDate = DateTime.Now;
        }
    }
}
