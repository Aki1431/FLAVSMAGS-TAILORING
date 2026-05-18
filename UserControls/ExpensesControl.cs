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
    public partial class ExpensesControl : UserControl
    {
        private readonly ExpenseService _expenseService;
        private int? _selectedExpenseId = null;
        private bool isSlideOpen = false;
        private System.Windows.Forms.Timer slideTimer = null!;

        public ExpensesControl()
        {
            InitializeComponent();
            this.Text = "Expenses";
            _expenseService = new ExpenseService();
            _expenseService.DataChanged += OnDataChanged;
            SetupSlideTimer();
            LoadExpenseData();
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
                if (panelSlide.Width < 360)
                    panelSlide.Width += step;
                else { panelSlide.Width = 360; slideTimer.Stop(); }
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
            btnAdd.Text = "Add Expense";
        }

        private void OnDataChanged(object? sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)LoadExpenseData);
            else LoadExpenseData();
        }

        private void LoadExpenseData()
        {
            LoadStatCards();
            LoadExpenseCards();
        }

        private void LoadStatCards()
        {
            var stats = _expenseService.GetStatistics();
            lblTotalExpenses.Text = $"₱{stats.TotalExpenses:N2}";
            lblExpenseCount.Text = stats.ExpenseCount.ToString();

            string sign = stats.PercentChange >= 0 ? "↑" : "↓";
            lblTrend.Text = $"{sign} {Math.Abs(stats.PercentChange):F1}%";
            lblTrend.ForeColor = stats.PercentChange >= 0 ? Color.Red : Color.Green;
        }

        private void LoadExpenseCards(string filter = "")
        {
            flowExpenses.Controls.Clear();
            IEnumerable<Expense> expenses = _expenseService.GetAllExpenses()
                .OrderByDescending(e => e.ExpenseDate);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                expenses = expenses.Where(e =>
                    e.Description.ToLower().Contains(filter.ToLower()) ||
                    e.Category.ToString().ToLower().Contains(filter.ToLower())
                );
            }

            foreach (var expense in expenses)
                flowExpenses.Controls.Add(CreateExpenseCard(expense));
        }

        private Guna2Panel CreateExpenseCard(Expense expense)
        {
            Guna2Panel card = new Guna2Panel
            {
                Width = flowExpenses.Width - 25,
                Height = 80,
                BackColor = Color.Transparent,
                FillColor = Color.FromArgb(45, 44, 90),
                BorderRadius = 10,
                Margin = new Padding(5),
                ShadowDecoration = { Enabled = true, Depth = 5 },
                Tag = expense
            };

            Label lblDesc = new Label { Text = expense.Description, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 8), AutoSize = true };
            Label lblCat = new Label { Text = expense.Category.ToString(), Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(10, 32), AutoSize = true };
            Label lblDate = new Label { Text = expense.ExpenseDate.ToString("MM/dd/yyyy"), Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(300, 8), AutoSize = true };
            Label lblAmount = new Label { Text = $"₱{expense.Amount:N2}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(300, 30), AutoSize = true };

            card.Click += (s, e) =>
            {
                _selectedExpenseId = expense.ExpenseId;
                FillFormFromExpense(expense);
                lblSlideTitle.Text = "Edit Expense";
                btnDelete.Visible = true;
                OpenSlide();
            };

            card.Controls.AddRange(new Control[] { lblDesc, lblCat, lblDate, lblAmount });
            return card;
        }

        private void FillFormFromExpense(Expense expense)
        {
            txtExpenseName.Text = expense.Description;
            txtAmount.Text = expense.Amount.ToString("F2");
            cmbCategory.SelectedItem = expense.Category.ToString();
            dtpDate.Value = expense.ExpenseDate;
            txtNotes.Text = expense.Notes;
        }

        // ========== Add / Edit / Delete ==========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isSlideOpen) { CloseSlide(); return; }
            _selectedExpenseId = null;
            ClearForm();
            lblSlideTitle.Text = "Add Expense";
            btnDelete.Visible = false;
            OpenSlide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpenseName.Text) ||
                !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0 ||
                cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please fill all required fields correctly.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Enum.TryParse<ExpenseCategory>(cmbCategory.SelectedItem.ToString(), out ExpenseCategory category))
            {
                MessageBox.Show("Invalid category.", "Error");
                return;
            }

            try
            {
                if (_selectedExpenseId.HasValue)
                {
                    var result = _expenseService.UpdateExpense(_selectedExpenseId.Value,
                        txtExpenseName.Text.Trim(), category, amount, dtpDate.Value, txtNotes.Text.Trim());
                    if (result.IsSuccess) { MessageBox.Show("Expense updated!", "Success"); CloseSlide(); }
                    else MessageBox.Show(result.Message, "Error");
                }
                else
                {
                    var result = _expenseService.CreateExpense(txtExpenseName.Text.Trim(), category, amount, dtpDate.Value, txtNotes.Text.Trim());
                    if (result.IsSuccess) { MessageBox.Show("Expense created!", "Success"); CloseSlide(); }
                    else MessageBox.Show(result.Message, "Error");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedExpenseId.HasValue) return;
            if (MessageBox.Show("Delete this expense?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var result = _expenseService.DeleteExpense(_selectedExpenseId.Value);
                if (result.IsSuccess) { MessageBox.Show("Expense deleted.", "Success"); CloseSlide(); }
                else MessageBox.Show(result.Message, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => CloseSlide();

        private void btnSearch_Click(object sender, EventArgs e) => LoadExpenseCards(txtSearch.Text.Trim());

        private void ClearForm()
        {
            txtExpenseName.Clear();
            txtAmount.Text = "0.00";
            cmbCategory.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            txtNotes.Clear();
            _selectedExpenseId = null;
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
//    public partial class ExpensesControl : UserControl
//    {
//        private readonly ExpenseService _expenseService;
//        private int? _selectedExpenseId = null; // Track selected expense for editing

//        public ExpensesControl()
//        {
//            InitializeComponent();
//            this.Text = "Expenses";

//            // Initialize service
//            _expenseService = new ExpenseService();

//            // Subscribe to data changes
//            _expenseService.DataChanged += OnDataChanged;

//            // Load initial data
//            LoadExpenseData();
//        }

//        /// <summary>
//        /// Event handler for data changes - refresh all data
//        /// </summary>
//        private void OnDataChanged(object? sender, EventArgs e)
//        {
//            LoadExpenseData();
//        }

//        /// <summary>
//        /// Load all expense data from service
//        /// </summary>
//        private void LoadExpenseData()
//        {
//            try
//            {
//                LoadExpenseStats();
//                LoadExpenseHistory();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading expense data: {ex.Message}",
//                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load expense statistics (total, trend)
//        /// </summary>
//        private void LoadExpenseStats()
//        {
//            var stats = _expenseService.GetStatistics();

//            lblTotalExpenses.Text = $"${stats.TotalExpenses:N2}";

//            // Calculate and display trend
//            string trend = stats.PercentChange >= 0 ? "↑" : "↓";
//            string trendText = $"{trend} {Math.Abs(stats.PercentChange):F1}% vs last month";
//            lblExpenseTrend.Text = trendText;

//            // Change color based on trend (red if increasing, green if decreasing)
//            lblExpenseTrend.ForeColor = stats.PercentChange >= 0 ? Color.Red : Color.Green;
//        }

//        /// <summary>
//        /// Load expense history into DataGridView
//        /// </summary>
//        private void LoadExpenseHistory()
//        {
//            var expenses = _expenseService.GetAllExpenses();

//            DataTable dt = new DataTable();
//            dt.Columns.Add("Expense ID", typeof(int)); // Hidden column for ID
//            dt.Columns.Add("Date", typeof(string));
//            dt.Columns.Add("Description", typeof(string));
//            dt.Columns.Add("Category", typeof(string));
//            dt.Columns.Add("Amount", typeof(string));

//            foreach (var expense in expenses.OrderByDescending(e => e.ExpenseDate))
//            {
//                dt.Rows.Add(
//                    expense.ExpenseId,
//                    expense.ExpenseDate.ToString("MM/dd/yyyy"),
//                    expense.Description,
//                    expense.Category.ToString(),
//                    $"${expense.Amount:F2}"
//                );
//            }

//            dgvExpenseHistory.DataSource = dt;
//            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//            // Hide the ID column but keep it for reference
//            if (dgvExpenseHistory.Columns["Expense ID"] != null)
//                dgvExpenseHistory.Columns["Expense ID"].Visible = false;

//            // Add Edit and Delete buttons if they don't exist
//            if (!dgvExpenseHistory.Columns.Contains("Edit"))
//            {
//                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
//                {
//                    Name = "Edit",
//                    HeaderText = "Edit",
//                    Text = "✏️ Edit",
//                    UseColumnTextForButtonValue = true,
//                    Width = 80
//                };
//                dgvExpenseHistory.Columns.Add(editColumn);
//            }

//            if (!dgvExpenseHistory.Columns.Contains("Delete"))
//            {
//                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
//                {
//                    Name = "Delete",
//                    HeaderText = "Delete",
//                    Text = "🗑️ Delete",
//                    UseColumnTextForButtonValue = true,
//                    Width = 80
//                };
//                dgvExpenseHistory.Columns.Add(deleteColumn);
//            }

//            // Style the grid
//            dgvExpenseHistory.BackgroundColor = Color.FromArgb(37, 36, 81);
//            dgvExpenseHistory.ForeColor = Color.White;
//            dgvExpenseHistory.GridColor = Color.FromArgb(90, 88, 140);
//            dgvExpenseHistory.EnableHeadersVisualStyles = false;
//            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
//            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

//            // Add cell click handler
//            dgvExpenseHistory.CellClick -= dgvExpenseHistory_CellClick; // Remove old handler
//            dgvExpenseHistory.CellClick += dgvExpenseHistory_CellClick;
//        }

//        /// <summary>
//        /// Handle Edit and Delete button clicks in grid
//        /// </summary>
//        private void dgvExpenseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            // Validate click
//            if (e.RowIndex < 0 || e.ColumnIndex < 0)
//                return;

//            try
//            {
//                var row = dgvExpenseHistory.Rows[e.RowIndex];
//                int expenseId = Convert.ToInt32(row.Cells["Expense ID"].Value);
//                string columnName = dgvExpenseHistory.Columns[e.ColumnIndex].Name;

//                if (columnName == "Edit")
//                {
//                    EditExpense(expenseId);
//                }
//                else if (columnName == "Delete")
//                {
//                    DeleteExpense(expenseId);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error processing action: {ex.Message}",
//                    "Action Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load expense into form for editing
//        /// </summary>
//        private void EditExpense(int expenseId)
//        {
//            var expense = _expenseService.GetAllExpenses()
//                .FirstOrDefault(e => e.ExpenseId == expenseId);

//            if (expense == null)
//            {
//                MessageBox.Show("Expense not found.", "Edit Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            _selectedExpenseId = expense.ExpenseId;
//            txtExpenseName.Text = expense.Description;
//            txtAmount.Text = expense.Amount.ToString("F2");
//            cmbCategory.SelectedItem = expense.Category.ToString();
//            dtpExpenseDate.Value = expense.ExpenseDate;

//            btnCreateExpense.Text = "Update Expense";
//            btnCreateExpense.BackColor = Color.FromArgb(255, 152, 0); // Orange for update
//        }

//        /// <summary>
//        /// Delete expense with confirmation
//        /// </summary>
//        private void DeleteExpense(int expenseId)
//        {
//            var expense = _expenseService.GetAllExpenses()
//                .FirstOrDefault(e => e.ExpenseId == expenseId);

//            if (expense == null)
//                return;

//            var confirmResult = MessageBox.Show(
//                $"Are you sure you want to delete this expense?\n\n" +
//                $"Description: {expense.Description}\n" +
//                $"Amount: ${expense.Amount:F2}\n" +
//                $"Category: {expense.Category}",
//                "Confirm Delete",
//                MessageBoxButtons.YesNo,
//                MessageBoxIcon.Warning
//            );

//            if (confirmResult == DialogResult.Yes)
//            {
//                var result = _expenseService.DeleteExpense(expenseId);

//                if (result.IsSuccess)
//                {
//                    MessageBox.Show("Expense deleted successfully!", "Success",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                else
//                {
//                    MessageBox.Show(result.Message, "Delete Failed",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        /// <summary>
//        /// Create or Update expense
//        /// </summary>
//        private void btnCreateExpense_Click(object sender, EventArgs e)
//        {
//            // Validation
//            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
//            {
//                MessageBox.Show("Please enter expense name!", "Validation",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(txtAmount.Text) ||
//                !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
//            {
//                MessageBox.Show("Please enter a valid amount!", "Validation",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (cmbCategory.SelectedItem == null)
//            {
//                MessageBox.Show("Please select a category!", "Validation",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            try
//            {
//                // Parse category
//                if (!Enum.TryParse<ExpenseCategory>(cmbCategory.SelectedItem.ToString(), out var category))
//                {
//                    MessageBox.Show("Invalid category selected.", "Validation",
//                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (_selectedExpenseId.HasValue)
//                {
//                    // UPDATE existing expense
//                    var result = _expenseService.UpdateExpense(
//                        _selectedExpenseId.Value,
//                        txtExpenseName.Text,
//                        category,
//                        amount,
//                        dtpExpenseDate.Value
//                    );

//                    if (result.IsSuccess)
//                    {
//                        MessageBox.Show("Expense updated successfully!", "Success",
//                            MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        ClearForm();
//                    }
//                    else
//                    {
//                        MessageBox.Show(result.Message, "Update Failed",
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                else
//                {
//                    // CREATE new expense
//                    var result = _expenseService.CreateExpense(
//                        txtExpenseName.Text,
//                        category,
//                        amount,
//                        dtpExpenseDate.Value
//                    );

//                    if (result.IsSuccess)
//                    {
//                        MessageBox.Show($"Expense created successfully!\n" +
//                                      $"Description: {result.Data!.Description}\n" +
//                                      $"Amount: ${result.Data.Amount:F2}",
//                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        ClearForm();
//                    }
//                    else
//                    {
//                        MessageBox.Show(result.Message, "Creation Failed",
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error: {ex.Message}", "Operation Failed",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Clear form and reset to create mode
//        /// </summary>
//        private void btnClearExpense_Click(object sender, EventArgs e)
//        {
//            ClearForm();
//        }

//        /// <summary>
//        /// Clear all form fields
//        /// </summary>
//        private void ClearForm()
//        {
//            txtExpenseName.Clear();
//            txtAmount.Clear();
//            cmbCategory.SelectedIndex = -1;
//            dtpExpenseDate.Value = DateTime.Now;
//            _selectedExpenseId = null;
//            btnCreateExpense.Text = "Create Expense";
//            btnCreateExpense.BackColor = Color.FromArgb(76, 175, 80); // Green for create
//        }
//    }
//}