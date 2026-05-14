using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    /// <summary>
    /// Represents inventory/material item with stock tracking
    /// </summary>
    public class InventoryItem
    {
        public string ItemId { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty; // meters, pcs, etc.
        public decimal UnitPrice { get; set; }
        public decimal TotalValue => Quantity * UnitPrice;
        public StockStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal ReorderLevel { get; set; } = 10; // Low stock threshold

        public InventoryItem()
        {
            LastUpdated = DateTime.Now;
            UpdateStatus();
        }

        /// <summary>
        /// Auto-update status based on quantity
        /// </summary>
        public void UpdateStatus()
        {
            if (Quantity == 0)
                Status = StockStatus.OutOfStock;
            else if (Quantity <= ReorderLevel)
                Status = StockStatus.LowStock;
            else
                Status = StockStatus.InStock;
        }
    }

    /// <summary>
    /// Stock status enumeration
    /// </summary>
    public enum StockStatus
    {
        InStock,
        LowStock,
        OutOfStock
    }
}
