using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FLAVSMAGS_TAILORING.Services;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class SalesControl : UserControl
    {
        private readonly SalesService _salesService;

        public SalesControl()
        {
            InitializeComponent();
            this.Text = "Sales";

            // Initialize service
            _salesService = new SalesService();

            // Subscribe to data changes
            _salesService.DataChanged += OnDataChanged;

            // Load initial data
            LoadSalesData();
        }

        /// <summary>
        /// Event handler for data changes - refresh all sales data
        /// </summary>
        private void OnDataChanged(object? sender, EventArgs e)
        {
            LoadSalesData();
        }

        /// <summary>
        /// Load all sales data from service
        /// </summary>
        private void LoadSalesData()
        {
            try
            {
                LoadStatCards();
                LoadChartData();
                LoadPieChartData();
                LoadTopProductsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales data: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load real-time statistics into stat cards
        /// </summary>
        private void LoadStatCards()
        {
            var stats = _salesService.GetStatistics();

            // Total Sales
            lblTotalSales.Text = $"${stats.TotalSales:N0}";
            lblSalesTrend.Text = $"↑ {stats.RevenueGrowth:F1}%";
            lblSalesTrend.ForeColor = stats.RevenueGrowth >= 0 ? Color.LightGreen : Color.Red;

            // Revenue (same as total sales for now)
            lblRevenue.Text = $"${stats.TotalSales:N0}";
            lblRevenueTrend.Text = $"↑ {stats.RevenueGrowth:F1}%";
            lblRevenueTrend.ForeColor = stats.RevenueGrowth >= 0 ? Color.LightGreen : Color.Red;

            // Average Order Value
            lblAvgOrder.Text = $"${stats.AverageOrderValue:F2}";
            lblAvgOrderTrend.Text = "↑ 5.2%"; // Placeholder

            // Conversion Rate
            lblConversion.Text = $"{stats.ConversionRate:F1}%";
        }

        /// <summary>
        /// Load revenue and profit trend chart (last 6 months)
        /// </summary>
        private void LoadChartData()
        {
            try
            {
                chartRevenueTrend.Series.Clear();

                // Revenue Series
                Series revenueSeries = new Series("Revenue");
                revenueSeries.ChartType = SeriesChartType.Line;
                revenueSeries.Color = Color.FromArgb(33, 150, 243);
                revenueSeries.BorderWidth = 3;

                // Profit Series
                Series profitSeries = new Series("Profit");
                profitSeries.ChartType = SeriesChartType.Line;
                profitSeries.Color = Color.FromArgb(76, 175, 80);
                profitSeries.BorderWidth = 3;

                // Get real data from service
                var revenueTrend = _salesService.GetRevenueTrend();
                var profitTrend = _salesService.GetProfitTrend();

                foreach (var kvp in revenueTrend)
                {
                    revenueSeries.Points.AddXY(kvp.Key, kvp.Value);
                }

                foreach (var kvp in profitTrend)
                {
                    profitSeries.Points.AddXY(kvp.Key, kvp.Value);
                }

                chartRevenueTrend.Series.Add(revenueSeries);
                chartRevenueTrend.Series.Add(profitSeries);

                // Chart styling
                if (chartRevenueTrend.ChartAreas.Count > 0)
                {
                    chartRevenueTrend.ChartAreas[0].BackColor = Color.FromArgb(26, 25, 62);
                    chartRevenueTrend.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
                    chartRevenueTrend.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
                    chartRevenueTrend.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 100);
                    chartRevenueTrend.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 100);
                }

                // Title
                chartRevenueTrend.Titles.Clear();
                Title title = new Title("Revenue & Profit Trend");
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = Color.White;
                chartRevenueTrend.Titles.Add(title);

                // Legend
                if (chartRevenueTrend.Legends.Count > 0)
                {
                    chartRevenueTrend.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
                    chartRevenueTrend.Legends[0].ForeColor = Color.White;
                }

                chartRevenueTrend.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart error: {ex.Message}", "Chart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load sales distribution pie chart by category
        /// </summary>
        private void LoadPieChartData()
        {
            try
            {
                chartSalesByCategory.Series.Clear();

                var stats = _salesService.GetStatistics();
                var salesByCategory = stats.SalesByCategory;

                if (!salesByCategory.Any())
                {
                    // No data available
                    return;
                }

                Series pieSeries = new Series("Sales by Category");
                pieSeries.ChartType = SeriesChartType.Pie;

                // Color palette
                Color[] colors = new Color[]
                {
                    Color.FromArgb(76, 175, 80),   // Green
                    Color.FromArgb(33, 150, 243),  // Blue
                    Color.FromArgb(156, 39, 176),  // Purple
                    Color.FromArgb(255, 152, 0),   // Orange
                    Color.FromArgb(244, 67, 54)    // Red
                };

                int colorIndex = 0;
                foreach (var category in salesByCategory)
                {
                    pieSeries.Points.AddXY(category.Key, category.Value);
                    pieSeries.Points[pieSeries.Points.Count - 1].Color =
                        colors[colorIndex % colors.Length];
                    colorIndex++;
                }

                pieSeries.IsValueShownAsLabel = true;
                pieSeries.Label = "#PERCENT{P0}";
                pieSeries.LabelForeColor = Color.White;
                pieSeries.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                chartSalesByCategory.Series.Add(pieSeries);

                // Chart styling
                if (chartSalesByCategory.ChartAreas.Count > 0)
                {
                    chartSalesByCategory.ChartAreas[0].BackColor = Color.FromArgb(90, 88, 140);
                }

                // Title
                chartSalesByCategory.Titles.Clear();
                Title title = new Title("Sales by Category");
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = Color.White;
                chartSalesByCategory.Titles.Add(title);

                // Legend
                if (chartSalesByCategory.Legends.Count > 0)
                {
                    chartSalesByCategory.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
                    chartSalesByCategory.Legends[0].ForeColor = Color.White;
                }

                chartSalesByCategory.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pie Chart error: {ex.Message}", "Chart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load top performing products into grid
        /// </summary>
        private void LoadTopProductsData()
        {
            try
            {
                var stats = _salesService.GetStatistics();
                var topProducts = stats.TopProducts;

                DataTable dt = new DataTable();
                dt.Columns.Add("Product", typeof(string));
                dt.Columns.Add("Units Sold", typeof(int));
                dt.Columns.Add("Revenue", typeof(string));

                foreach (var product in topProducts)
                {
                    dt.Rows.Add(
                        product.ProductName,
                        product.UnitsSold,
                        $"${product.Revenue:N2}"
                    );
                }

                dgvTopProducts.DataSource = dt;
                dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Style the grid
                dgvTopProducts.BackgroundColor = Color.FromArgb(37, 36, 81);
                dgvTopProducts.ForeColor = Color.White;
                dgvTopProducts.GridColor = Color.FromArgb(90, 88, 140);
                dgvTopProducts.EnableHeadersVisualStyles = false;
                dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
                dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DataGridView error: {ex.Message}", "Grid Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Keep existing event handlers for Designer compatibility
        private void lblConversion_Click(object sender, EventArgs e) { }
    }
}



//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class SalesControl : UserControl
//    {
//        public SalesControl()
//        {
//            InitializeComponent();
//            this.Text = "Sales";
//            LoadChartData();
//            LoadPieChartData();
//            LoadTopProductsData();
//        }

//        private void LoadChartData()
//        {
//            try
//            {
//                // Clear existing data
//                chartRevenueTrend.Series.Clear();

//                // Revenue Series
//                Series revenueSeries = new Series("Revenue");
//                revenueSeries.ChartType = SeriesChartType.Line;
//                revenueSeries.Color = Color.FromArgb(33, 150, 243);
//                revenueSeries.BorderWidth = 2;

//                // Profit Series
//                Series profitSeries = new Series("Profit");
//                profitSeries.ChartType = SeriesChartType.Line;
//                profitSeries.Color = Color.FromArgb(76, 175, 80);
//                profitSeries.BorderWidth = 2;

//                // Data
//                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun" };
//                double[] revenue = { 125000, 142000, 168000, 159000, 183000, 195000 };
//                double[] profit = { 31250, 35500, 42000, 39750, 45750, 48750 };

//                for (int i = 0; i < months.Length; i++)
//                {
//                    revenueSeries.Points.AddXY(months[i], revenue[i]);
//                    profitSeries.Points.AddXY(months[i], profit[i]);
//                }

//                chartRevenueTrend.Series.Add(revenueSeries);
//                chartRevenueTrend.Series.Add(profitSeries);
//                chartRevenueTrend.Invalidate();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Chart error: {ex.Message}");
//            }
//        }

//        private void LoadPieChartData()
//        {
//            try
//            {
//                chartSalesByCategory.Series.Clear();

//                Series pieSeries = new Series("Sales by Category");
//                pieSeries.ChartType = SeriesChartType.Pie;

//                pieSeries.Points.AddXY("Polos", 31125);
//                pieSeries.Points.AddXY("Trousers", 31220);
//                pieSeries.Points.AddXY("Dresses", 26700);
//                pieSeries.Points.AddXY("Accessories", 12450);
//                pieSeries.Points.AddXY("Alterations", 8900);

//                pieSeries.IsValueShownAsLabel = true;
//                pieSeries.Label = "#PERCENT{P0}";
//                pieSeries.LabelForeColor = Color.White;

//                chartSalesByCategory.Series.Add(pieSeries);
//                chartSalesByCategory.Invalidate();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Pie Chart error: {ex.Message}");
//            }
//        }

//        private void LoadTopProductsData()
//        {
//            try
//            {
//                DataTable dt = new DataTable();
//                dt.Columns.Add("Product", typeof(string));
//                dt.Columns.Add("Units Sold", typeof(int));
//                dt.Columns.Add("Revenue", typeof(string));

//                dt.Rows.Add("Classic Polo Shirt", 1245, "$31,125");
//                dt.Rows.Add("Slim Fit Jeans", 892, "$31,220");
//                dt.Rows.Add("Summer Dress", 534, "$26,700");
//                dt.Rows.Add("Leather Belt", 789, "$7,890");
//                dt.Rows.Add("Wool Scarf", 423, "$6,345");

//                dgvTopProducts.DataSource = dt;
//                dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"DataGridView error: {ex.Message}");
//            }
//        }
//    }
//}