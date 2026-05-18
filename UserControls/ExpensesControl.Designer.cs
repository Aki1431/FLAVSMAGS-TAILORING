using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class ExpensesControl
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
            cardTotalExpenses = new Guna2Panel();
            lblTotalExpenses = new Guna2HtmlLabel();
            lblTotalTitle = new Guna2HtmlLabel();
            cardCount = new Guna2Panel();
            lblExpenseCount = new Guna2HtmlLabel();
            lblCountTitle = new Guna2HtmlLabel();
            cardTrend = new Guna2Panel();
            lblTrend = new Guna2HtmlLabel();
            lblTrendTitle = new Guna2HtmlLabel();

            // Top bar
            panelTop = new Guna2Panel();
            txtSearch = new Guna2TextBox();
            btnSearch = new Guna2Button();
            btnAdd = new Guna2Button();
            lblTopTitle = new Guna2HtmlLabel();

            // Slide panel (add/edit)
            panelSlide = new Guna2Panel();
            lblSlideTitle = new Guna2HtmlLabel();
            lblExpenseName = new Guna2HtmlLabel();
            txtExpenseName = new Guna2TextBox();
            lblAmount = new Guna2HtmlLabel();
            txtAmount = new Guna2TextBox();
            lblCategory = new Guna2HtmlLabel();
            cmbCategory = new Guna2ComboBox();
            lblDate = new Guna2HtmlLabel();
            dtpDate = new Guna2DateTimePicker();
            lblNotes = new Guna2HtmlLabel();
            txtNotes = new Guna2TextBox();
            btnSave = new Guna2Button();
            btnDelete = new Guna2Button();
            btnCancel = new Guna2Button();

            // Card list
            flowExpenses = new FlowLayoutPanel();

            cardTotalExpenses.SuspendLayout();
            cardCount.SuspendLayout();
            cardTrend.SuspendLayout();
            panelTop.SuspendLayout();
            panelSlide.SuspendLayout();
            SuspendLayout();

            // =============== STAT CARDS ===============
            // Total Expenses
            cardTotalExpenses.BackColor = Color.Transparent;
            cardTotalExpenses.BorderRadius = 10;
            cardTotalExpenses.FillColor = Color.FromArgb(45, 44, 90);
            cardTotalExpenses.Size = new Size(210, 100);
            cardTotalExpenses.Location = new Point(20, 20);
            cardTotalExpenses.ShadowDecoration.Enabled = true;
            cardTotalExpenses.ShadowDecoration.Depth = 10;
            lblTotalTitle.Text = "Total Expenses";
            lblTotalTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotalTitle.ForeColor = Color.White;
            lblTotalTitle.Location = new Point(15, 15);
            lblTotalExpenses.ForeColor = Color.White;
            lblTotalExpenses.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalExpenses.Location = new Point(15, 50);
            lblTotalExpenses.Text = "₱0.00";
            cardTotalExpenses.Controls.Add(lblTotalTitle);
            cardTotalExpenses.Controls.Add(lblTotalExpenses);

            // Expense Count
            cardCount.BackColor = Color.Transparent;
            cardCount.BorderRadius = 10;
            cardCount.FillColor = Color.FromArgb(45, 44, 90);
            cardCount.Size = new Size(150, 100);
            cardCount.Location = new Point(250, 20);
            cardCount.ShadowDecoration.Enabled = true;
            cardCount.ShadowDecoration.Depth = 10;
            lblCountTitle.Text = "Count";
            lblCountTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCountTitle.ForeColor = Color.White;
            lblCountTitle.Location = new Point(15, 15);
            lblExpenseCount.ForeColor = Color.White;
            lblExpenseCount.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblExpenseCount.Location = new Point(15, 50);
            lblExpenseCount.Text = "0";
            cardCount.Controls.Add(lblCountTitle);
            cardCount.Controls.Add(lblExpenseCount);

            // Trend
            cardTrend.BackColor = Color.Transparent;
            cardTrend.BorderRadius = 10;
            cardTrend.FillColor = Color.FromArgb(45, 44, 90);
            cardTrend.Size = new Size(200, 100);
            cardTrend.Location = new Point(420, 20);
            cardTrend.ShadowDecoration.Enabled = true;
            cardTrend.ShadowDecoration.Depth = 10;
            lblTrendTitle.Text = "Monthly Trend";
            lblTrendTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTrendTitle.ForeColor = Color.White;
            lblTrendTitle.Location = new Point(15, 15);
            lblTrend.ForeColor = Color.White;
            lblTrend.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTrend.Location = new Point(15, 50);
            lblTrend.Text = "0%";
            cardTrend.Controls.Add(lblTrendTitle);
            cardTrend.Controls.Add(lblTrend);

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
            btnAdd.Text = "Add Expense";
            btnAdd.Click += btnAdd_Click;
            lblTopTitle.BackColor = Color.Transparent;
            lblTopTitle.ForeColor = Color.White;
            lblTopTitle.Font = new Font("Segoe UI", 9);
            lblTopTitle.Location = new Point(20, 21);
            lblTopTitle.Text = "Search Expenses";
            panelTop.Controls.Add(lblTopTitle);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(btnAdd);

            // =============== SLIDE PANEL ===============
            panelSlide.BackColor = Color.FromArgb(30, 30, 40);
            panelSlide.BorderRadius = 15;
            panelSlide.FillColor = Color.FromArgb(45, 44, 90);
            panelSlide.ShadowDecoration.Enabled = true;
            panelSlide.ShadowDecoration.Depth = 20;
            panelSlide.Location = new Point(0, 190);
            panelSlide.Name = "panelSlide";
            panelSlide.Size = new Size(0, 450);
            panelSlide.TabIndex = 10;

            lblSlideTitle.BackColor = Color.Transparent;
            lblSlideTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSlideTitle.ForeColor = Color.White;
            lblSlideTitle.Location = new Point(20, 20);
            lblSlideTitle.Text = "Add Expense";

            lblExpenseName.BackColor = Color.Transparent;
            lblExpenseName.ForeColor = Color.White;
            lblExpenseName.Location = new Point(20, 60);
            lblExpenseName.Text = "Description";
            txtExpenseName.BorderRadius = 8;
            txtExpenseName.FillColor = Color.FromArgb(60, 60, 80);
            txtExpenseName.ForeColor = Color.White;
            txtExpenseName.Location = new Point(20, 80);
            txtExpenseName.Size = new Size(320, 36);

            lblAmount.BackColor = Color.Transparent;
            lblAmount.ForeColor = Color.White;
            lblAmount.Location = new Point(20, 130);
            lblAmount.Text = "Amount (₱)";
            txtAmount.BorderRadius = 8;
            txtAmount.FillColor = Color.FromArgb(60, 60, 80);
            txtAmount.ForeColor = Color.White;
            txtAmount.Location = new Point(20, 150);
            txtAmount.Size = new Size(150, 36);
            txtAmount.Text = "0.00";

            lblCategory.BackColor = Color.Transparent;
            lblCategory.ForeColor = Color.White;
            lblCategory.Location = new Point(190, 130);
            lblCategory.Text = "Category";
            cmbCategory.BorderRadius = 8;
            cmbCategory.FillColor = Color.FromArgb(60, 60, 80);
            cmbCategory.ForeColor = Color.White;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new[] { "Supplies", "Equipment", "Utilities", "Rent", "Salaries", "Marketing", "Other" });
            cmbCategory.Location = new Point(190, 150);
            cmbCategory.Size = new Size(150, 36);

            lblDate.BackColor = Color.Transparent;
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(20, 200);
            lblDate.Text = "Date";
            dtpDate.BorderRadius = 8;
            dtpDate.FillColor = Color.FromArgb(60, 60, 80);
            dtpDate.ForeColor = Color.White;
            dtpDate.Location = new Point(20, 220);
            dtpDate.Size = new Size(200, 36);

            lblNotes.BackColor = Color.Transparent;
            lblNotes.ForeColor = Color.White;
            lblNotes.Location = new Point(20, 270);
            lblNotes.Text = "Notes (optional)";
            txtNotes.BorderRadius = 8;
            txtNotes.FillColor = Color.FromArgb(60, 60, 80);
            txtNotes.ForeColor = Color.White;
            txtNotes.Multiline = true;
            txtNotes.Location = new Point(20, 290);
            txtNotes.Size = new Size(320, 60);

            btnSave.BorderRadius = 8;
            btnSave.FillColor = Color.FromArgb(76, 175, 80);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 370);
            btnSave.Size = new Size(100, 36);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;

            btnDelete.BorderRadius = 8;
            btnDelete.FillColor = Color.FromArgb(244, 67, 54);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(130, 370);
            btnDelete.Size = new Size(100, 36);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;

            btnCancel.BorderRadius = 8;
            btnCancel.FillColor = Color.Gray;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(240, 370);
            btnCancel.Size = new Size(100, 36);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            panelSlide.Controls.Add(lblSlideTitle);
            panelSlide.Controls.Add(lblExpenseName);
            panelSlide.Controls.Add(txtExpenseName);
            panelSlide.Controls.Add(lblAmount);
            panelSlide.Controls.Add(txtAmount);
            panelSlide.Controls.Add(lblCategory);
            panelSlide.Controls.Add(cmbCategory);
            panelSlide.Controls.Add(lblDate);
            panelSlide.Controls.Add(dtpDate);
            panelSlide.Controls.Add(lblNotes);
            panelSlide.Controls.Add(txtNotes);
            panelSlide.Controls.Add(btnSave);
            panelSlide.Controls.Add(btnDelete);
            panelSlide.Controls.Add(btnCancel);

            // =============== CARD LIST ===============
            flowExpenses.BackColor = Color.FromArgb(26, 25, 62);
            flowExpenses.Dock = DockStyle.Fill;
            flowExpenses.AutoScroll = true;
            flowExpenses.FlowDirection = FlowDirection.TopDown;
            flowExpenses.Padding = new Padding(10);
            flowExpenses.Location = new Point(0, 190);
            flowExpenses.Size = new Size(949, 450);

            // =============== PARENT CONTROL ===============
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(flowExpenses);
            Controls.Add(panelTop);
            Controls.Add(cardTotalExpenses);
            Controls.Add(cardCount);
            Controls.Add(cardTrend);
            Controls.Add(panelSlide);
            Name = "ExpensesControl";
            Size = new Size(949, 640);

            cardTotalExpenses.ResumeLayout(false);
            cardCount.ResumeLayout(false);
            cardTrend.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelSlide.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Declarations
        private Guna2Panel cardTotalExpenses;
        private Guna2HtmlLabel lblTotalExpenses;
        private Guna2HtmlLabel lblTotalTitle;
        private Guna2Panel cardCount;
        private Guna2HtmlLabel lblExpenseCount;
        private Guna2HtmlLabel lblCountTitle;
        private Guna2Panel cardTrend;
        private Guna2HtmlLabel lblTrend;
        private Guna2HtmlLabel lblTrendTitle;

        private Guna2Panel panelTop;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnAdd;
        private Guna2HtmlLabel lblTopTitle;

        private Guna2Panel panelSlide;
        private Guna2HtmlLabel lblSlideTitle;
        private Guna2HtmlLabel lblExpenseName;
        private Guna2TextBox txtExpenseName;
        private Guna2HtmlLabel lblAmount;
        private Guna2TextBox txtAmount;
        private Guna2HtmlLabel lblCategory;
        private Guna2ComboBox cmbCategory;
        private Guna2HtmlLabel lblDate;
        private Guna2DateTimePicker dtpDate;
        private Guna2HtmlLabel lblNotes;
        private Guna2TextBox txtNotes;
        private Guna2Button btnSave;
        private Guna2Button btnDelete;
        private Guna2Button btnCancel;

        private FlowLayoutPanel flowExpenses;
    }
}

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    partial class ExpensesControl
//    {
//        private System.ComponentModel.IContainer components = null;
//        private RoundedPanel panelTotalExpenses;
//        private Label label1;
//        private Label lblTotalExpenses;
//        private Label lblExpenseTrend;
//        private RoundedPanel roundedPanel1;
//        private RoundedPanel roundedPanel2;
//        private RoundedPanel panelCreateExpense;
//        private Label label2;
//        private Label label3;
//        private TextBox txtExpenseName;
//        private Label label4;
//        private TextBox txtAmount;
//        private Label label5;
//        private DateTimePicker dtpExpenseDate;  // ✅ CHANGED FROM TextBox TO DateTimePicker
//        private Label label6;
//        private ComboBox cmbCategory;
//        private FontAwesome.Sharp.IconButton btnCreateExpense;
//        private FontAwesome.Sharp.IconButton btnClearExpense;
//        private DataGridView dgvExpenseHistory;
//        private RoundedPanel roundedPanel3;
//        //private PictureBox pictureBox1;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            panelTotalExpenses = new RoundedPanel();
//            roundedPanel1 = new RoundedPanel();
//            label1 = new Label();
//            roundedPanel2 = new RoundedPanel();
//            lblTotalExpenses = new Label();
//            lblExpenseTrend = new Label();
//            panelCreateExpense = new RoundedPanel();
//            btnClearExpense = new FontAwesome.Sharp.IconButton();
//            btnCreateExpense = new FontAwesome.Sharp.IconButton();
//            cmbCategory = new ComboBox();
//            label6 = new Label();
//            dtpExpenseDate = new DateTimePicker();
//            label5 = new Label();
//            txtAmount = new TextBox();
//            label4 = new Label();
//            txtExpenseName = new TextBox();
//            label3 = new Label();
//            label2 = new Label();
//            dgvExpenseHistory = new DataGridView();
//            roundedPanel3 = new RoundedPanel();
//            panelTotalExpenses.SuspendLayout();
//            roundedPanel1.SuspendLayout();
//            roundedPanel2.SuspendLayout();
//            panelCreateExpense.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)dgvExpenseHistory).BeginInit();
//            SuspendLayout();
//            // 
//            // panelTotalExpenses
//            // 
//            panelTotalExpenses.BackColor = Color.FromArgb(90, 88, 140);
//            panelTotalExpenses.Controls.Add(roundedPanel1);
//            panelTotalExpenses.Controls.Add(roundedPanel2);
//            panelTotalExpenses.Location = new Point(26, 16);
//            panelTotalExpenses.Name = "panelTotalExpenses";
//            panelTotalExpenses.Size = new Size(273, 185);
//            panelTotalExpenses.TabIndex = 0;
//            // 
//            // roundedPanel1
//            // 
//            roundedPanel1.BackColor = Color.FromArgb(26, 25, 62);
//            roundedPanel1.Controls.Add(label1);
//            roundedPanel1.Location = new Point(13, 15);
//            roundedPanel1.Name = "roundedPanel1";
//            roundedPanel1.Size = new Size(245, 58);
//            roundedPanel1.TabIndex = 3;
//            // 
//            // label1
//            // 
//            label1.AutoSize = true;
//            label1.BackColor = Color.FromArgb(26, 25, 62);
//            label1.ForeColor = Color.White;
//            label1.Location = new Point(29, 17);
//            label1.Name = "label1";
//            label1.Size = new Size(190, 20);
//            label1.TabIndex = 0;
//            label1.Text = "Total Expenses This Month :";
//            // 
//            // roundedPanel2
//            // 
//            roundedPanel2.BackColor = SystemColors.ActiveBorder;
//            roundedPanel2.Controls.Add(lblTotalExpenses);
//            roundedPanel2.Controls.Add(lblExpenseTrend);
//            roundedPanel2.Location = new Point(13, 79);
//            roundedPanel2.Name = "roundedPanel2";
//            roundedPanel2.Size = new Size(245, 88);
//            roundedPanel2.TabIndex = 4;
//            // 
//            // lblTotalExpenses
//            // 
//            lblTotalExpenses.AutoSize = true;
//            lblTotalExpenses.BackColor = SystemColors.ActiveBorder;
//            lblTotalExpenses.ForeColor = Color.Black;
//            lblTotalExpenses.Location = new Point(18, 17);
//            lblTotalExpenses.Name = "lblTotalExpenses";
//            lblTotalExpenses.Size = new Size(44, 20);
//            lblTotalExpenses.TabIndex = 1;
//            lblTotalExpenses.Text = "$0.00";
//            // 
//            // lblExpenseTrend
//            // 
//            lblExpenseTrend.AutoSize = true;
//            lblExpenseTrend.BackColor = SystemColors.ActiveBorder;
//            lblExpenseTrend.ForeColor = Color.Black;
//            lblExpenseTrend.Location = new Point(18, 48);
//            lblExpenseTrend.Name = "lblExpenseTrend";
//            lblExpenseTrend.Size = new Size(131, 20);
//            lblExpenseTrend.TabIndex = 2;
//            lblExpenseTrend.Text = "↑ 0% vs last month";
//            // 
//            // panelCreateExpense
//            // 
//            panelCreateExpense.BackColor = Color.FromArgb(90, 88, 140);
//            panelCreateExpense.Controls.Add(btnClearExpense);
//            panelCreateExpense.Controls.Add(btnCreateExpense);
//            panelCreateExpense.Controls.Add(cmbCategory);
//            panelCreateExpense.Controls.Add(label6);
//            panelCreateExpense.Controls.Add(dtpExpenseDate);
//            panelCreateExpense.Controls.Add(label5);
//            panelCreateExpense.Controls.Add(txtAmount);
//            panelCreateExpense.Controls.Add(label4);
//            panelCreateExpense.Controls.Add(txtExpenseName);
//            panelCreateExpense.Controls.Add(label3);
//            panelCreateExpense.Controls.Add(label2);
//            panelCreateExpense.ForeColor = Color.Black;
//            panelCreateExpense.Location = new Point(328, 16);
//            panelCreateExpense.Name = "panelCreateExpense";
//            panelCreateExpense.Size = new Size(581, 185);
//            panelCreateExpense.TabIndex = 1;
//            // 
//            // btnClearExpense
//            // 
//            btnClearExpense.BackColor = Color.FromArgb(244, 67, 54);
//            btnClearExpense.IconChar = FontAwesome.Sharp.IconChar.None;
//            btnClearExpense.IconColor = Color.Black;
//            btnClearExpense.IconFont = FontAwesome.Sharp.IconFont.Auto;
//            btnClearExpense.Location = new Point(364, 140);
//            btnClearExpense.Name = "btnClearExpense";
//            btnClearExpense.Size = new Size(127, 29);
//            btnClearExpense.TabIndex = 11;
//            btnClearExpense.Text = "Clear Form";
//            btnClearExpense.UseVisualStyleBackColor = false;
//            btnClearExpense.Click += btnClearExpense_Click;
//            // 
//            // btnCreateExpense
//            // 
//            btnCreateExpense.BackColor = Color.FromArgb(76, 175, 80);
//            btnCreateExpense.IconChar = FontAwesome.Sharp.IconChar.None;
//            btnCreateExpense.IconColor = Color.Black;
//            btnCreateExpense.IconFont = FontAwesome.Sharp.IconFont.Auto;
//            btnCreateExpense.Location = new Point(214, 140);
//            btnCreateExpense.Name = "btnCreateExpense";
//            btnCreateExpense.Size = new Size(127, 29);
//            btnCreateExpense.TabIndex = 10;
//            btnCreateExpense.Text = "Create Expense";
//            btnCreateExpense.UseVisualStyleBackColor = false;
//            btnCreateExpense.Click += btnCreateExpense_Click;
//            // 
//            // cmbCategory
//            // 
//            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbCategory.FormattingEnabled = true;
//            cmbCategory.Items.AddRange(new object[] { "Supplies", "Equipment", "Utilities", "Rent", "Salaries", "Marketing", "Other" });
//            cmbCategory.Location = new Point(13, 140);
//            cmbCategory.Name = "cmbCategory";
//            cmbCategory.Size = new Size(186, 28);
//            cmbCategory.TabIndex = 9;
//            // 
//            // label6
//            // 
//            label6.AutoSize = true;
//            label6.ForeColor = Color.White;
//            label6.Location = new Point(13, 117);
//            label6.Name = "label6";
//            label6.Size = new Size(76, 20);
//            label6.TabIndex = 7;
//            label6.Text = "Category :";
//            // 
//            // dtpExpenseDate
//            // 
//            dtpExpenseDate.Format = DateTimePickerFormat.Short;
//            dtpExpenseDate.Location = new Point(364, 79);
//            dtpExpenseDate.Name = "dtpExpenseDate";
//            dtpExpenseDate.Size = new Size(127, 27);
//            dtpExpenseDate.TabIndex = 6;
//            // 
//            // label5
//            // 
//            label5.AutoSize = true;
//            label5.ForeColor = Color.White;
//            label5.Location = new Point(364, 56);
//            label5.Name = "label5";
//            label5.Size = new Size(48, 20);
//            label5.TabIndex = 5;
//            label5.Text = "Date :";
//            // 
//            // txtAmount
//            // 
//            txtAmount.Location = new Point(214, 79);
//            txtAmount.Name = "txtAmount";
//            txtAmount.Size = new Size(127, 27);
//            txtAmount.TabIndex = 4;
//            // 
//            // label4
//            // 
//            label4.AutoSize = true;
//            label4.ForeColor = Color.White;
//            label4.Location = new Point(214, 56);
//            label4.Name = "label4";
//            label4.Size = new Size(69, 20);
//            label4.TabIndex = 3;
//            label4.Text = "Amount :";
//            // 
//            // txtExpenseName
//            // 
//            txtExpenseName.Location = new Point(13, 79);
//            txtExpenseName.Name = "txtExpenseName";
//            txtExpenseName.Size = new Size(186, 27);
//            txtExpenseName.TabIndex = 2;
//            // 
//            // label3
//            // 
//            label3.AutoSize = true;
//            label3.ForeColor = Color.White;
//            label3.Location = new Point(13, 56);
//            label3.Name = "label3";
//            label3.Size = new Size(110, 20);
//            label3.TabIndex = 1;
//            label3.Text = "Expense Name:";
//            // 
//            // label2
//            // 
//            label2.AutoSize = true;
//            label2.ForeColor = Color.White;
//            label2.Location = new Point(13, 15);
//            label2.Name = "label2";
//            label2.Size = new Size(144, 20);
//            label2.TabIndex = 0;
//            label2.Text = "Create New Expense";
//            // 
//            // dgvExpenseHistory
//            // 
//            dgvExpenseHistory.AllowUserToAddRows = false;
//            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            dgvExpenseHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            dgvExpenseHistory.Location = new Point(57, 274);
//            dgvExpenseHistory.Name = "dgvExpenseHistory";
//            dgvExpenseHistory.ReadOnly = true;
//            dgvExpenseHistory.RowHeadersVisible = false;
//            dgvExpenseHistory.RowHeadersWidth = 51;
//            dgvExpenseHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            dgvExpenseHistory.Size = new Size(805, 292);
//            dgvExpenseHistory.TabIndex = 2;
//            // 
//            // roundedPanel3
//            // 
//            roundedPanel3.BackColor = Color.FromArgb(90, 88, 140);
//            roundedPanel3.Location = new Point(26, 244);
//            roundedPanel3.Name = "roundedPanel3";
//            roundedPanel3.Size = new Size(883, 355);
//            roundedPanel3.TabIndex = 3;
//            // 
//            // ExpensesControl
//            // 
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(26, 25, 62);
//            Controls.Add(dgvExpenseHistory);
//            Controls.Add(panelCreateExpense);
//            Controls.Add(panelTotalExpenses);
//            Controls.Add(roundedPanel3);
//            Name = "ExpensesControl";
//            Size = new Size(949, 640);
//            panelTotalExpenses.ResumeLayout(false);
//            roundedPanel1.ResumeLayout(false);
//            roundedPanel1.PerformLayout();
//            roundedPanel2.ResumeLayout(false);
//            roundedPanel2.PerformLayout();
//            panelCreateExpense.ResumeLayout(false);
//            panelCreateExpense.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)dgvExpenseHistory).EndInit();
//            ResumeLayout(false);
//        }
//    }
//}