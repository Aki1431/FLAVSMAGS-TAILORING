using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class OrdersControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Sliding panel
            panelSlide = new Guna2Panel();
            lblSlideTitle = new Guna2HtmlLabel();
            // --- NEW Status Panel ---
            panelStatus = new Guna2Panel();
            lblStatusTitle = new Guna2HtmlLabel();
            cmbStatus = new Guna2ComboBox();
            btnStatusSave = new Guna2Button();
            btnStatusReturn = new Guna2Button();

            lblCustomer = new Guna2HtmlLabel();
            txtCustomer = new Guna2TextBox();
            lblContact = new Guna2HtmlLabel();
            txtContact = new Guna2TextBox();
            lblEmail = new Guna2HtmlLabel();
            txtEmail = new Guna2TextBox();
            lblService = new Guna2HtmlLabel();
            cmbService = new Guna2ComboBox();
            lblItemType = new Guna2HtmlLabel();
            cmbItemType = new Guna2ComboBox();
            lblQuantity = new Guna2HtmlLabel();
            numQuantity = new Guna2NumericUpDown();
            lblUnitPrice = new Guna2HtmlLabel();
            txtUnitPrice = new Guna2TextBox();
            lblTotalAmount = new Guna2HtmlLabel();
            lblTotalValue = new Guna2HtmlLabel();
            btnSave = new Guna2Button();
            btnDelete = new Guna2Button();
            btnCancel = new Guna2Button();

            // Top panel
            panelTop = new Guna2Panel();
            txtSearch = new Guna2TextBox();
            btnSearch = new Guna2Button();
            btnAdd = new Guna2Button();
            label6 = new Guna2HtmlLabel();

            // Card list
            flowOrders = new FlowLayoutPanel();

            panelSlide.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();

            // =============== SLIDING PANEL ===============
            panelSlide.BackColor = Color.FromArgb(30, 30, 40);
            panelSlide.BorderRadius = 15;
            panelSlide.FillColor = Color.FromArgb(45, 44, 90);
            panelSlide.ShadowDecoration.Enabled = true;
            panelSlide.ShadowDecoration.Depth = 20;
            panelSlide.Location = new Point(0, 60);
            panelSlide.Name = "panelSlide";
            panelSlide.Size = new Size(0, 580);
            panelSlide.TabIndex = 0;

            // Slide Title
            lblSlideTitle.BackColor = Color.Transparent;
            lblSlideTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSlideTitle.ForeColor = Color.White;
            lblSlideTitle.Location = new Point(20, 20);
            lblSlideTitle.Name = "lblSlideTitle";
            lblSlideTitle.Text = "Add Order";

            // Customer Name
            lblCustomer.BackColor = Color.Transparent;
            lblCustomer.Font = new Font("Segoe UI", 9);
            lblCustomer.ForeColor = Color.White;
            lblCustomer.Location = new Point(20, 60);
            lblCustomer.Text = "Customer Name";
            txtCustomer.BorderRadius = 8;
            txtCustomer.FillColor = Color.FromArgb(60, 60, 80);
            txtCustomer.ForeColor = Color.White;
            txtCustomer.Location = new Point(20, 80);
            txtCustomer.Size = new Size(350, 36);
            txtCustomer.Name = "txtCustomer";

            // Contact Number
            lblContact.BackColor = Color.Transparent;
            lblContact.Font = new Font("Segoe UI", 9);
            lblContact.ForeColor = Color.White;
            lblContact.Location = new Point(20, 130);
            lblContact.Text = "Contact Number";
            txtContact.BorderRadius = 8;
            txtContact.FillColor = Color.FromArgb(60, 60, 80);
            txtContact.ForeColor = Color.White;
            txtContact.Location = new Point(20, 150);
            txtContact.Size = new Size(350, 36);
            txtContact.Name = "txtContact";

            // Email
            lblEmail.BackColor = Color.Transparent;
            lblEmail.Font = new Font("Segoe UI", 9);
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(20, 200);
            lblEmail.Text = "E‑mail Address";
            txtEmail.BorderRadius = 8;
            txtEmail.FillColor = Color.FromArgb(60, 60, 80);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(20, 220);
            txtEmail.Size = new Size(350, 36);
            txtEmail.Name = "txtEmail";

            // Service (ComboBox)
            lblService.BackColor = Color.Transparent;
            lblService.Font = new Font("Segoe UI", 9);
            lblService.ForeColor = Color.White;
            lblService.Location = new Point(20, 270);
            lblService.Text = "Available Service";
            cmbService.BorderRadius = 8;
            cmbService.FillColor = Color.FromArgb(60, 60, 80);
            cmbService.ForeColor = Color.White;
            cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbService.Location = new Point(20, 290);
            cmbService.Size = new Size(350, 36);
            cmbService.Items.AddRange(new[] { "Clothing Alterations", "Custom Garment Creation", "Repairs & Restoration" });
            cmbService.Name = "cmbService";

            // Item Type (ComboBox)
            lblItemType.BackColor = Color.Transparent;
            lblItemType.Font = new Font("Segoe UI", 9);
            lblItemType.ForeColor = Color.White;
            lblItemType.Location = new Point(20, 340);
            lblItemType.Text = "Specific Item";
            cmbItemType.BorderRadius = 8;
            cmbItemType.FillColor = Color.FromArgb(60, 60, 80);
            cmbItemType.ForeColor = Color.White;
            cmbItemType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbItemType.Location = new Point(20, 360);
            cmbItemType.Size = new Size(350, 36);
            cmbItemType.Items.AddRange(new[] { "Pants/Jeans", "Shirts/Blouses", "Jackets/Coats", "Dresses/Skirts", "Formal Wear" });
            cmbItemType.Name = "cmbItemType";

            // Quantity (Numeric UpDown)
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Font = new Font("Segoe UI", 9);
            lblQuantity.ForeColor = Color.White;
            lblQuantity.Location = new Point(20, 410);
            lblQuantity.Text = "Quantity";
            numQuantity.BorderRadius = 8;
            numQuantity.FillColor = Color.FromArgb(60, 60, 80);
            numQuantity.ForeColor = Color.White;
            numQuantity.Location = new Point(20, 430);
            numQuantity.Size = new Size(120, 36);
            numQuantity.Minimum = 1;
            numQuantity.Maximum = 100;
            numQuantity.Name = "numQuantity";
            numQuantity.ValueChanged += CalculateTotal;

            // Unit Price
            lblUnitPrice.BackColor = Color.Transparent;
            lblUnitPrice.Font = new Font("Segoe UI", 9);
            lblUnitPrice.ForeColor = Color.White;
            lblUnitPrice.Location = new Point(160, 410);
            lblUnitPrice.Text = "Unit Price (₱)";
            txtUnitPrice.BorderRadius = 8;
            txtUnitPrice.FillColor = Color.FromArgb(60, 60, 80);
            txtUnitPrice.ForeColor = Color.White;
            txtUnitPrice.Location = new Point(160, 430);
            txtUnitPrice.Size = new Size(140, 36);
            txtUnitPrice.Text = "0.00";
            txtUnitPrice.TextChanged += CalculateTotal;
            txtUnitPrice.Name = "txtUnitPrice";

            // Total Amount (auto‑calculated)
            lblTotalAmount.BackColor = Color.Transparent;
            lblTotalAmount.Font = new Font("Segoe UI", 9);
            lblTotalAmount.ForeColor = Color.White;
            lblTotalAmount.Location = new Point(20, 480);
            lblTotalAmount.Text = "Total Amount (₱)";
            lblTotalValue.BackColor = Color.Transparent;
            lblTotalValue.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.Gold;
            lblTotalValue.Location = new Point(20, 500);
            lblTotalValue.Text = "₱0.00";
            lblTotalValue.Name = "lblTotalValue";

            // Buttons
            btnSave.BorderRadius = 8;
            btnSave.FillColor = Color.FromArgb(76, 175, 80);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 530);
            btnSave.Size = new Size(110, 36);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            btnSave.Name = "btnSave";

            btnDelete.BorderRadius = 8;
            btnDelete.FillColor = Color.FromArgb(244, 67, 54);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(140, 530);
            btnDelete.Size = new Size(110, 36);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            btnDelete.Name = "btnDelete";

            btnCancel.BorderRadius = 8;
            btnCancel.FillColor = Color.Gray;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(260, 530);
            btnCancel.Size = new Size(110, 36);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            btnCancel.Name = "btnCancel";

            // Add all slide controls
            panelSlide.Controls.Add(lblSlideTitle);
            panelSlide.Controls.Add(lblCustomer);
            panelSlide.Controls.Add(txtCustomer);
            panelSlide.Controls.Add(lblContact);
            panelSlide.Controls.Add(txtContact);
            panelSlide.Controls.Add(lblEmail);
            panelSlide.Controls.Add(txtEmail);
            panelSlide.Controls.Add(lblService);
            panelSlide.Controls.Add(cmbService);
            panelSlide.Controls.Add(lblItemType);
            panelSlide.Controls.Add(cmbItemType);
            panelSlide.Controls.Add(lblQuantity);
            panelSlide.Controls.Add(numQuantity);
            panelSlide.Controls.Add(lblUnitPrice);
            panelSlide.Controls.Add(txtUnitPrice);
            panelSlide.Controls.Add(lblTotalAmount);
            panelSlide.Controls.Add(lblTotalValue);
            panelSlide.Controls.Add(btnSave);
            panelSlide.Controls.Add(btnDelete);
            panelSlide.Controls.Add(btnCancel);

            // =============== STATUS PANEL (centered card) ===============
            panelStatus.BackColor = Color.FromArgb(30, 30, 40);
            panelStatus.BorderRadius = 15;
            panelStatus.FillColor = Color.FromArgb(45, 44, 90);
            panelStatus.ShadowDecoration.Enabled = true;
            panelStatus.ShadowDecoration.Depth = 25;
            panelStatus.Location = new Point(250, 150);   // centered approx
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(0, 250);          // initially collapsed
            panelStatus.Visible = false;                  // hidden by default
            panelStatus.TabIndex = 10;

            // Status Title
            lblStatusTitle.BackColor = Color.Transparent;
            lblStatusTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblStatusTitle.ForeColor = Color.White;
            lblStatusTitle.Location = new Point(20, 20);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Text = "Update Order Status";

            // Status ComboBox
            cmbStatus.BorderRadius = 8;
            cmbStatus.FillColor = Color.FromArgb(60, 60, 80);
            cmbStatus.ForeColor = Color.White;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new[] { "Pending", "Processing", "Ready", "Completed", "Cancelled" });
            cmbStatus.Location = new Point(20, 60);
            cmbStatus.Size = new Size(260, 36);
            cmbStatus.Name = "cmbStatus";

            // Save button
            btnStatusSave.BorderRadius = 8;
            btnStatusSave.FillColor = Color.FromArgb(76, 175, 80);
            btnStatusSave.ForeColor = Color.White;
            btnStatusSave.Location = new Point(20, 120);
            btnStatusSave.Size = new Size(120, 36);
            btnStatusSave.Text = "Save";
            btnStatusSave.Click += btnStatusSave_Click;
            btnStatusSave.Name = "btnStatusSave";

            // Return button
            btnStatusReturn.BorderRadius = 8;
            btnStatusReturn.FillColor = Color.Gray;
            btnStatusReturn.ForeColor = Color.White;
            btnStatusReturn.Location = new Point(160, 120);
            btnStatusReturn.Size = new Size(120, 36);
            btnStatusReturn.Text = "Return";
            btnStatusReturn.Click += btnStatusReturn_Click;
            btnStatusReturn.Name = "btnStatusReturn";

            // Add controls to status panel
            panelStatus.Controls.Add(lblStatusTitle);
            panelStatus.Controls.Add(cmbStatus);
            panelStatus.Controls.Add(btnStatusSave);
            panelStatus.Controls.Add(btnStatusReturn);

            // =============== TOP PANEL ===============
            panelTop.Dock = DockStyle.Top;
            panelTop.FillColor = Color.FromArgb(26, 25, 62);
            panelTop.Height = 60;
            panelTop.Name = "panelTop";

            txtSearch.BorderRadius = 8;
            txtSearch.FillColor = Color.FromArgb(50, 50, 70);
            txtSearch.ForeColor = Color.White;
            txtSearch.Location = new Point(140, 18);
            txtSearch.Size = new Size(200, 36);
            txtSearch.Name = "txtSearch";

            btnSearch.BorderRadius = 8;
            btnSearch.FillColor = Color.FromArgb(26, 25, 62);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(350, 17);
            btnSearch.Size = new Size(80, 36);
            btnSearch.Text = "Search";
            btnSearch.Click += btnSearch_Click;
            btnSearch.Name = "btnSearch";

            btnAdd.BorderRadius = 8;
            btnAdd.FillColor = Color.FromArgb(76, 175, 80);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(800, 15);
            btnAdd.Size = new Size(120, 36);
            btnAdd.Text = "Add Order";
            btnAdd.Click += btnAdd_Click;
            btnAdd.Name = "btnAdd";

            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 9);
            label6.ForeColor = Color.White;
            label6.Location = new Point(20, 21);
            label6.Text = "Search Orders";
            label6.Name = "label6";

            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(label6);

            // =============== CARD LIST ===============
            flowOrders.BackColor = Color.FromArgb(26, 25, 62);
            flowOrders.Dock = DockStyle.Fill;
            flowOrders.AutoScroll = true;
            flowOrders.FlowDirection = FlowDirection.TopDown;
            flowOrders.Padding = new Padding(10);
            flowOrders.Location = new Point(0, 60);
            flowOrders.Size = new Size(949, 580);
            flowOrders.Name = "flowOrders";

            // =============== PARENT CONTROL ===============
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(flowOrders);
            Controls.Add(panelTop);
            Controls.Add(panelSlide);
            Controls.Add(panelStatus);     // add status panel last so it's on top
            Name = "OrdersControl";
            Size = new Size(949, 640);

            panelSlide.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
        }

        // ----- Declarations -----
        private Guna2Panel panelSlide;
        private Guna2HtmlLabel lblSlideTitle;

        // ----- NEW Status Panel Declarations -----
        private Guna2Panel panelStatus;
        private Guna2HtmlLabel lblStatusTitle;
        private Guna2ComboBox cmbStatus;
        private Guna2Button btnStatusSave;
        private Guna2Button btnStatusReturn;

        private Guna2TextBox txtCustomer;
        private Guna2HtmlLabel lblCustomer;
        private Guna2TextBox txtContact;
        private Guna2HtmlLabel lblContact;
        private Guna2TextBox txtEmail;
        private Guna2HtmlLabel lblEmail;
        private Guna2ComboBox cmbService;
        private Guna2HtmlLabel lblService;
        private Guna2ComboBox cmbItemType;
        private Guna2HtmlLabel lblItemType;
        private Guna2NumericUpDown numQuantity;
        private Guna2HtmlLabel lblQuantity;
        private Guna2TextBox txtUnitPrice;
        private Guna2HtmlLabel lblUnitPrice;
        private Guna2HtmlLabel lblTotalAmount;
        private Guna2HtmlLabel lblTotalValue;
        private Guna2Button btnSave;
        private Guna2Button btnDelete;
        private Guna2Button btnCancel;

        private Guna2Panel panelTop;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnAdd;
        private Guna2HtmlLabel label6;

        private FlowLayoutPanel flowOrders;
    }
}

