using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Services;
using FLAVSMAGS_TAILORING.Models;
using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class InventoryControl : UserControl
    {
        private readonly InventoryService _inventoryService;
        private string? _selectedItemId = null;
        private bool isSlideOpen = false;
        private System.Windows.Forms.Timer slideTimer = null!;

        public InventoryControl()
        {
            InitializeComponent();
            this.Text = "Inventory";
            _inventoryService = new InventoryService();
            _inventoryService.DataChanged += OnDataChanged;
            SetupSlideTimer();
            LoadInventoryData();
        }

        private void SetupSlideTimer()
        {
            slideTimer = new System.Windows.Forms.Timer { Interval = 15 };
            slideTimer.Tick += SlideTimer_Tick;
        }

        private void SlideTimer_Tick(object? sender, EventArgs e)
        {
            int step = 40;
            if (isSlideOpen)
            {
                if (panelSlide.Width < 350)
                    panelSlide.Width += step;
                else { panelSlide.Width = 350; slideTimer.Stop(); }
            }
            else
            {
                if (panelSlide.Width > 0)
                    panelSlide.Width -= step;
                else { panelSlide.Width = 0; slideTimer.Stop(); }
            }
        }

        private void OpenSlide()
        {
            isSlideOpen = true;
            slideTimer.Start();
            panelSlide.BringToFront();
            btnAdd.Text = "Return";
        }
        private void CloseSlide()
        {
            isSlideOpen = false;
            slideTimer.Start();
            ClearForm();
            btnAdd.Text = "Add Item";
        }

        // Auto calculate total value in slide panel
        private void CalculateSlideTotal(object? sender, EventArgs e)
        {
            if (decimal.TryParse(txtUnitPrice.Text, out decimal price) && numQuantity.Value > 0)
                lblTotalValueNum.Text = $"₱{(price * numQuantity.Value):N2}";
            else
                lblTotalValueNum.Text = "₱0.00";
        }

        // Set status label based on quantity and reorder level
        private void UpdateStatusDisplay()
        {
            decimal qty = numQuantity.Value;
            decimal reorder = numReorderLevel.Value;
            if (qty == 0)
                lblStatusValue.Text = "Out of Stock";
            else if (qty <= reorder)
                lblStatusValue.Text = "Low Stock";
            else
                lblStatusValue.Text = "In Stock";
        }

        private void OnDataChanged(object? sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)LoadInventoryData);
            else LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            LoadStatCards();
            LoadMaterialCards();
        }

        private void LoadStatCards()
        {
            var stats = _inventoryService.GetStatistics();
            lblTotalValue.Text = $"₱{stats.TotalValue:N2}";
            lblActiveMaterials.Text = stats.ActiveMaterials.ToString();
            lblLowStock.Text = (stats.LowStockCount + stats.OutOfStockCount).ToString();
        }

        private void LoadMaterialCards(string filter = "")
        {
            flowMaterials.Controls.Clear();
            // Use IEnumerable so we can reassign with .Where() later
            IEnumerable<InventoryItem> items = _inventoryService.GetAllInventory()
                .OrderBy(i => i.MaterialName);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                items = items.Where(i =>
                    i.MaterialName.ToLower().Contains(filter.ToLower()) ||
                    i.ItemId.ToLower().Contains(filter.ToLower())
                );  // no .ToList() needed
            }

            foreach (var item in items)
                flowMaterials.Controls.Add(CreateMaterialCard(item));
        }

        private Guna2Panel CreateMaterialCard(InventoryItem item)
        {
            Guna2Panel card = new Guna2Panel
            {
                Width = flowMaterials.Width - 25,
                Height = 90,
                BackColor = Color.Transparent,
                FillColor = Color.FromArgb(45, 44, 90),
                BorderRadius = 10,
                Margin = new Padding(5),
                ShadowDecoration = { Enabled = true, Depth = 5 },
                Tag = item
            };

            Label lblId = new Label { Text = item.ItemId, Font = new Font("Segoe UI", 9), ForeColor = Color.LightGray, Location = new Point(10, 8), AutoSize = true };
            Label lblName = new Label { Text = item.MaterialName, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 28), AutoSize = true };
            Label lblQty = new Label { Text = $"{item.Quantity} {item.Unit}", Font = new Font("Segoe UI", 9), ForeColor = Color.White, Location = new Point(200, 8), AutoSize = true };
            Label lblPrice = new Label { Text = $"₱{item.UnitPrice:F2} / {item.Unit}", Font = new Font("Segoe UI", 9), ForeColor = Color.White, Location = new Point(200, 30), AutoSize = true };
            Label lblTotal = new Label { Text = $"Total: ₱{item.TotalValue:N2}", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(400, 8), AutoSize = true };
            Label lblStatus = new Label { Text = GetStatusText(item.Status), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = GetStatusColor(item.Status), Location = new Point(400, 32), AutoSize = true };

            card.Click += (s, e) =>
            {
                _selectedItemId = item.ItemId;
                FillFormFromItem(item);
                lblSlideTitle.Text = "Edit Material";
                btnDelete.Visible = true;
                OpenSlide();
            };

            card.Controls.AddRange(new Control[] { lblId, lblName, lblQty, lblPrice, lblTotal, lblStatus });
            return card;
        }

        private string GetStatusText(StockStatus status)
        {
            return status switch
            {
                StockStatus.InStock => "In Stock",
                StockStatus.LowStock => "Low Stock",
                StockStatus.OutOfStock => "Out of Stock",
                _ => "Unknown"
            };
        }

        private Color GetStatusColor(StockStatus status)
        {
            return status switch
            {
                StockStatus.InStock => Color.LightGreen,
                StockStatus.LowStock => Color.Orange,
                StockStatus.OutOfStock => Color.Red,
                _ => Color.Gray
            };
        }

        private void FillFormFromItem(InventoryItem item)
        {
            txtMaterialName.Text = item.MaterialName;
            numQuantity.Value = item.Quantity;
            cmbUnit.SelectedItem = item.Unit;
            txtUnitPrice.Text = item.UnitPrice.ToString("F2");
            numReorderLevel.Value = item.ReorderLevel;
            CalculateSlideTotal(null, EventArgs.Empty);
            UpdateStatusDisplay();
        }

        // ========== Add / Edit / Delete ==========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isSlideOpen) { CloseSlide(); return; }
            _selectedItemId = null;
            ClearForm();
            lblSlideTitle.Text = "Add Material";
            btnDelete.Visible = false;
            OpenSlide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaterialName.Text) || cmbUnit.SelectedItem == null ||
                !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Please fill all required fields correctly.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal qty = numQuantity.Value;
            string unit = cmbUnit.SelectedItem!.ToString()!;
            decimal reorder = numReorderLevel.Value;

            try
            {
                if (_selectedItemId != null)
                {
                    var result = _inventoryService.UpdateInventoryItem(_selectedItemId, txtMaterialName.Text.Trim(),
                        qty, unit, unitPrice, reorder);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Material updated!", "Success");
                        CloseSlide();
                    }
                    else MessageBox.Show(result.Message, "Error");
                }
                else
                {
                    var result = _inventoryService.AddInventoryItem(txtMaterialName.Text.Trim(), qty, unit, unitPrice, reorder);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Material added!", "Success");
                        CloseSlide();
                    }
                    else MessageBox.Show(result.Message, "Error");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedItemId == null) return;
            if (MessageBox.Show("Delete this material?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // We don't have a DeleteInventory method in service; we can set quantity to 0 as a workaround or implement delete.
                // For now, just hide and reset – but it's better to add a real delete later.
                MessageBox.Show("Deletion logic can be added by setting quantity to 0.", "Info");
            }
        }

        private void btnCancelSlide_Click(object sender, EventArgs e) => CloseSlide();

        private void btnSearch_Click(object sender, EventArgs e) => LoadMaterialCards(txtSearch.Text.Trim());

        private void ClearForm()
        {
            txtMaterialName.Clear();
            numQuantity.Value = 0;
            cmbUnit.SelectedIndex = -1;
            txtUnitPrice.Text = "0.00";
            numReorderLevel.Value = 10;
            lblTotalValueNum.Text = "₱0.00";
            lblStatusValue.Text = "In Stock";
            _selectedItemId = null;
        }
    }
}

