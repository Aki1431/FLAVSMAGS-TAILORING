using System;
using System.Collections.Generic;
using System.Linq;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Data;

namespace FLAVSMAGS_TAILORING.Services
{
    /// <summary>
    /// Service layer for Inventory/Stock management
    /// Handles material tracking, stock alerts, and consumption
    /// </summary>
    public class InventoryService : IBaseService
    {
        private readonly DatabaseManager _dataManager;

        public event EventHandler? DataChanged;

        public InventoryService()
        {
            _dataManager = DatabaseManager.Instance;
        }

        #region Inventory CRUD Operations

        /// <summary>
        /// Add new inventory item with validation
        /// </summary>
        public Result<InventoryItem> AddInventoryItem(string materialName, decimal quantity,
            string unit, decimal unitPrice, decimal reorderLevel = 10)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(materialName))
                return Result<InventoryItem>.Failure("Material name is required");

            if (quantity < 0)
                return Result<InventoryItem>.Failure("Quantity cannot be negative");

            if (unitPrice < 0)
                return Result<InventoryItem>.Failure("Unit price cannot be negative");

            if (string.IsNullOrWhiteSpace(unit))
                return Result<InventoryItem>.Failure("Unit is required");

            try
            {
                var item = new InventoryItem
                {
                    MaterialName = materialName.Trim(),
                    Quantity = quantity,
                    Unit = unit.Trim(),
                    UnitPrice = unitPrice,
                    ReorderLevel = reorderLevel
                };

                var createdItem = _dataManager.AddInventoryItem(item);
                OnDataChanged();

                return Result<InventoryItem>.Success(createdItem);
            }
            catch (Exception ex)
            {
                return Result<InventoryItem>.Failure($"Failed to add inventory: {ex.Message}");
            }
        }

        /// <summary>
        /// Update inventory item details
        /// </summary>
        public Result<InventoryItem> UpdateInventoryItem(string itemId, string materialName,
            decimal quantity, string unit, decimal unitPrice, decimal reorderLevel)
        {
            var existing = _dataManager.GetInventoryItem(itemId);
            if (existing == null)
                return Result<InventoryItem>.Failure($"Item {itemId} not found");

            // Validation
            if (string.IsNullOrWhiteSpace(materialName))
                return Result<InventoryItem>.Failure("Material name is required");

            if (quantity < 0)
                return Result<InventoryItem>.Failure("Quantity cannot be negative");

            if (unitPrice < 0)
                return Result<InventoryItem>.Failure("Unit price cannot be negative");

            try
            {
                existing.MaterialName = materialName.Trim();
                existing.Quantity = quantity;
                existing.Unit = unit.Trim();
                existing.UnitPrice = unitPrice;
                existing.ReorderLevel = reorderLevel;

                _dataManager.UpdateInventoryItem(existing);
                OnDataChanged();

                return Result<InventoryItem>.Success(existing);
            }
            catch (Exception ex)
            {
                return Result<InventoryItem>.Failure($"Failed to update inventory: {ex.Message}");
            }
        }

        /// <summary>
        /// Adjust stock quantity (add or subtract)
        /// </summary>
        public Result<InventoryItem> AdjustStock(string itemId, decimal quantityChange, string reason = "")
        {
            var item = _dataManager.GetInventoryItem(itemId);
            if (item == null)
                return Result<InventoryItem>.Failure($"Item {itemId} not found");

            // Business rule: Cannot reduce stock below zero
            if (item.Quantity + quantityChange < 0)
            {
                return Result<InventoryItem>.Failure(
                    $"Insufficient stock. Available: {item.Quantity}, Requested: {Math.Abs(quantityChange)}");
            }

            try
            {
                var success = _dataManager.AdjustInventory(itemId, quantityChange);
                if (!success)
                    return Result<InventoryItem>.Failure("Failed to adjust stock");

                OnDataChanged();

                // Check if stock alert needed
                var updatedItem = _dataManager.GetInventoryItem(itemId);
                if (updatedItem != null && updatedItem.Status == StockStatus.LowStock)
                {
                    return Result<InventoryItem>.Success(updatedItem,
                        $"Stock adjusted. WARNING: {updatedItem.MaterialName} is now LOW STOCK!");
                }

                return Result<InventoryItem>.Success(updatedItem!);
            }
            catch (Exception ex)
            {
                return Result<InventoryItem>.Failure($"Failed to adjust stock: {ex.Message}");
            }
        }

        /// <summary>
        /// Consume materials for an order
        /// </summary>
        public Result<bool> ConsumeForOrder(Dictionary<string, decimal> materials, int orderId)
        {
            // Validate all materials are available first
            foreach (var kvp in materials)
            {
                var item = _dataManager.GetInventoryItem(kvp.Key);
                if (item == null)
                    return Result<bool>.Failure($"Material {kvp.Key} not found");

                if (item.Quantity < kvp.Value)
                    return Result<bool>.Failure(
                        $"Insufficient {item.MaterialName}. Available: {item.Quantity}, Required: {kvp.Value}");
            }

            try
            {
                // Consume all materials
                foreach (var kvp in materials)
                {
                    _dataManager.AdjustInventory(kvp.Key, -kvp.Value);
                }

                OnDataChanged();
                return Result<bool>.Success(true, "Materials consumed successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to consume materials: {ex.Message}");
            }
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Get all inventory items
        /// </summary>
        public List<InventoryItem> GetAllInventory()
        {
            return _dataManager.GetAllInventory();
        }

        /// <summary>
        /// Get low stock items that need reordering
        /// </summary>
        public List<InventoryItem> GetLowStockItems()
        {
            return _dataManager.GetLowStockItems();
        }

        /// <summary>
        /// Get inventory statistics
        /// </summary>
        public InventoryStatistics GetStatistics()
        {
            var allItems = _dataManager.GetAllInventory();

            return new InventoryStatistics
            {
                TotalValue = allItems.Sum(i => i.TotalValue),
                ActiveMaterials = allItems.Count,
                LowStockCount = allItems.Count(i => i.Status == StockStatus.LowStock),
                OutOfStockCount = allItems.Count(i => i.Status == StockStatus.OutOfStock),
                MonthlyConsumption = CalculateMonthlyConsumption()
            };
        }

        /// <summary>
        /// Calculate estimated monthly consumption
        /// </summary>
        private decimal CalculateMonthlyConsumption()
        {
            // This is a placeholder - in real app, track actual consumption
            // For now, return an estimated value
            return 12350.00m;
        }

        #endregion

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Inventory statistics DTO
    /// </summary>
    public class InventoryStatistics
    {
        public decimal TotalValue { get; set; }
        public int ActiveMaterials { get; set; }
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
        public decimal MonthlyConsumption { get; set; }
    }
}