//using Guna.UI2.WinForms;   // <-- required
//using FLAVSMAGS_TAILORING;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    partial class OrdersControl
//    {
//        private System.ComponentModel.IContainer components = null;
//        private Guna2Panel panelSlide;                // modern panel
//        private Guna2HtmlLabel lblSlideTitle;
//        private Guna2TextBox txtCustomer;
//        private Guna2HtmlLabel lblCustomer;
//        private Guna2TextBox txtItems;
//        private Guna2HtmlLabel lblItems;
//        private Guna2TextBox txtTotal;
//        private Guna2HtmlLabel lblTotal;
//        private Guna2Button btnSave;
//        private Guna2Button btnDelete;
//        private Guna2Button btnCancel;
//        private Guna2Panel panelTop;
//        private Guna2TextBox txtSearch;
//        private Guna2Button btnSearch;
//        private Guna2Button btnAdd;
//        private Guna2HtmlLabel label6;
//        private FlowLayoutPanel flowOrders;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//                components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            panelSlide = new Guna2Panel();
//            lblSlideTitle = new Guna2HtmlLabel();
//            txtCustomer = new Guna2TextBox();
//            lblCustomer = new Guna2HtmlLabel();
//            txtItems = new Guna2TextBox();
//            lblItems = new Guna2HtmlLabel();
//            txtTotal = new Guna2TextBox();
//            lblTotal = new Guna2HtmlLabel();
//            btnSave = new Guna2Button();
//            btnDelete = new Guna2Button();
//            btnCancel = new Guna2Button();
//            panelTop = new Guna2Panel();
//            txtSearch = new Guna2TextBox();
//            btnSearch = new Guna2Button();
//            btnAdd = new Guna2Button();
//            label6 = new Guna2HtmlLabel();
//            flowOrders = new FlowLayoutPanel();

