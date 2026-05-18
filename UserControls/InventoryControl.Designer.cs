using Guna.UI2.WinForms;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class InventoryControl
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
            // Stat cards
            panelTotalValue = new Guna2Panel();
            lblTotalValue = new Guna2HtmlLabel();
            lblTotalValueTitle = new Guna2HtmlLabel();
            panelActiveMaterials = new Guna2Panel();
            lblActiveMaterials = new Guna2HtmlLabel();
            lblActiveMaterialsTitle = new Guna2HtmlLabel();
            panelLowStock = new Guna2Panel();
            lblLowStock = new Guna2HtmlLabel();
            lblLowStockTitle = new Guna2HtmlLabel();

            // Top panel
            panelTop = new Guna2Panel();
            txtSearch = new Guna2TextBox();
            btnSearch = new Guna2Button();
            btnAdd = new Guna2Button();
            lblTopTitle = new Guna2HtmlLabel();

            // Slide panel (add/edit)
            panelSlide = new Guna2Panel();
            lblSlideTitle = new Guna2HtmlLabel();
            lblMaterialName = new Guna2HtmlLabel();
            txtMaterialName = new Guna2TextBox();
            lblQuantity = new Guna2HtmlLabel();
            numQuantity = new Guna2NumericUpDown();
            lblUnit = new Guna2HtmlLabel();
            cmbUnit = new Guna2ComboBox();
            lblUnitPrice = new Guna2HtmlLabel();
            txtUnitPrice = new Guna2TextBox();
            lblReorderLevel = new Guna2HtmlLabel();
            numReorderLevel = new Guna2NumericUpDown();
            lblTotalValueSlide = new Guna2HtmlLabel();
            lblTotalValueNum = new Guna2HtmlLabel();
            lblStatusSlide = new Guna2HtmlLabel();
            lblStatusValue = new Guna2HtmlLabel();
            btnSave = new Guna2Button();
            btnDelete = new Guna2Button();
            btnCancelSlide = new Guna2Button();

            // Card list
            flowMaterials = new FlowLayoutPanel();

            panelTotalValue.SuspendLayout();
            panelActiveMaterials.SuspendLayout();
            panelLowStock.SuspendLayout();
            panelTop.SuspendLayout();
            panelSlide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReorderLevel).BeginInit();
            SuspendLayout();

            // =============== STAT CARDS ===============
            // Total Value
            panelTotalValue.BackColor = Color.Transparent;
            panelTotalValue.BorderRadius = 10;
            panelTotalValue.FillColor = Color.FromArgb(45, 44, 90);
            panelTotalValue.Size = new Size(200, 100);
            panelTotalValue.Location = new Point(20, 20);
            panelTotalValue.ShadowDecoration.Enabled = true;
            panelTotalValue.ShadowDecoration.Depth = 10;
            lblTotalValueTitle.Text = "Total Value";
            lblTotalValueTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotalValueTitle.ForeColor = Color.White;
            lblTotalValueTitle.Location = new Point(15, 15);
            lblTotalValue.ForeColor = Color.White;
            lblTotalValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalValue.Location = new Point(15, 50);
            lblTotalValue.Text = "₱0.00";
            panelTotalValue.Controls.Add(lblTotalValueTitle);
            panelTotalValue.Controls.Add(lblTotalValue);

            // Active Materials
            panelActiveMaterials.BackColor = Color.Transparent;
            panelActiveMaterials.BorderRadius = 10;
            panelActiveMaterials.FillColor = Color.FromArgb(45, 44, 90);
            panelActiveMaterials.Size = new Size(200, 100);
            panelActiveMaterials.Location = new Point(240, 20);
            panelActiveMaterials.ShadowDecoration.Enabled = true;
            panelActiveMaterials.ShadowDecoration.Depth = 10;
            lblActiveMaterialsTitle.Text = "Active Materials";
            lblActiveMaterialsTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblActiveMaterialsTitle.ForeColor = Color.White;
            lblActiveMaterialsTitle.Location = new Point(15, 15);
            lblActiveMaterials.ForeColor = Color.White;
            lblActiveMaterials.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblActiveMaterials.Location = new Point(15, 50);
            lblActiveMaterials.Text = "0";
            panelActiveMaterials.Controls.Add(lblActiveMaterialsTitle);
            panelActiveMaterials.Controls.Add(lblActiveMaterials);

            // Low Stock
            panelLowStock.BackColor = Color.Transparent;
            panelLowStock.BorderRadius = 10;
            panelLowStock.FillColor = Color.FromArgb(45, 44, 90);
            panelLowStock.Size = new Size(200, 100);
            panelLowStock.Location = new Point(460, 20);
            panelLowStock.ShadowDecoration.Enabled = true;
            panelLowStock.ShadowDecoration.Depth = 10;
            lblLowStockTitle.Text = "Low Stock Items";
            lblLowStockTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblLowStockTitle.ForeColor = Color.White;
            lblLowStockTitle.Location = new Point(15, 15);
            lblLowStock.ForeColor = Color.White;
            lblLowStock.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblLowStock.Location = new Point(15, 50);
            lblLowStock.Text = "0";
            panelLowStock.Controls.Add(lblLowStockTitle);
            panelLowStock.Controls.Add(lblLowStock);

            // =============== TOP PANEL ===============
            panelTop.Dock = DockStyle.Top;
            panelTop.FillColor = Color.FromArgb(26, 25, 62);
            panelTop.Height = 60;
            panelTop.Location = new Point(0, 130);
            txtSearch.BorderRadius = 8;
            txtSearch.FillColor = Color.FromArgb(50, 50, 70);
            txtSearch.ForeColor = Color.White;
            txtSearch.Location = new Point(140, 18);
            txtSearch.Size = new Size(200, 36);
            btnSearch.BorderRadius = 8;
            btnSearch.FillColor = Color.FromArgb(26, 25, 62);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(350, 17);
            btnSearch.Size = new Size(80, 36);
            btnSearch.Text = "Search";
            btnSearch.Click += btnSearch_Click;
            btnAdd.BorderRadius = 8;
            btnAdd.FillColor = Color.FromArgb(76, 175, 80);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(800, 15);
            btnAdd.Size = new Size(120, 36);
            btnAdd.Text = "Add Item";
            btnAdd.Click += btnAdd_Click;
            lblTopTitle.BackColor = Color.Transparent;
            lblTopTitle.ForeColor = Color.White;
            lblTopTitle.Font = new Font("Segoe UI", 9);
            lblTopTitle.Location = new Point(20, 21);
            lblTopTitle.Text = "Search Materials";
            panelTop.Controls.Add(lblTopTitle);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(btnAdd);

            // =============== SLIDE PANEL (Add/Edit) ===============
            panelSlide.BackColor = Color.FromArgb(30, 30, 40);
            panelSlide.BorderRadius = 15;
            panelSlide.FillColor = Color.FromArgb(45, 44, 90);
            panelSlide.ShadowDecoration.Enabled = true;
            panelSlide.ShadowDecoration.Depth = 20;
            panelSlide.Location = new Point(0, 190);
            panelSlide.Name = "panelSlide";
            panelSlide.Size = new Size(0, 450);
            panelSlide.TabIndex = 10;

            // Slide Title
            lblSlideTitle.BackColor = Color.Transparent;
            lblSlideTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSlideTitle.ForeColor = Color.White;
            lblSlideTitle.Location = new Point(20, 20);
            lblSlideTitle.Text = "Add Material";

            // Material Name
            lblMaterialName.BackColor = Color.Transparent;
            lblMaterialName.ForeColor = Color.White;
            lblMaterialName.Location = new Point(20, 60);
            lblMaterialName.Text = "Material Name";
            txtMaterialName.BorderRadius = 8;
            txtMaterialName.FillColor = Color.FromArgb(60, 60, 80);
            txtMaterialName.ForeColor = Color.White;
            txtMaterialName.Location = new Point(20, 80);
            txtMaterialName.Size = new Size(260, 36);

            // Quantity
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.ForeColor = Color.White;
            lblQuantity.Location = new Point(20, 130);
            lblQuantity.Text = "Quantity";
            numQuantity.BorderRadius = 8;
            numQuantity.FillColor = Color.FromArgb(60, 60, 80);
            numQuantity.ForeColor = Color.White;
            numQuantity.Location = new Point(20, 150);
            numQuantity.Size = new Size(120, 36);
            numQuantity.Minimum = 0;
            numQuantity.Maximum = 10000;
            numQuantity.ValueChanged += CalculateSlideTotal;

            // Unit
            lblUnit.BackColor = Color.Transparent;
            lblUnit.ForeColor = Color.White;
            lblUnit.Location = new Point(160, 130);
            lblUnit.Text = "Unit";
            cmbUnit.BorderRadius = 8;
            cmbUnit.FillColor = Color.FromArgb(60, 60, 80);
            cmbUnit.ForeColor = Color.White;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.Items.AddRange(new[] { "meters", "yards", "pcs", "spools", "rolls" });
            cmbUnit.Location = new Point(160, 150);
            cmbUnit.Size = new Size(120, 36);

            // Unit Price
            lblUnitPrice.BackColor = Color.Transparent;
            lblUnitPrice.ForeColor = Color.White;
            lblUnitPrice.Location = new Point(20, 200);
            lblUnitPrice.Text = "Unit Price (₱)";
            txtUnitPrice.BorderRadius = 8;
            txtUnitPrice.FillColor = Color.FromArgb(60, 60, 80);
            txtUnitPrice.ForeColor = Color.White;
            txtUnitPrice.Location = new Point(20, 220);
            txtUnitPrice.Size = new Size(120, 36);
            txtUnitPrice.Text = "0.00";
            txtUnitPrice.TextChanged += CalculateSlideTotal;

            // Reorder Level
            lblReorderLevel.BackColor = Color.Transparent;
            lblReorderLevel.ForeColor = Color.White;
            lblReorderLevel.Location = new Point(160, 200);
            lblReorderLevel.Text = "Reorder Level";
            numReorderLevel.BorderRadius = 8;
            numReorderLevel.FillColor = Color.FromArgb(60, 60, 80);
            numReorderLevel.ForeColor = Color.White;
            numReorderLevel.Location = new Point(160, 220);
            numReorderLevel.Size = new Size(120, 36);
            numReorderLevel.Minimum = 0;
            numReorderLevel.Maximum = 1000;

            // Total Value (computed)
            lblTotalValueSlide.BackColor = Color.Transparent;
            lblTotalValueSlide.ForeColor = Color.White;
            lblTotalValueSlide.Location = new Point(20, 270);
            lblTotalValueSlide.Text = "Total Value (₱)";
            lblTotalValueNum.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalValueNum.ForeColor = Color.Gold;
            lblTotalValueNum.Location = new Point(20, 290);
            lblTotalValueNum.Text = "₱0.00";

            // Status (read-only)
            lblStatusSlide.BackColor = Color.Transparent;
            lblStatusSlide.ForeColor = Color.White;
            lblStatusSlide.Location = new Point(160, 270);
            lblStatusSlide.Text = "Status";
            lblStatusValue.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblStatusValue.ForeColor = Color.White;
            lblStatusValue.Location = new Point(160, 290);
            lblStatusValue.Text = "In Stock";

            // Buttons
            btnSave.BorderRadius = 8;
            btnSave.FillColor = Color.FromArgb(76, 175, 80);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 340);
            btnSave.Size = new Size(110, 36);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;

            btnDelete.BorderRadius = 8;
            btnDelete.FillColor = Color.FromArgb(244, 67, 54);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(140, 340);
            btnDelete.Size = new Size(110, 36);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;

            btnCancelSlide.BorderRadius = 8;
            btnCancelSlide.FillColor = Color.Gray;
            btnCancelSlide.ForeColor = Color.White;
            btnCancelSlide.Location = new Point(260, 340);
            btnCancelSlide.Size = new Size(110, 36);
            btnCancelSlide.Text = "Cancel";
            btnCancelSlide.Click += btnCancelSlide_Click;

            panelSlide.Controls.Add(lblSlideTitle);
            panelSlide.Controls.Add(lblMaterialName);
            panelSlide.Controls.Add(txtMaterialName);
            panelSlide.Controls.Add(lblQuantity);
            panelSlide.Controls.Add(numQuantity);
            panelSlide.Controls.Add(lblUnit);
            panelSlide.Controls.Add(cmbUnit);
            panelSlide.Controls.Add(lblUnitPrice);
            panelSlide.Controls.Add(txtUnitPrice);
            panelSlide.Controls.Add(lblReorderLevel);
            panelSlide.Controls.Add(numReorderLevel);
            panelSlide.Controls.Add(lblTotalValueSlide);
            panelSlide.Controls.Add(lblTotalValueNum);
            panelSlide.Controls.Add(lblStatusSlide);
            panelSlide.Controls.Add(lblStatusValue);
            panelSlide.Controls.Add(btnSave);
            panelSlide.Controls.Add(btnDelete);
            panelSlide.Controls.Add(btnCancelSlide);

            // =============== CARD LIST ===============
            flowMaterials.BackColor = Color.FromArgb(26, 25, 62);
            flowMaterials.Dock = DockStyle.Fill;
            flowMaterials.AutoScroll = true;
            flowMaterials.FlowDirection = FlowDirection.TopDown;
            flowMaterials.Padding = new Padding(10);
            flowMaterials.Location = new Point(0, 190);
            flowMaterials.Size = new Size(949, 450);

            // =============== PARENT CONTROL ===============
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(flowMaterials);
            Controls.Add(panelTop);
            Controls.Add(panelTotalValue);
            Controls.Add(panelActiveMaterials);
            Controls.Add(panelLowStock);
            Controls.Add(panelSlide);
            Name = "InventoryControl";
            Size = new Size(949, 640);

            panelTotalValue.ResumeLayout(false);
            panelActiveMaterials.ResumeLayout(false);
            panelLowStock.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelSlide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReorderLevel).EndInit();
            ResumeLayout(false);
        }

        // Declarations
        private Guna2Panel panelTotalValue;
        private Guna2HtmlLabel lblTotalValue;
        private Guna2HtmlLabel lblTotalValueTitle;
        private Guna2Panel panelActiveMaterials;
        private Guna2HtmlLabel lblActiveMaterials;
        private Guna2HtmlLabel lblActiveMaterialsTitle;
        private Guna2Panel panelLowStock;
        private Guna2HtmlLabel lblLowStock;
        private Guna2HtmlLabel lblLowStockTitle;

        private Guna2Panel panelTop;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnAdd;
        private Guna2HtmlLabel lblTopTitle;

        private Guna2Panel panelSlide;
        private Guna2HtmlLabel lblSlideTitle;
        private Guna2HtmlLabel lblMaterialName;
        private Guna2TextBox txtMaterialName;
        private Guna2HtmlLabel lblQuantity;
        private Guna2NumericUpDown numQuantity;
        private Guna2HtmlLabel lblUnit;
        private Guna2ComboBox cmbUnit;
        private Guna2HtmlLabel lblUnitPrice;
        private Guna2TextBox txtUnitPrice;
        private Guna2HtmlLabel lblReorderLevel;
        private Guna2NumericUpDown numReorderLevel;
        private Guna2HtmlLabel lblTotalValueSlide;
        private Guna2HtmlLabel lblTotalValueNum;
        private Guna2HtmlLabel lblStatusSlide;
        private Guna2HtmlLabel lblStatusValue;
        private Guna2Button btnSave;
        private Guna2Button btnDelete;
        private Guna2Button btnCancelSlide;

        private FlowLayoutPanel flowMaterials;
    }
}


