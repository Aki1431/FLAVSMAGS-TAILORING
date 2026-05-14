using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class InventoryControl : UserControl
    {
        public InventoryControl()
        {
            InitializeComponent();
            this.Text = "Inventory";
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            // Load the stat cards with temporary data
            LoadStatCards();

            // Load the materials table with temporary data
            LoadMaterialsGrid();
        }

        private void LoadStatCards()
        {
            // Update the stat card labels with temporary data
            lblTotalValue.Text = "$45,250.00";
            lblActiveMaterials.Text = "156";
            lblMonthlyConsumption.Text = "$12,350.00";
        }

        private void LoadMaterialsGrid()
        {
            // Create a temporary DataTable with sample data
            DataTable dt = new DataTable();

            // Add columns
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Material Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Unit Price", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            // Add sample rows
            dt.Rows.Add("MAT-001", "Fabric - Cotton", "45 meters", "$5.00", "$225.00", "● In Stock");
            dt.Rows.Add("MAT-002", "Thread - Red", "120 pcs", "$2.00", "$240.00", "● In Stock");
            dt.Rows.Add("MAT-003", "Thread - Black", "85 pcs", "$2.00", "$170.00", "● In Stock");
            dt.Rows.Add("MAT-004", "Zipper - 20cm", "30 pcs", "$1.50", "$45.00", "⚠ Low Stock");
            dt.Rows.Add("MAT-005", "Button - Gold", "0 pcs", "$0.50", "$0.00", "🔴 Out of Stock");
            dt.Rows.Add("MAT-006", "Button - Silver", "50 pcs", "$0.50", "$25.00", "● In Stock");
            dt.Rows.Add("MAT-007", "Measuring Tape", "15 pcs", "$3.00", "$45.00", "⚠ Low Stock");
            dt.Rows.Add("MAT-008", "Scissors - Tailor", "8 pcs", "$12.00", "$96.00", "⚠ Low Stock");
            dt.Rows.Add("MAT-009", "Needle Set", "25 sets", "$4.00", "$100.00", "● In Stock");
            dt.Rows.Add("MAT-010", "Thread - White", "200 pcs", "$2.00", "$400.00", "● In Stock");

            // Assign the data to the DataGridView
            dgvMaterials.DataSource = dt;

            // Auto-fit the columns
            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Color the Status column based on the text
            dgvMaterials.CellFormatting += (sender, e) =>
            {
                if (e.ColumnIndex == dgvMaterials.Columns["Status"].Index && e.Value != null)
                {
                    string status = e.Value.ToString();
                    if (status.Contains("Low Stock"))
                        e.CellStyle.ForeColor = Color.Orange;
                    else if (status.Contains("Out of Stock"))
                        e.CellStyle.ForeColor = Color.Red;
                    else
                        e.CellStyle.ForeColor = Color.LightGreen;
                }
            };
        }
    }
}