//using System;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using FLAVSMAGS_TAILORING.Services;
//using FLAVSMAGS_TAILORING.Models;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class InventoryControl : UserControl
//    {
//        private readonly InventoryService _inventoryService;

//        public InventoryControl()
//        {
//            InitializeComponent();
//            this.Text = "Inventory";

//            // Initialize service
//            _inventoryService = new InventoryService();

//            // Subscribe to data changes
//            _inventoryService.DataChanged += OnDataChanged;

//            // Load initial data
//            LoadInventoryData();
//        }

//        /// <summary>
//        /// Event handler for data changes - refresh all inventory data
//        /// </summary>
//        private void OnDataChanged(object? sender, EventArgs e)
//        {
//            LoadInventoryData();
//        }

//        /// <summary>
//        /// Load all inventory data from service
//        /// </summary>
//        private void LoadInventoryData()
//        {
//            try
//            {
//                LoadStatCards();
//                LoadMaterialsGrid();
//                CheckLowStockAlerts();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading inventory data: {ex.Message}",
//                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load real-time statistics into stat cards
//        /// </summary>
//        private void LoadStatCards()
//        {
//            var stats = _inventoryService.GetStatistics();

//            lblTotalValue.Text = $"${stats.TotalValue:N2}";
//            lblActiveMaterials.Text = stats.ActiveMaterials.ToString();
//            lblMonthlyConsumption.Text = $"${stats.MonthlyConsumption:N2}";
//        }

