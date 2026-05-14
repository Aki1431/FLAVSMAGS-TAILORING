using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class OrdersControl : UserControl
    {
        public OrdersControl()
        {
            InitializeComponent();
            this.Text = "Orders";
            LoadActiveOrders();
        }

        private void LoadActiveOrders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order ID", typeof(int));
            dt.Columns.Add("Customer Name", typeof(string));
            dt.Columns.Add("Items", typeof(string));
            dt.Columns.Add("Total Amount", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Order Date", typeof(string));

            dt.Rows.Add(1001, "Scooby Jew", "2 pcs", "$150.00", "Pending", "2024-05-14");
            dt.Rows.Add(1002, "Adolf Rizzler", "3 pcs", "$230.00", "Processing", "2024-05-13");
            dt.Rows.Add(1003, "Nick Gher", "1 pc", "$75.00", "Ready", "2024-05-13");

            dgvActiveOrders.DataSource = dt;
            dgvActiveOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer name!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Order created for {txtCustomer.Text}!\nItems: {txtItems.Text}\nTotal: ${txtTotal.Text}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearForm();
            LoadActiveOrders();
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtCustomer.Clear();
            txtItems.Clear();
            txtTotal.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadActiveOrders();
            }
            else
            {
                MessageBox.Show($"Searching for: {txtSearch.Text}");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panelCreateOrder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}