//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    partial class InventoryControl
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            panelTotalValue = new RoundedPanel();
//            lblTotalValue = new Label();
//            label1 = new Label();
//            panelActiveMaterials = new RoundedPanel();
//            lblActiveMaterials = new Label();
//            label2 = new Label();
//            panelMonthlyConsumption = new RoundedPanel();
//            lblMonthlyConsumption = new Label();
//            label3 = new Label();
//            dgvMaterials = new DataGridView();
//            roundedPanel1 = new RoundedPanel();
//            panelTotalValue.SuspendLayout();
//            panelActiveMaterials.SuspendLayout();
//            panelMonthlyConsumption.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)dgvMaterials).BeginInit();
//            SuspendLayout();
//            // 
//            // panelTotalValue
//            // 
//            panelTotalValue.BackColor = Color.FromArgb(90, 88, 140);
//            panelTotalValue.Controls.Add(lblTotalValue);
//            panelTotalValue.Controls.Add(label1);
//            panelTotalValue.Location = new Point(72, 23);
//            panelTotalValue.Name = "panelTotalValue";
//            panelTotalValue.Size = new Size(227, 99);
//            panelTotalValue.TabIndex = 0;
//            // 
//            // lblTotalValue
//            // 
//            lblTotalValue.AutoSize = true;
//            lblTotalValue.ForeColor = Color.White;
//            lblTotalValue.Location = new Point(24, 55);
//            lblTotalValue.Name = "lblTotalValue";
//            lblTotalValue.Size = new Size(0, 20);
//            lblTotalValue.TabIndex = 1;
//            // 
//            // label1
//            // 
//            label1.AutoSize = true;
//            label1.BackColor = Color.FromArgb(26, 25, 62);
//            label1.ForeColor = Color.White;
//            label1.Location = new Point(21, 19);
//            label1.Name = "label1";
//            label1.Size = new Size(82, 20);
//            label1.TabIndex = 0;
//            label1.Text = "Total Value";
//            // 
//            // panelActiveMaterials
//            // 
//            panelActiveMaterials.BackColor = Color.FromArgb(90, 88, 140);
//            panelActiveMaterials.Controls.Add(lblActiveMaterials);
//            panelActiveMaterials.Controls.Add(label2);
//            panelActiveMaterials.Location = new Point(332, 23);
//            panelActiveMaterials.Name = "panelActiveMaterials";
//            panelActiveMaterials.Size = new Size(274, 99);
//            panelActiveMaterials.TabIndex = 1;
//            // 
//            // lblActiveMaterials
//            // 
//            lblActiveMaterials.AutoSize = true;
//            lblActiveMaterials.ForeColor = Color.White;
//            lblActiveMaterials.Location = new Point(23, 55);
//            lblActiveMaterials.Name = "lblActiveMaterials";
//            lblActiveMaterials.Size = new Size(0, 20);
//            lblActiveMaterials.TabIndex = 2;
//            // 
//            // label2
//            // 
//            label2.AutoSize = true;
//            label2.BackColor = Color.FromArgb(26, 25, 62);
//            label2.ForeColor = Color.White;
//            label2.Location = new Point(23, 19);
//            label2.Name = "label2";
//            label2.Size = new Size(115, 20);
//            label2.TabIndex = 1;
//            label2.Text = "Active Materials";
//            // 
//            // panelMonthlyConsumption
//            // 
//            panelMonthlyConsumption.BackColor = Color.FromArgb(90, 88, 140);
//            panelMonthlyConsumption.Controls.Add(lblMonthlyConsumption);
//            panelMonthlyConsumption.Controls.Add(label3);
//            panelMonthlyConsumption.Location = new Point(645, 23);
//            panelMonthlyConsumption.Name = "panelMonthlyConsumption";
//            panelMonthlyConsumption.Size = new Size(242, 99);
//            panelMonthlyConsumption.TabIndex = 2;
//            // 
//            // lblMonthlyConsumption
//            // 
//            lblMonthlyConsumption.AutoSize = true;
//            lblMonthlyConsumption.ForeColor = Color.White;
//            lblMonthlyConsumption.Location = new Point(23, 55);
//            lblMonthlyConsumption.Name = "lblMonthlyConsumption";
//            lblMonthlyConsumption.Size = new Size(0, 20);
//            lblMonthlyConsumption.TabIndex = 3;
//            // 
//            // label3
//            // 
//            label3.AutoSize = true;
//            label3.BackColor = Color.FromArgb(26, 25, 62);
//            label3.ForeColor = Color.White;
//            label3.Location = new Point(23, 19);
//            label3.Name = "label3";
//            label3.Size = new Size(155, 20);
//            label3.TabIndex = 2;
//            label3.Text = "Monthly Consumption";
//            // 
//            // dgvMaterials
//            // 
//            dgvMaterials.AllowUserToAddRows = false;
//            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            dgvMaterials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            dgvMaterials.Location = new Point(94, 179);
//            dgvMaterials.Name = "dgvMaterials";
//            dgvMaterials.ReadOnly = true;
//            dgvMaterials.RowHeadersVisible = false;
//            dgvMaterials.RowHeadersWidth = 51;
//            dgvMaterials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            dgvMaterials.Size = new Size(768, 417);
//            dgvMaterials.TabIndex = 3;
//            // 
//            // roundedPanel1
//            // 
//            roundedPanel1.BackColor = Color.FromArgb(90, 88, 140);
//            roundedPanel1.Location = new Point(72, 158);
//            roundedPanel1.Name = "roundedPanel1";
//            roundedPanel1.Size = new Size(815, 463);
//            roundedPanel1.TabIndex = 4;
//            // 
//            // InventoryControl
//            // 
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(26, 25, 62);
//            Controls.Add(dgvMaterials);
//            Controls.Add(panelMonthlyConsumption);
//            Controls.Add(panelActiveMaterials);
//            Controls.Add(panelTotalValue);
//            Controls.Add(roundedPanel1);
//            Name = "InventoryControl";
//            Size = new Size(949, 640);
//            panelTotalValue.ResumeLayout(false);
//            panelTotalValue.PerformLayout();
//            panelActiveMaterials.ResumeLayout(false);
//            panelActiveMaterials.PerformLayout();
//            panelMonthlyConsumption.ResumeLayout(false);
//            panelMonthlyConsumption.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)dgvMaterials).EndInit();
//            ResumeLayout(false);
//        }

//        #endregion

//        private RoundedPanel panelTotalValue;
//        private RoundedPanel panelActiveMaterials;
//        private RoundedPanel panelMonthlyConsumption;
//        private Label label1;
//        private Label label2;
//        private Label label3;
//        private Label lblTotalValue;
//        private Label lblActiveMaterials;
//        private Label lblMonthlyConsumption;
//        private DataGridView dgvMaterials;
//        private RoundedPanel roundedPanel1;
//    }
//}