//        /// <summary>
//        /// Load materials into DataGridView with real data
//        /// </summary>
//        private void LoadMaterialsGrid()
//        {
//            var allInventory = _inventoryService.GetAllInventory();

//            DataTable dt = new DataTable();
//            dt.Columns.Add("ID", typeof(string));
//            dt.Columns.Add("Material Name", typeof(string));
//            dt.Columns.Add("Quantity", typeof(string));
//            dt.Columns.Add("Unit Price", typeof(string));
//            dt.Columns.Add("Total", typeof(string));
//            dt.Columns.Add("Status", typeof(string));

//            foreach (var item in allInventory.OrderBy(i => i.MaterialName))
//            {
//                string status = GetStatusDisplay(item.Status);

//                dt.Rows.Add(
//                    item.ItemId,
//                    item.MaterialName,
//                    $"{item.Quantity} {item.Unit}",
//                    $"${item.UnitPrice:F2}",
//                    $"${item.TotalValue:F2}",
//                    status
//                );
//            }

//            dgvMaterials.DataSource = dt;
//            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//            // Style the grid
//            dgvMaterials.BackgroundColor = Color.FromArgb(37, 36, 81);
//            dgvMaterials.ForeColor = Color.White;
//            dgvMaterials.GridColor = Color.FromArgb(90, 88, 140);
//            dgvMaterials.EnableHeadersVisualStyles = false;
//            dgvMaterials.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
//            dgvMaterials.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

//            // Apply cell formatting for status colors
//            dgvMaterials.CellFormatting -= DgvMaterials_CellFormatting; // Remove old handler
//            dgvMaterials.CellFormatting += DgvMaterials_CellFormatting;

//            // Add context menu for actions
//            AddContextMenu();
//        }

//        /// <summary>
//        /// Get display text for stock status
//        /// </summary>
//        private string GetStatusDisplay(StockStatus status)
//        {
//            return status switch
//            {
//                StockStatus.InStock => "● In Stock",
//                StockStatus.LowStock => "⚠ Low Stock",
//                StockStatus.OutOfStock => "🔴 Out of Stock",
//                _ => "Unknown"
//            };
//        }

//        /// <summary>
//        /// Format cells based on status (color coding)
//        /// </summary>
//        private void DgvMaterials_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
//        {
//            if (dgvMaterials.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
//            {
//                string status = e.Value.ToString()!;

//                if (status.Contains("Low Stock"))
//                    e.CellStyle.ForeColor = Color.Orange;
//                else if (status.Contains("Out of Stock"))
//                    e.CellStyle.ForeColor = Color.Red;
//                else
//                    e.CellStyle.ForeColor = Color.LightGreen;
//            }
//        }

//        /// <summary>
//        /// Add right-click context menu for inventory actions
//        /// </summary>
//        private void AddContextMenu()
//        {
//            ContextMenuStrip contextMenu = new ContextMenuStrip();

//            ToolStripMenuItem adjustStockItem = new ToolStripMenuItem("Adjust Stock");
//            adjustStockItem.Click += AdjustStock_Click;

//            ToolStripMenuItem viewDetailsItem = new ToolStripMenuItem("View Details");
//            viewDetailsItem.Click += ViewDetails_Click;

//            contextMenu.Items.Add(adjustStockItem);
//            contextMenu.Items.Add(viewDetailsItem);

