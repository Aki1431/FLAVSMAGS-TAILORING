using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    public class Order
    {
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Items { get; set; } = string.Empty; // e.g., "2 pcs"
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime? DeletedDate { get; set; }   // null = not deleted, otherwise deletion time

        public Order()
        {
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Ready,
        Completed,
        Cancelled
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FLAVSMAGS_TAILORING.Models
//{
//    /// <summary>
//    /// Represents an order with status tracking
//    /// </summary>
//    public class Order
//    {
//        public string ContactNumber { get; set; } = string.Empty;
//        public string Email { get; set; } = string.Empty;
//        public string ServiceType { get; set; } = string.Empty;
//        public string ItemType { get; set; } = string.Empty;
//        public int Quantity { get; set; } = 1;
//        public int OrderId { get; set; }
//        public int CustomerId { get; set; }
//        public string CustomerName { get; set; } = string.Empty;
//        public string Items { get; set; } = string.Empty; // e.g., "2 pcs"
//        public decimal TotalAmount { get; set; }
//        public OrderStatus Status { get; set; }
//        public DateTime OrderDate { get; set; }
//        public DateTime? CompletionDate { get; set; }
//        public string Notes { get; set; } = string.Empty;

//        public Order()
//        {
//            OrderDate = DateTime.Now;
//            Status = OrderStatus.Pending;
//        }
//    }

//    /// <summary>
//    /// Order status enumeration
//    /// </summary>
//    public enum OrderStatus
//    {
//        Pending,
//        Processing,
//        Ready,
//        Completed,
//        Cancelled
//    }
//}
