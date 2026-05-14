using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Services;
using FLAVSMAGS_TAILORING.Models;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class InventoryControl : UserControl
    {
        private readonly InventoryService _inventoryService;

        public InventoryControl()
        {
            InitializeComponent();
            this.Text = "Inventory";

            // Initialize service
            _inventoryService = new InventoryService();

            // Subscribe to data changes
            _inventoryService.DataChanged += OnDataChanged;

            // Load initial data
            LoadInventoryData();
        }

        /// <summary>
        /// Event handler for data changes - refresh all inventory data
        /// </summary>
        private void OnDataChanged(object? sender, EventArgs e)
        {
            LoadInventoryData();
        }

        /// <summary>
        /// Load all inventory data from service
        /// </summary>
        private void LoadInventoryData()
        {
            try
            {
                LoadStatCards();
                LoadMaterialsGrid();
                CheckLowStockAlerts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load real-time statistics into stat cards
        /// </summary>
        private void LoadStatCards()
        {
            var stats = _inventoryService.GetStatistics();

            lblTotalValue.Text = $"${stats.TotalValue:N2}";
            lblActiveMaterials.Text = stats.ActiveMaterials.ToString();
            lblMonthlyConsumption.Text = $"${stats.MonthlyConsumption:N2}";
        }

        /// <summary>
        /// Load materials into DataGridView with real data
        /// </summary>
        private void LoadMaterialsGrid()
        {
            var allInventory = _inventoryService.GetAllInventory();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Material Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Unit Price", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            foreach (var item in allInventory.OrderBy(i => i.MaterialName))
            {
                string status = GetStatusDisplay(item.Status);

                dt.Rows.Add(
                    item.ItemId,
                    item.MaterialName,
                    $"{item.Quantity} {item.Unit}",
                    $"${item.UnitPrice:F2}",
                    $"${item.TotalValue:F2}",
                    status
                );
            }

            dgvMaterials.DataSource = dt;
            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Style the grid
            dgvMaterials.BackgroundColor = Color.FromArgb(37, 36, 81);
            dgvMaterials.ForeColor = Color.White;
            dgvMaterials.GridColor = Color.FromArgb(90, 88, 140);
            dgvMaterials.EnableHeadersVisualStyles = false;
            dgvMaterials.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
            dgvMaterials.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Apply cell formatting for status colors
            dgvMaterials.CellFormatting -= DgvMaterials_CellFormatting; // Remove old handler
            dgvMaterials.CellFormatting += DgvMaterials_CellFormatting;

            // Add context menu for actions
            AddContextMenu();
        }

        /// <summary>
        /// Get display text for stock status
        /// </summary>
        private string GetStatusDisplay(StockStatus status)
        {
            return status switch
            {
                StockStatus.InStock => "● In Stock",
                StockStatus.LowStock => "⚠ Low Stock",
                StockStatus.OutOfStock => "🔴 Out of Stock",
                _ => "Unknown"
            };
        }

        /// <summary>
        /// Format cells based on status (color coding)
        /// </summary>
        private void DgvMaterials_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMaterials.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString()!;

                if (status.Contains("Low Stock"))
                    e.CellStyle.ForeColor = Color.Orange;
                else if (status.Contains("Out of Stock"))
                    e.CellStyle.ForeColor = Color.Red;
                else
                    e.CellStyle.ForeColor = Color.LightGreen;
            }
        }

        /// <summary>
        /// Add right-click context menu for inventory actions
        /// </summary>
        private void AddContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem adjustStockItem = new ToolStripMenuItem("Adjust Stock");
            adjustStockItem.Click += AdjustStock_Click;

            ToolStripMenuItem viewDetailsItem = new ToolStripMenuItem("View Details");
            viewDetailsItem.Click += ViewDetails_Click;

            contextMenu.Items.Add(adjustStockItem);
            contextMenu.Items.Add(viewDetailsItem);

            dgvMaterials.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Adjust stock quantity (add or subtract)
        /// </summary>
        private void AdjustStock_Click(object? sender, EventArgs e)
        {
            if (dgvMaterials.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item first.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var selectedRow = dgvMaterials.SelectedRows[0];
                string itemId = selectedRow.Cells["ID"].Value.ToString()!;
                string materialName = selectedRow.Cells["Material Name"].Value.ToString()!;

                // Show input dialog for quantity adjustment
                using (Form inputForm = new Form())
                {
                    inputForm.Text = $"Adjust Stock - {materialName}";
                    inputForm.Size = new Size(350, 180);
                    inputForm.StartPosition = FormStartPosition.CenterParent;
                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputForm.MaximizeBox = false;
                    inputForm.MinimizeBox = false;

                    Label lblInstruction = new Label()
                    {
                        Text = "Enter quantity change (positive to add, negative to subtract):",
                        Location = new Point(10, 20),
                        Size = new Size(320, 40)
                    };

                    TextBox txtQuantity = new TextBox()
                    {
                        Location = new Point(10, 65),
                        Size = new Size(320, 25)
                    };

                    Button btnOk = new Button()
                    {
                        Text = "Adjust",
                        DialogResult = DialogResult.OK,
                        Location = new Point(170, 100),
                        Size = new Size(75, 30)
                    };

                    Button btnCancel = new Button()
                    {
                        Text = "Cancel",
                        DialogResult = DialogResult.Cancel,
                        Location = new Point(255, 100),
                        Size = new Size(75, 30)
                    };

                    inputForm.Controls.AddRange(new Control[] { lblInstruction, txtQuantity, btnOk, btnCancel });
                    inputForm.AcceptButton = btnOk;
                    inputForm.CancelButton = btnCancel;

                    if (inputForm.ShowDialog() == DialogResult.OK)
                    {
                        if (decimal.TryParse(txtQuantity.Text, out decimal quantityChange))
                        {
                            var result = _inventoryService.AdjustStock(itemId, quantityChange);

                            if (result.IsSuccess)
                            {
                                MessageBox.Show(result.Message, "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(result.Message, "Adjustment Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid number.", "Invalid Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adjusting stock: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// View detailed information about selected item
        /// </summary>
        private void ViewDetails_Click(object? sender, EventArgs e)
        {
            if (dgvMaterials.SelectedRows.Count == 0)
                return;

            try
            {
                var selectedRow = dgvMaterials.SelectedRows[0];
                string itemId = selectedRow.Cells["ID"].Value.ToString()!;

                var item = _inventoryService.GetAllInventory()
                    .FirstOrDefault(i => i.ItemId == itemId);

                if (item != null)
                {
                    string details = $"Item ID: {item.ItemId}\n" +
                                   $"Material: {item.MaterialName}\n" +
                                   $"Quantity: {item.Quantity} {item.Unit}\n" +
                                   $"Unit Price: ${item.UnitPrice:F2}\n" +
                                   $"Total Value: ${item.TotalValue:F2}\n" +
                                   $"Status: {item.Status}\n" +
                                   $"Reorder Level: {item.ReorderLevel}\n" +
                                   $"Last Updated: {item.LastUpdated:yyyy-MM-dd HH:mm}";

                    MessageBox.Show(details, "Inventory Details",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing details: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Check for low stock items and show alerts
        /// </summary>
        private void CheckLowStockAlerts()
        {
            var lowStockItems = _inventoryService.GetLowStockItems();

            if (lowStockItems.Count > 0)
            {
                // Could show a notification or update a label
                // For now, we'll just track it silently
                // You could add a label to show "X items need reordering"
            }
        }

        // Keep existing event handlers for Designer compatibility
        private void lblTotalValue_Click(object sender, EventArgs e) { }
    }
}


//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class InventoryControl : UserControl
//    {
//        public InventoryControl()
//        {
//            InitializeComponent();
//            this.Text = "Inventory";
//            LoadInventoryData();
//        }

//        private void LoadInventoryData()
//        {
//            // Load the stat cards with temporary data
//            LoadStatCards();

//            // Load the materials table with temporary data
//            LoadMaterialsGrid();
//        }

//        private void LoadStatCards()
//        {
//            // Update the stat card labels with temporary data
//            lblTotalValue.Text = "$45,250.00";
//            lblActiveMaterials.Text = "156";
//            lblMonthlyConsumption.Text = "$12,350.00";
//        }

//        private void LoadMaterialsGrid()
//        {
//            // Create a temporary DataTable with sample data
//            DataTable dt = new DataTable();

//            // Add columns
//            dt.Columns.Add("ID", typeof(string));
//            dt.Columns.Add("Material Name", typeof(string));
//            dt.Columns.Add("Quantity", typeof(string));
//            dt.Columns.Add("Unit Price", typeof(string));
//            dt.Columns.Add("Total", typeof(string));
//            dt.Columns.Add("Status", typeof(string));

//            // Add sample rows
//            dt.Rows.Add("MAT-001", "Fabric - Cotton", "45 meters", "$5.00", "$225.00", "● In Stock");
//            dt.Rows.Add("MAT-002", "Thread - Red", "120 pcs", "$2.00", "$240.00", "● In Stock");
//            dt.Rows.Add("MAT-003", "Thread - Black", "85 pcs", "$2.00", "$170.00", "● In Stock");
//            dt.Rows.Add("MAT-004", "Zipper - 20cm", "30 pcs", "$1.50", "$45.00", "⚠ Low Stock");
//            dt.Rows.Add("MAT-005", "Button - Gold", "0 pcs", "$0.50", "$0.00", "🔴 Out of Stock");
//            dt.Rows.Add("MAT-006", "Button - Silver", "50 pcs", "$0.50", "$25.00", "● In Stock");
//            dt.Rows.Add("MAT-007", "Measuring Tape", "15 pcs", "$3.00", "$45.00", "⚠ Low Stock");
//            dt.Rows.Add("MAT-008", "Scissors - Tailor", "8 pcs", "$12.00", "$96.00", "⚠ Low Stock");
//            dt.Rows.Add("MAT-009", "Needle Set", "25 sets", "$4.00", "$100.00", "● In Stock");
//            dt.Rows.Add("MAT-010", "Thread - White", "200 pcs", "$2.00", "$400.00", "● In Stock");

//            // Assign the data to the DataGridView
//            dgvMaterials.DataSource = dt;

//            // Auto-fit the columns
//            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//            // Color the Status column based on the text
//            dgvMaterials.CellFormatting += (sender, e) =>
//            {
//                if (e.ColumnIndex == dgvMaterials.Columns["Status"].Index && e.Value != null)
//                {
//                    string status = e.Value.ToString();
//                    if (status.Contains("Low Stock"))
//                        e.CellStyle.ForeColor = Color.Orange;
//                    else if (status.Contains("Out of Stock"))
//                        e.CellStyle.ForeColor = Color.Red;
//                    else
//                        e.CellStyle.ForeColor = Color.LightGreen;
//                }
//            };
//        }
//    }
//}