//            dgvMaterials.ContextMenuStrip = contextMenu;
//        }

//        /// <summary>
//        /// Adjust stock quantity (add or subtract)
//        /// </summary>
//        private void AdjustStock_Click(object? sender, EventArgs e)
//        {
//            if (dgvMaterials.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Please select an item first.", "Selection Required",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            try
//            {
//                var selectedRow = dgvMaterials.SelectedRows[0];
//                string itemId = selectedRow.Cells["ID"].Value.ToString()!;
//                string materialName = selectedRow.Cells["Material Name"].Value.ToString()!;

//                // Show input dialog for quantity adjustment
//                using (Form inputForm = new Form())
//                {
//                    inputForm.Text = $"Adjust Stock - {materialName}";
//                    inputForm.Size = new Size(350, 180);
//                    inputForm.StartPosition = FormStartPosition.CenterParent;
//                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
//                    inputForm.MaximizeBox = false;
//                    inputForm.MinimizeBox = false;

//                    Label lblInstruction = new Label()
//                    {
//                        Text = "Enter quantity change (positive to add, negative to subtract):",
//                        Location = new Point(10, 20),
//                        Size = new Size(320, 40)
//                    };

//                    TextBox txtQuantity = new TextBox()
//                    {
//                        Location = new Point(10, 65),
//                        Size = new Size(320, 25)
//                    };

//                    Button btnOk = new Button()
//                    {
//                        Text = "Adjust",
//                        DialogResult = DialogResult.OK,
//                        Location = new Point(170, 100),
//                        Size = new Size(75, 30)
//                    };

//                    Button btnCancel = new Button()
//                    {
//                        Text = "Cancel",
//                        DialogResult = DialogResult.Cancel,
//                        Location = new Point(255, 100),
//                        Size = new Size(75, 30)
//                    };

//                    inputForm.Controls.AddRange(new Control[] { lblInstruction, txtQuantity, btnOk, btnCancel });
//                    inputForm.AcceptButton = btnOk;
//                    inputForm.CancelButton = btnCancel;

//                    if (inputForm.ShowDialog() == DialogResult.OK)
//                    {
//                        if (decimal.TryParse(txtQuantity.Text, out decimal quantityChange))
//                        {
//                            var result = _inventoryService.AdjustStock(itemId, quantityChange);

//                            if (result.IsSuccess)
//                            {
//                                MessageBox.Show(result.Message, "Success",
//                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            }
//                            else
//                            {
//                                MessageBox.Show(result.Message, "Adjustment Failed",
//                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            }
//                        }
//                        else
//                        {
//                            MessageBox.Show("Please enter a valid number.", "Invalid Input",
//                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error adjusting stock: {ex.Message}",
//                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// View detailed information about selected item
//        /// </summary>
//        private void ViewDetails_Click(object? sender, EventArgs e)
//        {
//            if (dgvMaterials.SelectedRows.Count == 0)
//                return;

//            try
//            {
//                var selectedRow = dgvMaterials.SelectedRows[0];
//                string itemId = selectedRow.Cells["ID"].Value.ToString()!;

//                var item = _inventoryService.GetAllInventory()
//                    .FirstOrDefault(i => i.ItemId == itemId);

//                if (item != null)
//                {
//                    string details = $"Item ID: {item.ItemId}\n" +
//                                   $"Material: {item.MaterialName}\n" +
//                                   $"Quantity: {item.Quantity} {item.Unit}\n" +
//                                   $"Unit Price: ${item.UnitPrice:F2}\n" +
//                                   $"Total Value: ${item.TotalValue:F2}\n" +
//                                   $"Status: {item.Status}\n" +
//                                   $"Reorder Level: {item.ReorderLevel}\n" +
//                                   $"Last Updated: {item.LastUpdated:yyyy-MM-dd HH:mm}";

//                    MessageBox.Show(details, "Inventory Details",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error viewing details: {ex.Message}",
//                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Check for low stock items and show alerts
//        /// </summary>
//        private void CheckLowStockAlerts()
//        {
//            var lowStockItems = _inventoryService.GetLowStockItems();

//            if (lowStockItems.Count > 0)
//            {
//                // Could show a notification or update a label
//                // For now, we'll just track it silently
//                // You could add a label to show "X items need reordering"
//            }
//        }

//        // Keep existing event handlers for Designer compatibility
//        private void lblTotalValue_Click(object sender, EventArgs e) { }
//    }
//}