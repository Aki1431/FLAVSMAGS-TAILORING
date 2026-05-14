using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Services;
using FLAVSMAGS_TAILORING.Models;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class OrdersControl : UserControl
    {
        private readonly OrderService _orderService;
        private int? _selectedOrderId = null; // Track selected order for editing

        public OrdersControl()
        {
            InitializeComponent();
            this.Text = "Orders";

            // Initialize service
            _orderService = new OrderService();

            // Subscribe to data changes
            _orderService.DataChanged += OnDataChanged;

            // Load initial data
            LoadActiveOrders();
        }

        /// <summary>
        /// Event handler for data changes - refresh grid
        /// </summary>
        private void OnDataChanged(object? sender, EventArgs e)
        {
            LoadActiveOrders();
        }

        /// <summary>
        /// Load orders from service into DataGridView
        /// </summary>
        private void LoadActiveOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders();

                DataTable dt = new DataTable();
                dt.Columns.Add("Order ID", typeof(int));
                dt.Columns.Add("Customer Name", typeof(string));
                dt.Columns.Add("Items", typeof(string));
                dt.Columns.Add("Total Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Order Date", typeof(string));

                foreach (var order in orders.OrderByDescending(o => o.OrderDate))
                {
                    dt.Rows.Add(
                        order.OrderId,
                        order.CustomerName,
                        order.Items,
                        $"${order.TotalAmount:F2}",
                        order.Status.ToString(),
                        order.OrderDate.ToString("yyyy-MM-dd")
                    );
                }

                dgvActiveOrders.DataSource = dt;
                dgvActiveOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Style the grid
                dgvActiveOrders.BackgroundColor = System.Drawing.Color.FromArgb(37, 36, 81);
                dgvActiveOrders.ForeColor = System.Drawing.Color.White;
                dgvActiveOrders.GridColor = System.Drawing.Color.FromArgb(90, 88, 140);
                dgvActiveOrders.EnableHeadersVisualStyles = false;
                dgvActiveOrders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(90, 88, 140);
                dgvActiveOrders.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

                // Allow row selection
                dgvActiveOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvActiveOrders.MultiSelect = false;

                // Add row selection event
                dgvActiveOrders.SelectionChanged += DgvActiveOrders_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handle row selection - load order into form for editing
        /// </summary>
        private void DgvActiveOrders_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvActiveOrders.SelectedRows.Count == 0)
                return;

            try
            {
                var selectedRow = dgvActiveOrders.SelectedRows[0];
                var orderId = Convert.ToInt32(selectedRow.Cells["Order ID"].Value);

                var order = _orderService.GetAllOrders().FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    _selectedOrderId = order.OrderId;
                    txtCustomer.Text = order.CustomerName;
                    txtItems.Text = order.Items;
                    txtTotal.Text = order.TotalAmount.ToString("F2");

                    // Change button text to "Update Order"
                    btnCreateOrder.Text = "Update Order";
                }
            }
            catch (Exception ex)
            {
                // Silently handle - not critical
            }
        }

        /// <summary>
        /// Create or Update order
        /// </summary>
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer name!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtItems.Text))
            {
                MessageBox.Show("Please enter items!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtTotal.Text, out decimal totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("Please enter a valid total amount!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_selectedOrderId.HasValue)
                {
                    // UPDATE existing order
                    var result = _orderService.UpdateOrder(
                        _selectedOrderId.Value,
                        txtCustomer.Text,
                        txtItems.Text,
                        totalAmount,
                        OrderStatus.Pending, // Keep status as pending for manual changes
                        string.Empty
                    );

                    if (result.IsSuccess)
                    {
                        MessageBox.Show($"Order #{_selectedOrderId} updated successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Update Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // CREATE new order
                    var result = _orderService.CreateOrder(
                        txtCustomer.Text,
                        txtItems.Text,
                        totalAmount
                    );

                    if (result.IsSuccess)
                    {
                        MessageBox.Show($"Order #{result.Data!.OrderId} created successfully!\n" +
                                      $"Customer: {result.Data.CustomerName}\n" +
                                      $"Items: {result.Data.Items}\n" +
                                      $"Total: ${result.Data.TotalAmount:F2}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Creation Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Operation Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear form and reset to create mode
        /// </summary>
        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Clear all form fields
        /// </summary>
        private void ClearForm()
        {
            txtCustomer.Clear();
            txtItems.Clear();
            txtTotal.Clear();
            _selectedOrderId = null;
            btnCreateOrder.Text = "Create Order";

            // Clear selection in grid
            dgvActiveOrders.ClearSelection();
        }

        /// <summary>
        /// Search orders by customer name or ID
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    // If search is empty, reload all orders
                    LoadActiveOrders();
                    return;
                }

                var searchResults = _orderService.SearchOrders(txtSearch.Text);

                DataTable dt = new DataTable();
                dt.Columns.Add("Order ID", typeof(int));
                dt.Columns.Add("Customer Name", typeof(string));
                dt.Columns.Add("Items", typeof(string));
                dt.Columns.Add("Total Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Order Date", typeof(string));

                foreach (var order in searchResults.OrderByDescending(o => o.OrderDate))
                {
                    dt.Rows.Add(
                        order.OrderId,
                        order.CustomerName,
                        order.Items,
                        $"${order.TotalAmount:F2}",
                        order.Status.ToString(),
                        order.OrderDate.ToString("yyyy-MM-dd")
                    );
                }

                dgvActiveOrders.DataSource = dt;

                if (searchResults.Count == 0)
                {
                    MessageBox.Show("No orders found matching your search.",
                        "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}",
                    "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Keep existing empty event handlers for Designer compatibility
        private void label6_Click(object sender, EventArgs e) { }
        private void panelCreateOrder_Paint(object sender, PaintEventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}

//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class OrdersControl : UserControl
//    {
//        public OrdersControl()
//        {
//            InitializeComponent();
//            this.Text = "Orders";
//            LoadActiveOrders();
//        }

//        private void LoadActiveOrders()
//        {
//            DataTable dt = new DataTable();
//            dt.Columns.Add("Order ID", typeof(int));
//            dt.Columns.Add("Customer Name", typeof(string));
//            dt.Columns.Add("Items", typeof(string));
//            dt.Columns.Add("Total Amount", typeof(string));
//            dt.Columns.Add("Status", typeof(string));
//            dt.Columns.Add("Order Date", typeof(string));

//            dt.Rows.Add(1001, "Scooby Jew", "2 pcs", "$150.00", "Pending", "2024-05-14");
//            dt.Rows.Add(1002, "Adolf Rizzler", "3 pcs", "$230.00", "Processing", "2024-05-13");
//            dt.Rows.Add(1003, "Nick Gher", "1 pc", "$75.00", "Ready", "2024-05-13");

//            dgvActiveOrders.DataSource = dt;
//            dgvActiveOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//        }

//        private void btnCreateOrder_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
//            {
//                MessageBox.Show("Please enter customer name!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            MessageBox.Show($"Order created for {txtCustomer.Text}!\nItems: {txtItems.Text}\nTotal: ${txtTotal.Text}",
//                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            ClearForm();
//            LoadActiveOrders();
//        }

//        private void btnClearOrder_Click(object sender, EventArgs e)
//        {
//            ClearForm();
//        }

//        private void ClearForm()
//        {
//            txtCustomer.Clear();
//            txtItems.Clear();
//            txtTotal.Clear();
//        }

//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtSearch.Text))
//            {
//                LoadActiveOrders();
//            }
//            else
//            {
//                MessageBox.Show($"Searching for: {txtSearch.Text}");
//            }
//        }

//        private void label6_Click(object sender, EventArgs e)
//        {

//        }

//        private void panelCreateOrder_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void label5_Click(object sender, EventArgs e)
//        {

//        }

//        private void label4_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}