//            panelSlide.SuspendLayout();
//            panelTop.SuspendLayout();
//            SuspendLayout();

//            // 
//            // panelSlide (dark, rounded, shadow)
//            // 
//            panelSlide.BackColor = Color.FromArgb(30, 30, 40);
//            panelSlide.BorderRadius = 12;
//            panelSlide.FillColor = Color.FromArgb(45, 44, 90);
//            panelSlide.ShadowDecoration.Depth = 20;
//            panelSlide.ShadowDecoration.Enabled = true;
//            panelSlide.Controls.Add(lblSlideTitle);
//            panelSlide.Controls.Add(txtCustomer);
//            panelSlide.Controls.Add(lblCustomer);
//            panelSlide.Controls.Add(txtItems);
//            panelSlide.Controls.Add(lblItems);
//            panelSlide.Controls.Add(txtTotal);
//            panelSlide.Controls.Add(lblTotal);
//            panelSlide.Controls.Add(btnSave);
//            panelSlide.Controls.Add(btnDelete);
//            panelSlide.Controls.Add(btnCancel);
//            panelSlide.Location = new Point(0, 60);
//            panelSlide.Name = "panelSlide";
//            panelSlide.Size = new Size(0, 580);
//            panelSlide.TabIndex = 0;

//            // lblSlideTitle
//            lblSlideTitle.BackColor = Color.Transparent;
//            lblSlideTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
//            lblSlideTitle.ForeColor = Color.White;
//            lblSlideTitle.Location = new Point(20, 20);
//            lblSlideTitle.Name = "lblSlideTitle";
//            lblSlideTitle.Size = new Size(98, 28);
//            lblSlideTitle.Text = "Add Order";

