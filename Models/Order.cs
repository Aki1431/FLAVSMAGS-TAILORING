using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    /// <summary>
    /// Represents an order with status tracking
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Items { get; set; } = string.Empty; // e.g., "2 pcs"
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; } = string.Empty;

        public Order()
        {
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }
    }

    /// <summary>
    /// Order status enumeration
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        Processing,
        Ready,
        Completed,
        Cancelled
    }
}
