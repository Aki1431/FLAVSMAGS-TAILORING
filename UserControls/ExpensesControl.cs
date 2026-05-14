using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Services;
using FLAVSMAGS_TAILORING.Models;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class ExpensesControl : UserControl
    {
        private readonly ExpenseService _expenseService;
        private int? _selectedExpenseId = null; // Track selected expense for editing

        public ExpensesControl()
        {
            InitializeComponent();
            this.Text = "Expenses";

            // Initialize service
            _expenseService = new ExpenseService();

            // Subscribe to data changes
            _expenseService.DataChanged += OnDataChanged;

            // Load initial data
            LoadExpenseData();
        }

        /// <summary>
        /// Event handler for data changes - refresh all data
        /// </summary>
        private void OnDataChanged(object? sender, EventArgs e)
        {
            LoadExpenseData();
        }

        /// <summary>
        /// Load all expense data from service
        /// </summary>
        private void LoadExpenseData()
        {
            try
            {
                LoadExpenseStats();
                LoadExpenseHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expense data: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load expense statistics (total, trend)
        /// </summary>
        private void LoadExpenseStats()
        {
            var stats = _expenseService.GetStatistics();

            lblTotalExpenses.Text = $"${stats.TotalExpenses:N2}";

            // Calculate and display trend
            string trend = stats.PercentChange >= 0 ? "↑" : "↓";
            string trendText = $"{trend} {Math.Abs(stats.PercentChange):F1}% vs last month";
            lblExpenseTrend.Text = trendText;

            // Change color based on trend (red if increasing, green if decreasing)
            lblExpenseTrend.ForeColor = stats.PercentChange >= 0 ? Color.Red : Color.Green;
        }

        /// <summary>
        /// Load expense history into DataGridView
        /// </summary>
        private void LoadExpenseHistory()
        {
            var expenses = _expenseService.GetAllExpenses();

            DataTable dt = new DataTable();
            dt.Columns.Add("Expense ID", typeof(int)); // Hidden column for ID
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Amount", typeof(string));

            foreach (var expense in expenses.OrderByDescending(e => e.ExpenseDate))
            {
                dt.Rows.Add(
                    expense.ExpenseId,
                    expense.ExpenseDate.ToString("MM/dd/yyyy"),
                    expense.Description,
                    expense.Category.ToString(),
                    $"${expense.Amount:F2}"
                );
            }

            dgvExpenseHistory.DataSource = dt;
            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Hide the ID column but keep it for reference
            if (dgvExpenseHistory.Columns["Expense ID"] != null)
                dgvExpenseHistory.Columns["Expense ID"].Visible = false;

            // Add Edit and Delete buttons if they don't exist
            if (!dgvExpenseHistory.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "Edit",
                    Text = "✏️ Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 80
                };
                dgvExpenseHistory.Columns.Add(editColumn);
            }

            if (!dgvExpenseHistory.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "Delete",
                    Text = "🗑️ Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 80
                };
                dgvExpenseHistory.Columns.Add(deleteColumn);
            }

            // Style the grid
            dgvExpenseHistory.BackgroundColor = Color.FromArgb(37, 36, 81);
            dgvExpenseHistory.ForeColor = Color.White;
            dgvExpenseHistory.GridColor = Color.FromArgb(90, 88, 140);
            dgvExpenseHistory.EnableHeadersVisualStyles = false;
            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Add cell click handler
            dgvExpenseHistory.CellClick -= dgvExpenseHistory_CellClick; // Remove old handler
            dgvExpenseHistory.CellClick += dgvExpenseHistory_CellClick;
        }

        /// <summary>
        /// Handle Edit and Delete button clicks in grid
        /// </summary>
        private void dgvExpenseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validate click
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            try
            {
                var row = dgvExpenseHistory.Rows[e.RowIndex];
                int expenseId = Convert.ToInt32(row.Cells["Expense ID"].Value);
                string columnName = dgvExpenseHistory.Columns[e.ColumnIndex].Name;

                if (columnName == "Edit")
                {
                    EditExpense(expenseId);
                }
                else if (columnName == "Delete")
                {
                    DeleteExpense(expenseId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing action: {ex.Message}",
                    "Action Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load expense into form for editing
        /// </summary>
        private void EditExpense(int expenseId)
        {
            var expense = _expenseService.GetAllExpenses()
                .FirstOrDefault(e => e.ExpenseId == expenseId);

            if (expense == null)
            {
                MessageBox.Show("Expense not found.", "Edit Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _selectedExpenseId = expense.ExpenseId;
            txtExpenseName.Text = expense.Description;
            txtAmount.Text = expense.Amount.ToString("F2");
            cmbCategory.SelectedItem = expense.Category.ToString();
            dtpExpenseDate.Value = expense.ExpenseDate;

            btnCreateExpense.Text = "Update Expense";
            btnCreateExpense.BackColor = Color.FromArgb(255, 152, 0); // Orange for update
        }

        /// <summary>
        /// Delete expense with confirmation
        /// </summary>
        private void DeleteExpense(int expenseId)
        {
            var expense = _expenseService.GetAllExpenses()
                .FirstOrDefault(e => e.ExpenseId == expenseId);

            if (expense == null)
                return;

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete this expense?\n\n" +
                $"Description: {expense.Description}\n" +
                $"Amount: ${expense.Amount:F2}\n" +
                $"Category: {expense.Category}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {
                var result = _expenseService.DeleteExpense(expenseId);

                if (result.IsSuccess)
                {
                    MessageBox.Show("Expense deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Delete Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Create or Update expense
        /// </summary>
        private void btnCreateExpense_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
            {
                MessageBox.Show("Please enter expense name!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) ||
                !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Parse category
                if (!Enum.TryParse<ExpenseCategory>(cmbCategory.SelectedItem.ToString(), out var category))
                {
                    MessageBox.Show("Invalid category selected.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_selectedExpenseId.HasValue)
                {
                    // UPDATE existing expense
                    var result = _expenseService.UpdateExpense(
                        _selectedExpenseId.Value,
                        txtExpenseName.Text,
                        category,
                        amount,
                        dtpExpenseDate.Value
                    );

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Expense updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    // CREATE new expense
                    var result = _expenseService.CreateExpense(
                        txtExpenseName.Text,
                        category,
                        amount,
                        dtpExpenseDate.Value
                    );

                    if (result.IsSuccess)
                    {
                        MessageBox.Show($"Expense created successfully!\n" +
                                      $"Description: {result.Data!.Description}\n" +
                                      $"Amount: ${result.Data.Amount:F2}",
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
        private void btnClearExpense_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Clear all form fields
        /// </summary>
        private void ClearForm()
        {
            txtExpenseName.Clear();
            txtAmount.Clear();
            cmbCategory.SelectedIndex = -1;
            dtpExpenseDate.Value = DateTime.Now;
            _selectedExpenseId = null;
            btnCreateExpense.Text = "Create Expense";
            btnCreateExpense.BackColor = Color.FromArgb(76, 175, 80); // Green for create
        }
    }
}


//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class ExpensesControl : UserControl
//    {
//        public ExpensesControl()
//        {
//            InitializeComponent();
//            this.Text = "Expenses";
//            LoadExpenseData();
//        }

//        private void LoadExpenseData()
//        {
//            lblTotalExpenses.Text = "$12,450.00";
//            lblExpenseTrend.Text = "↑ 5% vs last month";

//            DataTable dt = new DataTable();
//            dt.Columns.Add("Date", typeof(string));
//            dt.Columns.Add("Description", typeof(string));
//            dt.Columns.Add("Category", typeof(string));
//            dt.Columns.Add("Amount", typeof(string));

//            dt.Rows.Add("05/14/2024", "Thread - Red", "Supplies", "$150.00");
//            dt.Rows.Add("05/13/2024", "Tailor Scissors", "Equipment", "$75.00");
//            dt.Rows.Add("05/12/2024", "Electricity Bill", "Utilities", "$250.00");
//            dt.Rows.Add("05/11/2024", "Monthly Rent", "Rent", "$1,500.00");
//            dt.Rows.Add("05/10/2024", "Staff Salary", "Salaries", "$2,000.00");
//            dt.Rows.Add("05/09/2024", "Facebook Ads", "Marketing", "$100.00");

//            dgvExpenseHistory.DataSource = dt;
//            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//            if (!dgvExpenseHistory.Columns.Contains("Actions"))
//            {
//                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
//                editColumn.Name = "Actions";
//                editColumn.HeaderText = "Actions";
//                editColumn.Text = "✏️ Edit";
//                editColumn.UseColumnTextForButtonValue = true;
//                dgvExpenseHistory.Columns.Add(editColumn);
//            }

//            dgvExpenseHistory.BackgroundColor = Color.FromArgb(37, 36, 81);
//            dgvExpenseHistory.ForeColor = Color.White;
//            dgvExpenseHistory.GridColor = Color.FromArgb(90, 88, 140);
//            dgvExpenseHistory.EnableHeadersVisualStyles = false;
//            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
//            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
//        }

//        private void btnCreateExpense_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
//            {
//                MessageBox.Show("Please enter expense name!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(txtAmount.Text))
//            {
//                MessageBox.Show("Please enter amount!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            MessageBox.Show($"Expense created: {txtExpenseName.Text}\nAmount: ${txtAmount.Text}",
//                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            txtExpenseName.Text = "";
//            txtAmount.Text = "";
//            cmbCategory.SelectedIndex = -1;
//            dtpExpenseDate.Value = DateTime.Now;
//        }

//        private void btnClearExpense_Click(object sender, EventArgs e)
//        {
//            txtExpenseName.Text = "";
//            txtAmount.Text = "";
//            cmbCategory.SelectedIndex = -1;
//            dtpExpenseDate.Value = DateTime.Now;
//        }

//        private void dgvExpenseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            // Check if the click is valid
//            if (e.RowIndex < 0 || e.ColumnIndex < 0)
//                return;

//            // Check if the clicked column is the Actions column
//            if (dgvExpenseHistory.Columns[e.ColumnIndex].Name == "Actions")
//            {
//                // Safely get the description
//                DataGridViewRow row = dgvExpenseHistory.Rows[e.RowIndex];
//                string description = "this expense";

//                if (row.Cells["Description"].Value != null)
//                {
//                    description = row.Cells["Description"].Value.ToString();
//                }

//                MessageBox.Show($"Action for: {description}", "Expense Action",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//    }
//}