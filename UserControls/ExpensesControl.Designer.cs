namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class ExpensesControl
    {
        private System.ComponentModel.IContainer components = null;
        private RoundedPanel panelTotalExpenses;
        private Label label1;
        private Label lblTotalExpenses;
        private Label lblExpenseTrend;
        private RoundedPanel roundedPanel1;
        private RoundedPanel roundedPanel2;
        private RoundedPanel panelCreateExpense;
        private Label label2;
        private Label label3;
        private TextBox txtExpenseName;
        private Label label4;
        private TextBox txtAmount;
        private Label label5;
        private DateTimePicker dtpExpenseDate;  // ✅ CHANGED FROM TextBox TO DateTimePicker
        private Label label6;
        private ComboBox cmbCategory;
        private FontAwesome.Sharp.IconButton btnCreateExpense;
        private FontAwesome.Sharp.IconButton btnClearExpense;
        private DataGridView dgvExpenseHistory;
        private RoundedPanel roundedPanel3;
        private PictureBox pictureBox1;

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
            panelTotalExpenses = new RoundedPanel();
            roundedPanel1 = new RoundedPanel();
            label1 = new Label();
            roundedPanel2 = new RoundedPanel();
            lblTotalExpenses = new Label();
            lblExpenseTrend = new Label();
            panelCreateExpense = new RoundedPanel();
            btnClearExpense = new FontAwesome.Sharp.IconButton();
            btnCreateExpense = new FontAwesome.Sharp.IconButton();
            cmbCategory = new ComboBox();
            label6 = new Label();
            dtpExpenseDate = new DateTimePicker();
            label5 = new Label();
            txtAmount = new TextBox();
            label4 = new Label();
            txtExpenseName = new TextBox();
            label3 = new Label();
            label2 = new Label();
            dgvExpenseHistory = new DataGridView();
            roundedPanel3 = new RoundedPanel();
            panelTotalExpenses.SuspendLayout();
            roundedPanel1.SuspendLayout();
            roundedPanel2.SuspendLayout();
            panelCreateExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenseHistory).BeginInit();
            SuspendLayout();
            // 
            // panelTotalExpenses
            // 
            panelTotalExpenses.BackColor = Color.FromArgb(90, 88, 140);
            panelTotalExpenses.Controls.Add(roundedPanel1);
            panelTotalExpenses.Controls.Add(roundedPanel2);
            panelTotalExpenses.Location = new Point(26, 16);
            panelTotalExpenses.Name = "panelTotalExpenses";
            panelTotalExpenses.Size = new Size(273, 185);
            panelTotalExpenses.TabIndex = 0;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(26, 25, 62);
            roundedPanel1.Controls.Add(label1);
            roundedPanel1.Location = new Point(13, 15);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(245, 58);
            roundedPanel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(26, 25, 62);
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 17);
            label1.Name = "label1";
            label1.Size = new Size(190, 20);
            label1.TabIndex = 0;
            label1.Text = "Total Expenses This Month :";
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = SystemColors.ActiveBorder;
            roundedPanel2.Controls.Add(lblTotalExpenses);
            roundedPanel2.Controls.Add(lblExpenseTrend);
            roundedPanel2.Location = new Point(13, 79);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(245, 88);
            roundedPanel2.TabIndex = 4;
            // 
            // lblTotalExpenses
            // 
            lblTotalExpenses.AutoSize = true;
            lblTotalExpenses.BackColor = SystemColors.ActiveBorder;
            lblTotalExpenses.ForeColor = Color.Black;
            lblTotalExpenses.Location = new Point(18, 17);
            lblTotalExpenses.Name = "lblTotalExpenses";
            lblTotalExpenses.Size = new Size(44, 20);
            lblTotalExpenses.TabIndex = 1;
            lblTotalExpenses.Text = "$0.00";
            // 
            // lblExpenseTrend
            // 
            lblExpenseTrend.AutoSize = true;
            lblExpenseTrend.BackColor = SystemColors.ActiveBorder;
            lblExpenseTrend.ForeColor = Color.Black;
            lblExpenseTrend.Location = new Point(18, 48);
            lblExpenseTrend.Name = "lblExpenseTrend";
            lblExpenseTrend.Size = new Size(131, 20);
            lblExpenseTrend.TabIndex = 2;
            lblExpenseTrend.Text = "↑ 0% vs last month";
            // 
            // panelCreateExpense
            // 
            panelCreateExpense.BackColor = Color.FromArgb(90, 88, 140);
            panelCreateExpense.Controls.Add(btnClearExpense);
            panelCreateExpense.Controls.Add(btnCreateExpense);
            panelCreateExpense.Controls.Add(cmbCategory);
            panelCreateExpense.Controls.Add(label6);
            panelCreateExpense.Controls.Add(dtpExpenseDate);
            panelCreateExpense.Controls.Add(label5);
            panelCreateExpense.Controls.Add(txtAmount);
            panelCreateExpense.Controls.Add(label4);
            panelCreateExpense.Controls.Add(txtExpenseName);
            panelCreateExpense.Controls.Add(label3);
            panelCreateExpense.Controls.Add(label2);
            panelCreateExpense.ForeColor = Color.Black;
            panelCreateExpense.Location = new Point(328, 16);
            panelCreateExpense.Name = "panelCreateExpense";
            panelCreateExpense.Size = new Size(581, 185);
            panelCreateExpense.TabIndex = 1;
            // 
            // btnClearExpense
            // 
            btnClearExpense.BackColor = Color.FromArgb(244, 67, 54);
            btnClearExpense.IconChar = FontAwesome.Sharp.IconChar.None;
            btnClearExpense.IconColor = Color.Black;
            btnClearExpense.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClearExpense.Location = new Point(364, 140);
            btnClearExpense.Name = "btnClearExpense";
            btnClearExpense.Size = new Size(127, 29);
            btnClearExpense.TabIndex = 11;
            btnClearExpense.Text = "Clear Form";
            btnClearExpense.UseVisualStyleBackColor = false;
            btnClearExpense.Click += btnClearExpense_Click;
            // 
            // btnCreateExpense
            // 
            btnCreateExpense.BackColor = Color.FromArgb(76, 175, 80);
            btnCreateExpense.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCreateExpense.IconColor = Color.Black;
            btnCreateExpense.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCreateExpense.Location = new Point(214, 140);
            btnCreateExpense.Name = "btnCreateExpense";
            btnCreateExpense.Size = new Size(127, 29);
            btnCreateExpense.TabIndex = 10;
            btnCreateExpense.Text = "Create Expense";
            btnCreateExpense.UseVisualStyleBackColor = false;
            btnCreateExpense.Click += btnCreateExpense_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Items.AddRange(new object[] { "Supplies", "Equipment", "Utilities", "Rent", "Salaries", "Marketing", "Other" });
            cmbCategory.Location = new Point(13, 140);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(186, 28);
            cmbCategory.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(13, 117);
            label6.Name = "label6";
            label6.Size = new Size(76, 20);
            label6.TabIndex = 7;
            label6.Text = "Category :";
            // 
            // dtpExpenseDate
            // 
            dtpExpenseDate.Format = DateTimePickerFormat.Short;
            dtpExpenseDate.Location = new Point(364, 79);
            dtpExpenseDate.Name = "dtpExpenseDate";
            dtpExpenseDate.Size = new Size(127, 27);
            dtpExpenseDate.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(364, 56);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 5;
            label5.Text = "Date :";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(214, 79);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(127, 27);
            txtAmount.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(214, 56);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 3;
            label4.Text = "Amount :";
            // 
            // txtExpenseName
            // 
            txtExpenseName.Location = new Point(13, 79);
            txtExpenseName.Name = "txtExpenseName";
            txtExpenseName.Size = new Size(186, 27);
            txtExpenseName.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(13, 56);
            label3.Name = "label3";
            label3.Size = new Size(110, 20);
            label3.TabIndex = 1;
            label3.Text = "Expense Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(13, 15);
            label2.Name = "label2";
            label2.Size = new Size(144, 20);
            label2.TabIndex = 0;
            label2.Text = "Create New Expense";
            // 
            // dgvExpenseHistory
            // 
            dgvExpenseHistory.AllowUserToAddRows = false;
            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpenseHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpenseHistory.Location = new Point(57, 274);
            dgvExpenseHistory.Name = "dgvExpenseHistory";
            dgvExpenseHistory.ReadOnly = true;
            dgvExpenseHistory.RowHeadersVisible = false;
            dgvExpenseHistory.RowHeadersWidth = 51;
            dgvExpenseHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenseHistory.Size = new Size(805, 292);
            dgvExpenseHistory.TabIndex = 2;
            // 
            // roundedPanel3
            // 
            roundedPanel3.BackColor = Color.FromArgb(90, 88, 140);
            roundedPanel3.Location = new Point(26, 244);
            roundedPanel3.Name = "roundedPanel3";
            roundedPanel3.Size = new Size(883, 355);
            roundedPanel3.TabIndex = 3;
            // 
            // ExpensesControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(dgvExpenseHistory);
            Controls.Add(panelCreateExpense);
            Controls.Add(panelTotalExpenses);
            Controls.Add(roundedPanel3);
            Name = "ExpensesControl";
            Size = new Size(949, 640);
            panelTotalExpenses.ResumeLayout(false);
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            panelCreateExpense.ResumeLayout(false);
            panelCreateExpense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenseHistory).EndInit();
            ResumeLayout(false);
        }
    }
}