//            // txtCustomer
//            txtCustomer.BorderRadius = 8;
//            txtCustomer.FillColor = Color.FromArgb(60, 60, 80);
//            txtCustomer.ForeColor = Color.White;
//            txtCustomer.Location = new Point(20, 80);
//            txtCustomer.Name = "txtCustomer";
//            txtCustomer.Size = new Size(250, 36);
//            txtCustomer.TabIndex = 1;

//            // lblCustomer
//            lblCustomer.BackColor = Color.Transparent;
//            lblCustomer.Font = new Font("Segoe UI", 9);
//            lblCustomer.ForeColor = Color.White;
//            lblCustomer.Location = new Point(20, 57);
//            lblCustomer.Name = "lblCustomer";
//            lblCustomer.Size = new Size(122, 17);
//            lblCustomer.Text = "Customer Name";

//            // txtItems
//            txtItems.BorderRadius = 8;
//            txtItems.FillColor = Color.FromArgb(60, 60, 80);
//            txtItems.ForeColor = Color.White;
//            txtItems.Location = new Point(20, 140);
//            txtItems.Name = "txtItems";
//            txtItems.Size = new Size(250, 36);
//            txtItems.TabIndex = 3;

//            // lblItems
//            lblItems.BackColor = Color.Transparent;
//            lblItems.Font = new Font("Segoe UI", 9);
//            lblItems.ForeColor = Color.White;
//            lblItems.Location = new Point(20, 117);
//            lblItems.Name = "lblItems";
//            lblItems.Size = new Size(45, 17);
//            lblItems.Text = "Items";

