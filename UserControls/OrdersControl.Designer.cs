using FLAVSMAGS_TAILORING;
namespace FLAVSMAGS_TAILORING.UserControls



{
    partial class OrdersControl
    {
        private System.ComponentModel.IContainer components = null;
        private RoundedPanel panelCreateOrder;  // Changed from Panel to RoundedPanel
        private Label label2;
        private TextBox txtCustomer;
        private Label label3;
        private TextBox txtItems;
        private Label label4;
        private FontAwesome.Sharp.IconButton btnCreateOrder;
        private TextBox txtTotal;
        private Label label5;
        private FontAwesome.Sharp.IconButton btnClearOrder;
        private RoundedPanel panelSearch;  // Changed from Panel to RoundedPanel
        private Label label6;
        private Label label7;
        private TextBox txtSearch;
        private FontAwesome.Sharp.IconButton btnSearch;
        private DataGridView dgvActiveOrders;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelCreateOrder = new RoundedPanel();
            btnClearOrder = new FontAwesome.Sharp.IconButton();
            btnCreateOrder = new FontAwesome.Sharp.IconButton();
            txtTotal = new TextBox();
            label5 = new Label();
            txtItems = new TextBox();
            label4 = new Label();
            txtCustomer = new TextBox();
            label3 = new Label();
            label2 = new Label();
            panelSearch = new RoundedPanel();
            btnSearch = new FontAwesome.Sharp.IconButton();
            txtSearch = new TextBox();
            label7 = new Label();
            label6 = new Label();
            dgvActiveOrders = new DataGridView();
            roundedPanel1 = new RoundedPanel();
            panelCreateOrder.SuspendLayout();
            panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveOrders).BeginInit();
            SuspendLayout();
            // 
            // panelCreateOrder
            // 
            panelCreateOrder.BackColor = Color.FromArgb(90, 88, 140);
            panelCreateOrder.Controls.Add(btnClearOrder);
            panelCreateOrder.Controls.Add(btnCreateOrder);
            panelCreateOrder.Controls.Add(txtTotal);
            panelCreateOrder.Controls.Add(label5);
            panelCreateOrder.Controls.Add(txtItems);
            panelCreateOrder.Controls.Add(label4);
            panelCreateOrder.Controls.Add(txtCustomer);
            panelCreateOrder.Controls.Add(label3);
            panelCreateOrder.Controls.Add(label2);
            panelCreateOrder.Location = new Point(16, 95);
            panelCreateOrder.Name = "panelCreateOrder";
            panelCreateOrder.Size = new Size(907, 158);
            panelCreateOrder.TabIndex = 1;
            // 
            // btnClearOrder
            // 
            btnClearOrder.BackColor = Color.FromArgb(26, 25, 62);
            btnClearOrder.ForeColor = Color.White;
            btnClearOrder.IconChar = FontAwesome.Sharp.IconChar.None;
            btnClearOrder.IconColor = Color.Black;
            btnClearOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClearOrder.Location = new Point(648, 89);
            btnClearOrder.Name = "btnClearOrder";
            btnClearOrder.Size = new Size(118, 27);
            btnClearOrder.TabIndex = 8;
            btnClearOrder.Text = "Clear Form";
            btnClearOrder.UseVisualStyleBackColor = false;
            btnClearOrder.Click += btnClearOrder_Click;
            // 
            // btnCreateOrder
            // 
            btnCreateOrder.BackColor = Color.FromArgb(26, 25, 62);
            btnCreateOrder.ForeColor = Color.White;
            btnCreateOrder.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCreateOrder.IconColor = Color.Black;
            btnCreateOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCreateOrder.Location = new Point(772, 89);
            btnCreateOrder.Name = "btnCreateOrder";
            btnCreateOrder.Size = new Size(119, 27);
            btnCreateOrder.TabIndex = 7;
            btnCreateOrder.Text = "Create Order";
            btnCreateOrder.UseVisualStyleBackColor = false;
            btnCreateOrder.Click += btnCreateOrder_Click;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(440, 89);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(192, 27);
            txtTotal.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(90, 88, 140);
            label5.ForeColor = Color.White;
            label5.Location = new Point(440, 56);
            label5.Name = "label5";
            label5.Size = new Size(99, 20);
            label5.TabIndex = 5;
            label5.Text = "Total Amount";
            label5.Click += label5_Click;
            // 
            // txtItems
            // 
            txtItems.Location = new Point(232, 89);
            txtItems.Name = "txtItems";
            txtItems.Size = new Size(192, 27);
            txtItems.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(90, 88, 140);
            label4.ForeColor = Color.White;
            label4.Location = new Point(232, 56);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 3;
            label4.Text = "Items";
            label4.Click += label4_Click;
            // 
            // txtCustomer
            // 
            txtCustomer.Location = new Point(23, 89);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new Size(192, 27);
            txtCustomer.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(90, 88, 140);
            label3.ForeColor = Color.White;
            label3.Location = new Point(23, 56);
            label3.Name = "label3";
            label3.Size = new Size(129, 20);
            label3.TabIndex = 1;
            label3.Text = "Customer's Name ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(90, 88, 140);
            label2.ForeColor = Color.White;
            label2.Location = new Point(23, 16);
            label2.Name = "label2";
            label2.Size = new Size(128, 20);
            label2.TabIndex = 0;
            label2.Text = "Create New Order";
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.FromArgb(90, 88, 140);
            panelSearch.Controls.Add(btnSearch);
            panelSearch.Controls.Add(txtSearch);
            panelSearch.Controls.Add(label7);
            panelSearch.Location = new Point(373, 32);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(550, 46);
            panelSearch.TabIndex = 9;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(26, 25, 62);
            btnSearch.ForeColor = Color.White;
            btnSearch.IconChar = FontAwesome.Sharp.IconChar.None;
            btnSearch.IconColor = Color.Black;
            btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSearch.Location = new Point(425, 9);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(109, 27);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(167, 9);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(234, 27);
            txtSearch.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(90, 88, 140);
            label7.ForeColor = Color.White;
            label7.Location = new Point(11, 12);
            label7.Name = "label7";
            label7.Size = new Size(150, 20);
            label7.TabIndex = 9;
            label7.Text = "Order ID/ Customer : ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(26, 25, 62);
            label6.ForeColor = Color.White;
            label6.Location = new Point(384, 9);
            label6.Name = "label6";
            label6.Size = new Size(101, 20);
            label6.TabIndex = 10;
            label6.Text = "Search Orders";
            // 
            // dgvActiveOrders
            // 
            dgvActiveOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActiveOrders.Location = new Point(63, 305);
            dgvActiveOrders.Name = "dgvActiveOrders";
            dgvActiveOrders.RowHeadersWidth = 51;
            dgvActiveOrders.Size = new Size(820, 292);
            dgvActiveOrders.TabIndex = 11;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(90, 88, 140);
            roundedPanel1.Location = new Point(16, 288);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(907, 327);
            roundedPanel1.TabIndex = 12;
            // 
            // OrdersControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(dgvActiveOrders);
            Controls.Add(label6);
            Controls.Add(panelSearch);
            Controls.Add(panelCreateOrder);
            Controls.Add(roundedPanel1);
            Name = "OrdersControl";
            Size = new Size(949, 640);
            panelCreateOrder.ResumeLayout(false);
            panelCreateOrder.PerformLayout();
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private RoundedPanel roundedPanel1;
    }
}