using System;
using System.Collections.Generic;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Data;

namespace FLAVSMAGS_TAILORING.Services
{
    public class OrderService : IBaseService
    {
        private readonly DatabaseManager _dataManager;
        public event EventHandler? DataChanged;

        public OrderService()
        {
            _dataManager = DatabaseManager.Instance;
        }

        #region Original CRUD Operations (unchanged)

        public Result<Order> CreateOrder(string customerName, string items, decimal totalAmount, string notes = "")
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return Result<Order>.Failure("Customer name is required");
            if (string.IsNullOrWhiteSpace(items))
                return Result<Order>.Failure("Order items are required");
            if (totalAmount <= 0)
                return Result<Order>.Failure("Total amount must be greater than zero");

            try
            {
                var order = new Order
                {
                    CustomerName = customerName.Trim(),
                    Items = items.Trim(),
                    TotalAmount = totalAmount,
                    Notes = notes.Trim(),
                    Status = OrderStatus.Pending,
                    OrderDate = DateTime.Now
                };
                var createdOrder = _dataManager.AddOrder(order);
                OnDataChanged();
                return Result<Order>.Success(createdOrder);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to create order: {ex.Message}");
            }
        }

        public Result<Order> UpdateOrder(int orderId, string customerName, string items,
            decimal totalAmount, OrderStatus status, string notes = "")
        {
            var existing = _dataManager.GetOrderById(orderId);
            if (existing == null)
                return Result<Order>.Failure($"Order {orderId} not found");
            if (string.IsNullOrWhiteSpace(customerName))
                return Result<Order>.Failure("Customer name is required");
            if (string.IsNullOrWhiteSpace(items))
                return Result<Order>.Failure("Order items are required");
            if (totalAmount <= 0)
                return Result<Order>.Failure("Total amount must be greater than zero");

            try
            {
                existing.CustomerName = customerName.Trim();
                existing.Items = items.Trim();
                existing.TotalAmount = totalAmount;
                existing.Status = status;
                existing.Notes = notes.Trim();

                if (status == OrderStatus.Completed && !existing.CompletionDate.HasValue)
                    existing.CompletionDate = DateTime.Now;

                var success = _dataManager.UpdateOrder(existing);
                if (!success)
                    return Result<Order>.Failure("Failed to update order");

                OnDataChanged();
                return Result<Order>.Success(existing);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to update order: {ex.Message}");
            }
        }

        public Result<Order> ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = _dataManager.GetOrderById(orderId);
            if (order == null)
                return Result<Order>.Failure($"Order {orderId} not found");
            if (order.Status == OrderStatus.Cancelled && newStatus != OrderStatus.Cancelled)
                return Result<Order>.Failure("Cannot modify cancelled orders");