//            // txtTotal
//            txtTotal.BorderRadius = 8;
//            txtTotal.FillColor = Color.FromArgb(60, 60, 80);
//            txtTotal.ForeColor = Color.White;
//            txtTotal.Location = new Point(20, 200);
//            txtTotal.Name = "txtTotal";
//            txtTotal.Size = new Size(250, 36);
//            txtTotal.TabIndex = 5;

//            // lblTotal
//            lblTotal.BackColor = Color.Transparent;
//            lblTotal.Font = new Font("Segoe UI", 9);
//            lblTotal.ForeColor = Color.White;
//            lblTotal.Location = new Point(20, 177);
//            lblTotal.Name = "lblTotal";
//            lblTotal.Size = new Size(99, 17);
//            lblTotal.Text = "Total Amount";

//            // btnSave (green accent)
//            btnSave.BorderRadius = 8;
//            btnSave.FillColor = Color.FromArgb(76, 175, 80);
//            btnSave.ForeColor = Color.White;
//            btnSave.Location = new Point(20, 260);
//            btnSave.Name = "btnSave";
//            btnSave.Size = new Size(110, 36);
//            btnSave.Text = "Save";
//            btnSave.Click += btnSave_Click;

//            // btnDelete
//            btnDelete.BorderRadius = 8;
//            btnDelete.FillColor = Color.FromArgb(244, 67, 54);
//            btnDelete.ForeColor = Color.White;
//            btnDelete.Location = new Point(20, 300);
//            btnDelete.Name = "btnDelete";
//            btnDelete.Size = new Size(110, 36);
//            btnDelete.Text = "Delete";
//            btnDelete.Click += btnDelete_Click;

