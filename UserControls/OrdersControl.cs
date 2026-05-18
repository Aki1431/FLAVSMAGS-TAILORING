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
    public partial class OrdersControl : UserControl
    {
        private readonly OrderService _orderService;
        private readonly SalesService _salesService;
        private int? _selectedOrderId = null;
        private bool isSlideOpen = false;
        private bool isStatusOpen = false;
        private System.Windows.Forms.Timer slideTimer = null!;
        private System.Windows.Forms.Timer statusTimer = null!;   // fixed CS8618

        public OrdersControl()
        {
            InitializeComponent();
            this.Text = "Orders";
            _orderService = new OrderService();
            _salesService = new SalesService();
            _orderService.DataChanged += OnDataChanged;
            _salesService.DataChanged += OnDataChanged;
            SetupSlideTimer();
            SetupStatusTimer();
            LoadOrderCards();
        }

        // ========== Detail Slide Timer ==========
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
                if (panelSlide.Width < 400)
                    panelSlide.Width += step;
                else { panelSlide.Width = 400; slideTimer.Stop(); }
            }
            else
            {
                if (panelSlide.Width > 0)
                    panelSlide.Width -= step;
                else { panelSlide.Width = 0; slideTimer.Stop(); }
            }
        }

        // ========== Status Panel Timer (centered popup) ==========
        private void SetupStatusTimer()
        {
            statusTimer = new System.Windows.Forms.Timer { Interval = 15 };
            statusTimer.Tick += StatusTimer_Tick;
        }

        private void StatusTimer_Tick(object? sender, EventArgs e)
        {
            int step = 40;
            if (isStatusOpen)
            {
                if (panelStatus.Width < 350)
                    panelStatus.Width += step;
                else { panelStatus.Width = 350; statusTimer.Stop(); }
            }
            else
            {
                if (panelStatus.Width > 0)
                    panelStatus.Width -= step;
                else { panelStatus.Width = 0; statusTimer.Stop(); panelStatus.Visible = false; }
            }
        }

        // ========== Open / Close methods ==========
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
            btnAdd.Text = "Add";
        }

        private void OpenStatusPanel(int orderId)
        {
            _selectedOrderId = orderId;
            var order = _orderService.GetAllOrders().FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
                cmbStatus.SelectedItem = order.Status.ToString();
            panelStatus.Visible = true;
            isStatusOpen = true;
            panelStatus.BringToFront();
            statusTimer.Start();
        }
        private void CloseStatusPanel()
        {
            isStatusOpen = false;
            statusTimer.Start();
        }

        // ========== Calculate Total ==========
        private void CalculateTotal(object? sender, EventArgs e)
        {
            if (decimal.TryParse(txtUnitPrice.Text, out decimal price) && numQuantity.Value > 0)
                lblTotalValue.Text = $"₱{(price * numQuantity.Value):N2}";
            else lblTotalValue.Text = "₱0.00";
        }

        // ========== Data Refresh ==========
        private void OnDataChanged(object? sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)(() => LoadOrderCards()));
            else LoadOrderCards();
        }

        // Grace period: 3 days after deletion
        private void LoadOrderCards(string filter = "")
        {
            flowOrders.Controls.Clear();
            var now = DateTime.Now;
            IEnumerable<Order> orders = _orderService.GetAllOrders()
                .Where(o => o.DeletedDate == null ||
                            (now - o.DeletedDate.Value).TotalDays < 3)  // show for 3 days after deletion
                .OrderByDescending(o => o.OrderDate);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                orders = orders.Where(o =>
                    o.CustomerName.ToLower().Contains(filter.ToLower()) ||
                    o.OrderId.ToString().Contains(filter));
            }

            foreach (var order in orders)
                flowOrders.Controls.Add(CreateOrderCard(order));
        }

        // ========== Card Creation (with "Status" button) ==========
        private Guna2Panel CreateOrderCard(Order order)
        {
            Guna2Panel card = new Guna2Panel
            {
                Width = flowOrders.Width - 25,
                Height = 100,
                BackColor = Color.Transparent,
                FillColor = order.DeletedDate != null ? Color.FromArgb(80, 40, 40) : Color.FromArgb(45, 44, 90),
                BorderRadius = 10,
                Margin = new Padding(5),
                ShadowDecoration = { Enabled = true, Depth = 5 },
                Tag = order
            };

            Label lblId = new Label { Text = $"ORD-{order.OrderId}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 8), AutoSize = true };
            Label lblDate = new Label { Text = order.OrderDate.ToString("dd/MM/yyyy"), Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(10, 30), AutoSize = true };
            Label lblCustomer = new Label { Text = order.CustomerName, Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = Color.White, Location = new Point(160, 8), AutoSize = true };
            Label lblContact = new Label { Text = order.ContactNumber ?? "", Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(160, 30), AutoSize = true };
            Label lblService = new Label { Text = order.ServiceType, Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(350, 8), AutoSize = true };
            Label lblItem = new Label { Text = $"{order.ItemType} x {order.Quantity}", Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(350, 28), AutoSize = true };
            Label lblAmount = new Label { Text = $"₱{order.TotalAmount:N2}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(550, 8), AutoSize = true };
            Label lblStatus = new Label { Text = order.DeletedDate != null ? "Deleted" : order.Status.ToString(), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = GetStatusColor(order.Status), Location = new Point(550, 32), AutoSize = true };

            // "Change Status" button
            Guna2Button btnStatus = new Guna2Button
            {
                Text = "Status",
                BorderRadius = 6,
                FillColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Size = new Size(70, 26),
                Location = new Point(650, 55)
            };
            btnStatus.Click += (s, e) =>
            {
                OpenStatusPanel(order.OrderId);
            };

            card.Click += (s, e) =>
            {
                _selectedOrderId = order.OrderId;
                FillFormFromOrder(order);
                lblSlideTitle.Text = "Edit Order";
                btnDelete.Visible = true;
                OpenSlide();
            };

            card.Controls.AddRange(new Control[] { lblId, lblDate, lblCustomer, lblContact, lblService, lblItem, lblAmount, lblStatus, btnStatus });
            return card;
        }

        private Color GetStatusColor(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => Color.Orange,
                OrderStatus.Processing => Color.DodgerBlue,
                OrderStatus.Ready => Color.MediumSeaGreen,
                OrderStatus.Completed => Color.Green,
                OrderStatus.Cancelled => Color.Red,
                _ => Color.Gray
            };
        }

        private void FillFormFromOrder(Order order)
        {
            txtCustomer.Text = order.CustomerName;
            txtContact.Text = order.ContactNumber ?? "";
            txtEmail.Text = order.Email ?? "";
            cmbService.SelectedItem = order.ServiceType ?? (cmbService.Items.Count > 0 ? cmbService.Items[0] : null);
            cmbItemType.SelectedItem = order.ItemType ?? (cmbItemType.Items.Count > 0 ? cmbItemType.Items[0] : null);
            numQuantity.Value = order.Quantity > 0 ? order.Quantity : 1;
            txtUnitPrice.Text = (order.Quantity > 0 ? order.TotalAmount / order.Quantity : 0).ToString("F2");
            CalculateTotal(null, EventArgs.Empty);
        }

        // ========== Add / Edit / Delete ==========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isSlideOpen) { CloseSlide(); return; }
            _selectedOrderId = null;
            ClearForm();
            lblSlideTitle.Text = "Add Order";
            btnDelete.Visible = false;
            OpenSlide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text) || cmbService.SelectedItem == null ||
                cmbItemType.SelectedItem == null || !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice <= 0)
            {
                MessageBox.Show("Please fill all required fields correctly.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int qty = (int)numQuantity.Value;
            decimal total = unitPrice * qty;
            string service = cmbService.SelectedItem!.ToString()!;
            string itemType = cmbItemType.SelectedItem!.ToString()!;
            try
            {
                if (_selectedOrderId.HasValue)
                {
                    var result = _orderService.UpdateOrderFull(_selectedOrderId.Value, txtCustomer.Text.Trim(), txtContact.Text.Trim(), txtEmail.Text.Trim(), service, itemType, qty, total);
                    if (result.IsSuccess) { MessageBox.Show("Order updated!", "Success"); CloseSlide(); }
                    else MessageBox.Show(result.Message, "Error");
                }
                else
                {
                    var result = _orderService.CreateOrderFull(txtCustomer.Text.Trim(), txtContact.Text.Trim(), txtEmail.Text.Trim(), service, itemType, qty, total);
                    if (result.IsSuccess) { MessageBox.Show("Order created!", "Success"); CloseSlide(); }
                    else MessageBox.Show(result.Message, "Error");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        // ----- Grace‑period soft delete -----
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue) return;
            if (MessageBox.Show("Delete this order? It will be hidden after 3 days but kept for reporting.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var order = _orderService.GetAllOrders().FirstOrDefault(o => o.OrderId == _selectedOrderId.Value);
                if (order == null) return;

                // Mark as deleted (soft delete)
                order.DeletedDate = DateTime.Now;
                order.Status = OrderStatus.Cancelled;
                var result = _orderService.UpdateOrder(order.OrderId, order.CustomerName, order.Items, order.TotalAmount, order.Status, order.Notes);
                // (If you have a more comprehensive update method, use that)

                if (result.IsSuccess)
                {
                    MessageBox.Show("Order marked for deletion. It will disappear after 3 days.", "Success");
                    CloseSlide();
                }
                else MessageBox.Show(result.Message, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => CloseSlide();

        // ========== Status Panel Save – auto‑record sale ==========
        private void btnStatusSave_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue || cmbStatus.SelectedItem == null) return;
            if (Enum.TryParse<OrderStatus>(cmbStatus.SelectedItem.ToString(), out OrderStatus newStatus))
            {
                var result = _orderService.ChangeOrderStatus(_selectedOrderId.Value, newStatus);
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Error");
                    return;
                }

                // Auto‑record sale when status becomes Completed
                if (newStatus == OrderStatus.Completed)
                {
                    var order = _orderService.GetAllOrders().FirstOrDefault(o => o.OrderId == _selectedOrderId.Value);
                    if (order != null)
                    {
                        string category = order.ItemType ?? "General";
                        int unitsSold = order.Quantity;
                        decimal revenue = order.TotalAmount;
                        decimal cost = 0;   // you can add cost tracking later
                        var saleResult = _salesService.RecordSale(order.OrderId, category, unitsSold, revenue, cost);
                        if (!saleResult.IsSuccess)
                            MessageBox.Show("Order completed, but failed to record sale: " + saleResult.Message, "Warning");
                    }
                }

                MessageBox.Show("Status updated!", "Success");
                CloseStatusPanel();
            }
        }

        private void btnStatusReturn_Click(object sender, EventArgs e) => CloseStatusPanel();

        // ========== Search ==========
        private void btnSearch_Click(object sender, EventArgs e) => LoadOrderCards(txtSearch.Text.Trim());

        private void ClearForm()
        {
            txtCustomer.Clear(); txtContact.Clear(); txtEmail.Clear();
            cmbService.SelectedIndex = -1; cmbItemType.SelectedIndex = -1;
            numQuantity.Value = 1; txtUnitPrice.Text = "0.00"; lblTotalValue.Text = "₱0.00";
            _selectedOrderId = null;
        }
    }
}

//using System;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using FLAVSMAGS_TAILORING.Services;
//using FLAVSMAGS_TAILORING.Models;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class OrdersControl : UserControl
//    {
//        private readonly OrderService _orderService;
//        private int? _selectedOrderId = null;
//        private bool isSlideOpen = false;
//        private System.Windows.Forms.Timer slideTimer;

//        public OrdersControl()
//        {
//            InitializeComponent();
//            this.Text = "Orders";
//            _orderService = new OrderService();
//            _orderService.DataChanged += OnDataChanged;
//            SetupSlideTimer();
//            LoadOrderCards();
//        }

//        // ---------- Sliding panel animation ----------
//        private void SetupSlideTimer()
//        {
//            slideTimer = new System.Windows.Forms.Timer { Interval = 15 };
//            slideTimer.Tick += SlideTimer_Tick;
//        }

//        private void SlideTimer_Tick(object? sender, EventArgs e)
//        {
//            if (isSlideOpen)
//            {
//                if (panelSlide.Width < 300)
//                    panelSlide.Width += 30;
//                else
//                {
//                    panelSlide.Width = 300;
//                    slideTimer.Stop();
//                }
//            }
//            else
//            {
//                if (panelSlide.Width > 0)
//                    panelSlide.Width -= 30;
//                else
//                {
//                    panelSlide.Width = 0;
//                    slideTimer.Stop();
//                }
//            }
//        }

//        private void OpenSlide()
//        {
//            isSlideOpen = true;
//            slideTimer.Start();
//            panelSlide.BringToFront();   // ensure on top
//            btnAdd.Text = "Return";
//        }

//        private void CloseSlide()
//        {
//            isSlideOpen = false;
//            slideTimer.Start();
//            ClearForm();
//            btnAdd.Text = "Add";
//        }

//        // ---------- Order cards (built-in panels) ----------
//        private void OnDataChanged(object? sender, EventArgs e)
//        {
//            LoadOrderCards();
//        }

//        private void LoadOrderCards()
//        {
//            flowOrders.Controls.Clear();
//            var orders = _orderService.GetAllOrders()
//                                      .OrderByDescending(o => o.OrderDate);

//            foreach (var order in orders)
//            {
//                flowOrders.Controls.Add(CreateOrderCard(order));
//            }
//        }

//        private Panel CreateOrderCard(Order order)
//        {
//            Panel card = new Panel
//            {
//                Width = flowOrders.Width - 25,
//                Height = 80,
//                BackColor = Color.FromArgb(45, 44, 90),
//                Margin = new Padding(5),
//                Tag = order
//            };

//            Label lblId = new Label
//            {
//                Text = $"ORD-{order.OrderId}",
//                Font = new Font("Segoe UI", 10, FontStyle.Bold),
//                ForeColor = Color.White,
//                Location = new Point(10, 8),
//                AutoSize = true
//            };

//            Label lblCustomer = new Label
//            {
//                Text = order.CustomerName,
//                Font = new Font("Segoe UI", 10, FontStyle.Regular),
//                ForeColor = Color.White,
//                Location = new Point(10, 32),
//                AutoSize = true
//            };

//            Label lblItems = new Label
//            {
//                Text = order.Items,
//                Font = new Font("Segoe UI", 8),
//                ForeColor = Color.LightGray,
//                Location = new Point(200, 32),
//                AutoSize = true
//            };

//            Label lblAmount = new Label
//            {
//                Text = $"${order.TotalAmount:F2}",
//                Font = new Font("Segoe UI", 10, FontStyle.Bold),
//                ForeColor = Color.Gold,
//                Location = new Point(400, 8),
//                AutoSize = true
//            };

//            Label lblStatus = new Label
//            {
//                Text = order.Status.ToString(),
//                Font = new Font("Segoe UI", 9, FontStyle.Bold),
//                ForeColor = GetStatusColor(order.Status),
//                Location = new Point(400, 32),
//                AutoSize = true
//            };

//            Label lblDate = new Label
//            {
//                Text = order.OrderDate.ToString("dd/MM/yyyy"),
//                Font = new Font("Segoe UI", 8),
//                ForeColor = Color.LightGray,
//                Location = new Point(600, 8),
//                AutoSize = true
//            };

//            card.Click += (s, e) =>
//            {
//                _selectedOrderId = order.OrderId;
//                txtCustomer.Text = order.CustomerName;
//                txtItems.Text = order.Items;
//                txtTotal.Text = order.TotalAmount.ToString("F2");
//                lblSlideTitle.Text = "Edit Order";
//                btnDelete.Visible = true;
//                OpenSlide();
//            };

//            card.Controls.AddRange(new Control[] { lblId, lblCustomer, lblItems, lblAmount, lblStatus, lblDate });
//            return card;
//        }

//        private Color GetStatusColor(OrderStatus status)
//        {
//            return status switch
//            {
//                OrderStatus.Pending => Color.Orange,
//                OrderStatus.Processing => Color.DodgerBlue,
//                OrderStatus.Ready => Color.MediumSeaGreen,
//                OrderStatus.Completed => Color.Green,
//                OrderStatus.Cancelled => Color.Red,
//                _ => Color.Gray
//            };
//        }

//        // ---------- Add / Edit / Delete ----------
//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            if (isSlideOpen)
//            {
//                CloseSlide();
//                return;
//            }

//            _selectedOrderId = null;
//            ClearForm();
//            lblSlideTitle.Text = "Add Order";
//            btnDelete.Visible = false;
//            OpenSlide();
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtCustomer.Text) ||
//                string.IsNullOrWhiteSpace(txtItems.Text) ||
//                !decimal.TryParse(txtTotal.Text, out decimal amount) || amount <= 0)
//            {
//                MessageBox.Show("Please fill all fields correctly.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                if (_selectedOrderId.HasValue)
//                {
//                    var result = _orderService.UpdateOrder(_selectedOrderId.Value, txtCustomer.Text, txtItems.Text, amount, OrderStatus.Pending, "");
//                    if (result.IsSuccess)
//                    {
//                        MessageBox.Show("Order updated!", "Success");
//                        CloseSlide();
//                    }
//                    else MessageBox.Show(result.Message, "Error");
//                }
//                else
//                {
//                    var result = _orderService.CreateOrder(txtCustomer.Text, txtItems.Text, amount);
//                    if (result.IsSuccess)
//                    {
//                        MessageBox.Show("Order created!", "Success");
//                        CloseSlide();
//                    }
//                    else MessageBox.Show(result.Message, "Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Error");
//            }
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (!_selectedOrderId.HasValue) return;
//            var confirm = MessageBox.Show("Delete this order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//            if (confirm == DialogResult.Yes)
//            {
//                var result = _orderService.DeleteOrder(_selectedOrderId.Value);
//                if (result.IsSuccess)
//                {
//                    MessageBox.Show("Order deleted.", "Success");
//                    CloseSlide();
//                }
//                else MessageBox.Show(result.Message, "Error");
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            CloseSlide();
//        }

//        private void ClearForm()
//        {
//            txtCustomer.Clear();
//            txtItems.Clear();
//            txtTotal.Clear();
//            _selectedOrderId = null;
//        }

//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            LoadOrderCards();
//        }
//    }
//}