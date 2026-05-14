using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class SalesControl : UserControl
    {
        public SalesControl()
        {
            InitializeComponent();
            this.Text = "Sales";
            LoadChartData();
            LoadPieChartData();
            LoadTopProductsData();
        }

        private void LoadChartData()
        {
            try
            {
                // Clear existing data
                chartRevenueTrend.Series.Clear();

                // Revenue Series
                Series revenueSeries = new Series("Revenue");
                revenueSeries.ChartType = SeriesChartType.Line;
                revenueSeries.Color = Color.FromArgb(33, 150, 243);
                revenueSeries.BorderWidth = 2;

                // Profit Series
                Series profitSeries = new Series("Profit");
                profitSeries.ChartType = SeriesChartType.Line;
                profitSeries.Color = Color.FromArgb(76, 175, 80);
                profitSeries.BorderWidth = 2;

                // Data
                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun" };
                double[] revenue = { 125000, 142000, 168000, 159000, 183000, 195000 };
                double[] profit = { 31250, 35500, 42000, 39750, 45750, 48750 };

                for (int i = 0; i < months.Length; i++)
                {
                    revenueSeries.Points.AddXY(months[i], revenue[i]);
                    profitSeries.Points.AddXY(months[i], profit[i]);
                }

                chartRevenueTrend.Series.Add(revenueSeries);
                chartRevenueTrend.Series.Add(profitSeries);
                chartRevenueTrend.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart error: {ex.Message}");
            }
        }

        private void LoadPieChartData()
        {
            try
            {
                chartSalesByCategory.Series.Clear();

                Series pieSeries = new Series("Sales by Category");
                pieSeries.ChartType = SeriesChartType.Pie;

                pieSeries.Points.AddXY("Polos", 31125);
                pieSeries.Points.AddXY("Trousers", 31220);
                pieSeries.Points.AddXY("Dresses", 26700);
                pieSeries.Points.AddXY("Accessories", 12450);
                pieSeries.Points.AddXY("Alterations", 8900);

                pieSeries.IsValueShownAsLabel = true;
                pieSeries.Label = "#PERCENT{P0}";
                pieSeries.LabelForeColor = Color.White;

                chartSalesByCategory.Series.Add(pieSeries);
                chartSalesByCategory.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pie Chart error: {ex.Message}");
            }
        }

        private void LoadTopProductsData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Product", typeof(string));
                dt.Columns.Add("Units Sold", typeof(int));
                dt.Columns.Add("Revenue", typeof(string));

                dt.Rows.Add("Classic Polo Shirt", 1245, "$31,125");
                dt.Rows.Add("Slim Fit Jeans", 892, "$31,220");
                dt.Rows.Add("Summer Dress", 534, "$26,700");
                dt.Rows.Add("Leather Belt", 789, "$7,890");
                dt.Rows.Add("Wool Scarf", 423, "$6,345");

                dgvTopProducts.DataSource = dt;
                dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DataGridView error: {ex.Message}");
            }
        }
    }
}