            try
            {
                order.Status = newStatus;
                if (newStatus == OrderStatus.Completed)
                    order.CompletionDate = DateTime.Now;
                _dataManager.UpdateOrder(order);
                OnDataChanged();
                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to change status: {ex.Message}");
            }
        }

        public Result<bool> DeleteOrder(int orderId)
        {
            var order = _dataManager.GetOrderById(orderId);
            if (order == null)
                return Result<bool>.Failure($"Order {orderId} not found");

            if (order.Status != OrderStatus.Pending)
            {
                order.Status = OrderStatus.Cancelled;
                _dataManager.UpdateOrder(order);
                OnDataChanged();
                return Result<bool>.Success(true, "Order cancelled successfully");
            }

            try
            {
                var success = _dataManager.DeleteOrder(orderId);
                if (success)
                {
                    OnDataChanged();
                    return Result<bool>.Success(true, "Order deleted successfully");
                }
                return Result<bool>.Failure("Failed to delete order");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting order: {ex.Message}");
            }
        }

        #endregion

        #region New Full-Feature Methods (used by the new Orders UI)

        public Result<Order> CreateOrderFull(string customerName, string contact, string email,
            string service, string itemType, int quantity, decimal totalAmount)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return Result<Order>.Failure("Customer name is required");
            if (string.IsNullOrWhiteSpace(service))
                return Result<Order>.Failure("Service type is required");
            if (quantity <= 0)
                return Result<Order>.Failure("Quantity must be at least 1");
            if (totalAmount <= 0)
                return Result<Order>.Failure("Total amount must be greater than zero");

            try
            {
                var order = new Order
                {
                    CustomerName = customerName.Trim(),
                    ContactNumber = contact.Trim(),
                    Email = email.Trim(),
                    ServiceType = service,
                    ItemType = itemType,
                    Quantity = quantity,
                    Items = $"{itemType} x {quantity}",   // backward‑compatible summary
                    TotalAmount = totalAmount,
                    Status = OrderStatus.Pending,
                    OrderDate = DateTime.Now
                };
                var created = _dataManager.AddOrder(order);
                OnDataChanged();
                return Result<Order>.Success(created);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to create order: {ex.Message}");
            }
        }

        public Result<Order> UpdateOrderFull(int orderId, string customerName, string contact,
            string email, string service, string itemType, int quantity, decimal totalAmount)
        {
            var existing = _dataManager.GetOrderById(orderId);
            if (existing == null)
                return Result<Order>.Failure($"Order {orderId} not found");

            if (string.IsNullOrWhiteSpace(customerName))
                return Result<Order>.Failure("Customer name is required");
            if (quantity <= 0)
                return Result<Order>.Failure("Quantity must be at least 1");
            if (totalAmount <= 0)
                return Result<Order>.Failure("Total amount must be greater than zero");

            try
            {
                existing.CustomerName = customerName.Trim();
                existing.ContactNumber = contact.Trim();
                existing.Email = email.Trim();
                existing.ServiceType = service;
                existing.ItemType = itemType;
                existing.Quantity = quantity;
                existing.Items = $"{itemType} x {quantity}";
                existing.TotalAmount = totalAmount;
                // Status remains unchanged unless you explicitly change it
                _dataManager.UpdateOrder(existing);
                OnDataChanged();
                return Result<Order>.Success(existing);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to update order: {ex.Message}");
            }
        }

        #endregion

        #region Query Operations (unchanged)

        public List<Order> GetAllOrders() => _dataManager.GetAllOrders();

        public List<Order> GetOrdersByStatus(OrderStatus status) => _dataManager.GetOrdersByStatus(status);

        public List<Order> SearchOrders(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllOrders();
            var term = searchTerm.Trim().ToLower();
            return _dataManager.GetAllOrders()
                .Where(o => o.CustomerName.ToLower().Contains(term) ||
                            o.OrderId.ToString().Contains(term)).ToList();
        }

        public OrderStatistics GetStatistics()
        {
            var allOrders = _dataManager.GetAllOrders();
            return new OrderStatistics
            {
                TotalOrders = allOrders.Count,
                PendingOrders = allOrders.Count(o => o.Status == OrderStatus.Pending),
                ProcessingOrders = allOrders.Count(o => o.Status == OrderStatus.Processing),
                CompletedOrders = allOrders.Count(o => o.Status == OrderStatus.Completed),
                TotalRevenue = allOrders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalAmount)
            };
        }

        #endregion

        protected virtual void OnDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
    }

    // ---------- Helper classes (unchanged) ----------
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string Message { get; private set; } = string.Empty;

        private Result(bool isSuccess, T? data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static Result<T> Success(T data, string message = "Operation successful")
            => new Result<T>(true, data, message);

        public static Result<T> Failure(string message)
            => new Result<T>(false, default, message);
    }

    public class OrderStatistics
    {
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using FLAVSMAGS_TAILORING.Models;
//using FLAVSMAGS_TAILORING.Data;

//namespace FLAVSMAGS_TAILORING.Services
//{
//    /// <summary>
//    /// Service layer for Order operations
//    /// Handles business logic, validation, and data orchestration
//    /// </summary>
//    public class OrderService : IBaseService
//    {
//        private readonly DatabaseManager _dataManager;

//        public event EventHandler? DataChanged;

//        public OrderService()
//        {
//            _dataManager = DatabaseManager.Instance;
//        }

//        #region Order CRUD Operations

//        /// <summary>
//        /// Create a new order with validation
//        /// </summary>
//        public Result<Order> CreateOrder(string customerName, string items, decimal totalAmount, string notes = "")
//        {
//            // Validation
//            if (string.IsNullOrWhiteSpace(customerName))
//                return Result<Order>.Failure("Customer name is required");

//            if (string.IsNullOrWhiteSpace(items))
//                return Result<Order>.Failure("Order items are required");

//            if (totalAmount <= 0)
//                return Result<Order>.Failure("Total amount must be greater than zero");

//            try
//            {
//                // Create order entity
//                var order = new Order
//                {
//                    CustomerName = customerName.Trim(),
//                    Items = items.Trim(),
//                    TotalAmount = totalAmount,
//                    Notes = notes.Trim(),
//                    Status = OrderStatus.Pending,
//                    OrderDate = DateTime.Now
//                };

//                // Persist to data layer
//                var createdOrder = _dataManager.AddOrder(order);

//                // Notify listeners (UI updates automatically)
//                OnDataChanged();

//                return Result<Order>.Success(createdOrder);
//            }
//            catch (Exception ex)
//            {
//                return Result<Order>.Failure($"Failed to create order: {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Update existing order
//        /// </summary>
//        public Result<Order> UpdateOrder(int orderId, string customerName, string items,
//            decimal totalAmount, OrderStatus status, string notes = "")
//        {
//            // Validation
//            var existing = _dataManager.GetOrderById(orderId);
//            if (existing == null)
//                return Result<Order>.Failure($"Order {orderId} not found");

//            if (string.IsNullOrWhiteSpace(customerName))
//                return Result<Order>.Failure("Customer name is required");

//            if (string.IsNullOrWhiteSpace(items))
//                return Result<Order>.Failure("Order items are required");

//            if (totalAmount <= 0)
//                return Result<Order>.Failure("Total amount must be greater than zero");

//            try
//            {
//                existing.CustomerName = customerName.Trim();
//                existing.Items = items.Trim();
//                existing.TotalAmount = totalAmount;
//                existing.Status = status;
//                existing.Notes = notes.Trim();

//                // Mark completion if status is completed
//                if (status == OrderStatus.Completed && !existing.CompletionDate.HasValue)
//                {
//                    existing.CompletionDate = DateTime.Now;
//                }

//                var success = _dataManager.UpdateOrder(existing);

//                if (!success)
//                    return Result<Order>.Failure("Failed to update order");

//                OnDataChanged();
//                return Result<Order>.Success(existing);
//            }
//            catch (Exception ex)
//            {
//                return Result<Order>.Failure($"Failed to update order: {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Change order status with validation
//        /// </summary>
//        public Result<Order> ChangeOrderStatus(int orderId, OrderStatus newStatus)
//        {
//            var order = _dataManager.GetOrderById(orderId);
//            if (order == null)
//                return Result<Order>.Failure($"Order {orderId} not found");

//            // Business rule: Cannot change status of cancelled orders
//            if (order.Status == OrderStatus.Cancelled && newStatus != OrderStatus.Cancelled)
//                return Result<Order>.Failure("Cannot modify cancelled orders");

//            try
//            {
//                order.Status = newStatus;

//                if (newStatus == OrderStatus.Completed)
//                    order.CompletionDate = DateTime.Now;

//                _dataManager.UpdateOrder(order);
//                OnDataChanged();

//                return Result<Order>.Success(order);
//            }
//            catch (Exception ex)
//            {
//                return Result<Order>.Failure($"Failed to change status: {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Delete/Cancel an order
//        /// </summary>
//        public Result<bool> DeleteOrder(int orderId)
//        {
//            var order = _dataManager.GetOrderById(orderId);
//            if (order == null)
//                return Result<bool>.Failure($"Order {orderId} not found");

//            // Business rule: Can only delete pending orders, others must be cancelled
//            if (order.Status != OrderStatus.Pending)
//            {
//                order.Status = OrderStatus.Cancelled;
//                _dataManager.UpdateOrder(order);
//                OnDataChanged();
//                return Result<bool>.Success(true, "Order cancelled successfully");
//            }

//            try
//            {
//                var success = _dataManager.DeleteOrder(orderId);
//                if (success)
//                {
//                    OnDataChanged();
//                    return Result<bool>.Success(true, "Order deleted successfully");
//                }

//                return Result<bool>.Failure("Failed to delete order");
//            }
//            catch (Exception ex)
//            {
//                return Result<bool>.Failure($"Error deleting order: {ex.Message}");
//            }
//        }

//        #endregion

//        #region Query Operations

//        /// <summary>
//        /// Get all orders
//        /// </summary>
//        public List<Order> GetAllOrders()
//        {
//            return _dataManager.GetAllOrders();
//        }

//        /// <summary>
//        /// Get orders by status
//        /// </summary>
//        public List<Order> GetOrdersByStatus(OrderStatus status)
//        {
//            return _dataManager.GetOrdersByStatus(status);
//        }

//        /// <summary>
//        /// Search orders by customer name or order ID
//        /// </summary>
//        public List<Order> SearchOrders(string searchTerm)
//        {
//            if (string.IsNullOrWhiteSpace(searchTerm))
//                return GetAllOrders();

//            var term = searchTerm.Trim().ToLower();
//            var allOrders = _dataManager.GetAllOrders();

//            return allOrders.Where(o =>
//                o.CustomerName.ToLower().Contains(term) ||
//                o.OrderId.ToString().Contains(term)
//            ).ToList();
//        }

//        /// <summary>
//        /// Get order statistics
//        /// </summary>
//        public OrderStatistics GetStatistics()
//        {
//            var allOrders = _dataManager.GetAllOrders();

//            return new OrderStatistics
//            {
//                TotalOrders = allOrders.Count,
//                PendingOrders = allOrders.Count(o => o.Status == OrderStatus.Pending),
//                ProcessingOrders = allOrders.Count(o => o.Status == OrderStatus.Processing),
//                CompletedOrders = allOrders.Count(o => o.Status == OrderStatus.Completed),
//                TotalRevenue = allOrders.Where(o => o.Status == OrderStatus.Completed)
//                                       .Sum(o => o.TotalAmount)
//            };
//        }

//        #endregion

//        /// <summary>
//        /// Trigger data changed event
//        /// </summary>
//        protected virtual void OnDataChanged()
//        {
//            DataChanged?.Invoke(this, EventArgs.Empty);
//        }
//    }

//    #region Helper Classes

//    /// <summary>
//    /// Generic result wrapper for service operations
//    /// Provides consistent error handling across all services
//    /// </summary>
//    public class Result<T>
//    {
//        public bool IsSuccess { get; private set; }
//        public T? Data { get; private set; }
//        public string Message { get; private set; } = string.Empty;

//        private Result(bool isSuccess, T? data, string message)
//        {
//            IsSuccess = isSuccess;
//            Data = data;
//            Message = message;
//        }

//        public static Result<T> Success(T data, string message = "Operation successful")
//        {
//            return new Result<T>(true, data, message);
//        }

//        public static Result<T> Failure(string message)
//        {
//            return new Result<T>(false, default, message);
//        }
//    }

//    /// <summary>
//    /// Order statistics data transfer object
//    /// </summary>
//    public class OrderStatistics
//    {
//        public int TotalOrders { get; set; }
//        public int PendingOrders { get; set; }
//        public int ProcessingOrders { get; set; }
//        public int CompletedOrders { get; set; }
//        public decimal TotalRevenue { get; set; }
//    }

//    #endregion
//}