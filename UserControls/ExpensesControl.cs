using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class ExpensesControl : UserControl
    {
        public ExpensesControl()
        {
            InitializeComponent();
            this.Text = "Expenses";
            LoadExpenseData();
        }

        private void LoadExpenseData()
        {
            lblTotalExpenses.Text = "$12,450.00";
            lblExpenseTrend.Text = "↑ 5% vs last month";

            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Amount", typeof(string));

            dt.Rows.Add("05/14/2024", "Thread - Red", "Supplies", "$150.00");
            dt.Rows.Add("05/13/2024", "Tailor Scissors", "Equipment", "$75.00");
            dt.Rows.Add("05/12/2024", "Electricity Bill", "Utilities", "$250.00");
            dt.Rows.Add("05/11/2024", "Monthly Rent", "Rent", "$1,500.00");
            dt.Rows.Add("05/10/2024", "Staff Salary", "Salaries", "$2,000.00");
            dt.Rows.Add("05/09/2024", "Facebook Ads", "Marketing", "$100.00");

            dgvExpenseHistory.DataSource = dt;
            dgvExpenseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!dgvExpenseHistory.Columns.Contains("Actions"))
            {
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                editColumn.Name = "Actions";
                editColumn.HeaderText = "Actions";
                editColumn.Text = "✏️ Edit";
                editColumn.UseColumnTextForButtonValue = true;
                dgvExpenseHistory.Columns.Add(editColumn);
            }

            dgvExpenseHistory.BackgroundColor = Color.FromArgb(37, 36, 81);
            dgvExpenseHistory.ForeColor = Color.White;
            dgvExpenseHistory.GridColor = Color.FromArgb(90, 88, 140);
            dgvExpenseHistory.EnableHeadersVisualStyles = false;
            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
            dgvExpenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnCreateExpense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
            {
                MessageBox.Show("Please enter expense name!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter amount!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Expense created: {txtExpenseName.Text}\nAmount: ${txtAmount.Text}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtExpenseName.Text = "";
            txtAmount.Text = "";
            cmbCategory.SelectedIndex = -1;
            dtpExpenseDate.Value = DateTime.Now;
        }

        private void btnClearExpense_Click(object sender, EventArgs e)
        {
            txtExpenseName.Text = "";
            txtAmount.Text = "";
            cmbCategory.SelectedIndex = -1;
            dtpExpenseDate.Value = DateTime.Now;
        }

        private void dgvExpenseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is valid
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Check if the clicked column is the Actions column
            if (dgvExpenseHistory.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Safely get the description
                DataGridViewRow row = dgvExpenseHistory.Rows[e.RowIndex];
                string description = "this expense";

                if (row.Cells["Description"].Value != null)
                {
                    description = row.Cells["Description"].Value.ToString();
                }

                MessageBox.Show($"Action for: {description}", "Expense Action",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}