//            // btnCancel
//            btnCancel.BorderRadius = 8;
//            btnCancel.FillColor = Color.Gray;
//            btnCancel.ForeColor = Color.White;
//            btnCancel.Location = new Point(160, 260);
//            btnCancel.Name = "btnCancel";
//            btnCancel.Size = new Size(110, 36);
//            btnCancel.Text = "Cancel";
//            btnCancel.Click += btnCancel_Click;

//            // panelTop
//            panelTop.Dock = DockStyle.Top;
//            panelTop.FillColor = Color.FromArgb(26, 25, 62);
//            panelTop.Location = new Point(0, 0);
//            panelTop.Name = "panelTop";
//            panelTop.Size = new Size(949, 60);
//            panelTop.Controls.Add(txtSearch);
//            panelTop.Controls.Add(btnSearch);
//            panelTop.Controls.Add(btnAdd);
//            panelTop.Controls.Add(label6);

//            // txtSearch
//            txtSearch.BorderRadius = 8;
//            txtSearch.FillColor = Color.FromArgb(50, 50, 70);
//            txtSearch.ForeColor = Color.White;
//            txtSearch.Location = new Point(140, 18);
//            txtSearch.Name = "txtSearch";
//            txtSearch.Size = new Size(200, 36);
//            txtSearch.TabIndex = 0;

//            // btnSearch
//            btnSearch.BorderRadius = 8;
//            btnSearch.FillColor = Color.FromArgb(26, 25, 62);
//            btnSearch.ForeColor = Color.White;
//            btnSearch.Location = new Point(350, 17);
//            btnSearch.Size = new Size(80, 36);
//            btnSearch.Text = "Search";
//            btnSearch.Click += btnSearch_Click;

//            // btnAdd
//            btnAdd.BorderRadius = 8;
//            btnAdd.FillColor = Color.FromArgb(76, 175, 80);
//            btnAdd.ForeColor = Color.White;
//            btnAdd.Location = new Point(800, 15);
//            btnAdd.Name = "btnAdd";
//            btnAdd.Size = new Size(120, 36);
//            btnAdd.Text = "Add Order";
//            btnAdd.Click += btnAdd_Click;

//            // label6
//            label6.BackColor = Color.Transparent;
//            label6.Font = new Font("Segoe UI", 9);
//            label6.ForeColor = Color.White;
//            label6.Location = new Point(20, 21);
//            label6.Name = "label6";
//            label6.Size = new Size(114, 17);
//            label6.Text = "Search Orders";

//            // flowOrders (cards area)
//            flowOrders.BackColor = Color.FromArgb(26, 25, 62);
//            flowOrders.Dock = DockStyle.Fill;
//            flowOrders.AutoScroll = true;
//            flowOrders.FlowDirection = FlowDirection.TopDown;
//            flowOrders.Padding = new Padding(10);
//            flowOrders.Location = new Point(0, 60);
//            flowOrders.Size = new Size(949, 580);

//            // OrdersControl
//            BackColor = Color.FromArgb(26, 25, 62);
//            Controls.Add(flowOrders);
//            Controls.Add(panelTop);
//            Controls.Add(panelSlide);        // slide panel on top
//            Name = "OrdersControl";
//            Size = new Size(949, 640);
//            panelSlide.ResumeLayout(false);
//            panelTop.ResumeLayout(false);
//            ResumeLayout(false);
//        }
//    }
//}