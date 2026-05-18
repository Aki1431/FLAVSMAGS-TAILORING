using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FLAVSMAGS_TAILORING.Services;
using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class SalesControl : UserControl
    {
        private readonly SalesService _salesService;
        // Chart animation timers
        private System.Windows.Forms.Timer lineChartTimer = null!;
        private System.Windows.Forms.Timer pieChartTimer = null!;
        private Queue<KeyValuePair<string, decimal>> lineDataQueue = new Queue<KeyValuePair<string, decimal>>();
        private Queue<KeyValuePair<string, decimal>> pieDataQueue = new Queue<KeyValuePair<string, decimal>>();
        private Series? lineSeries;
        private Series? pieSeries;

        public SalesControl()
        {
            InitializeComponent();
            this.Text = "Sales";
            _salesService = new SalesService();
            _salesService.DataChanged += OnDataChanged;
            SetupChartAnimations();
            LoadSalesData();
        }

        private void SetupChartAnimations()
        {
            // Line chart animation
            lineChartTimer = new System.Windows.Forms.Timer { Interval = 50 };
            lineChartTimer.Tick += LineChartTimer_Tick;
            // Pie chart animation
            pieChartTimer = new System.Windows.Forms.Timer { Interval = 100 };
            pieChartTimer.Tick += PieChartTimer_Tick;
        }

        private void OnDataChanged(object? sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)LoadSalesData);
            else LoadSalesData();
        }

        private void LoadSalesData()
        {
            LoadStatCards();
            LoadLineChartData();
            LoadPieChartData();
            LoadTopProductsCards();
        }

        private void LoadStatCards()
        {
            var stats = _salesService.GetStatistics();
            lblTotalSales.Text = $"₱{stats.TotalSales:N0}";
            string sign = stats.RevenueGrowth >= 0 ? "↑" : "↓";
            lblSalesTrend.Text = $"{sign} {Math.Abs(stats.RevenueGrowth):F1}%";
            lblSalesTrend.ForeColor = stats.RevenueGrowth >= 0 ? Color.LightGreen : Color.Red;

            lblRevenue.Text = $"₱{stats.TotalSales:N0}"; // same metric for simplicity
            lblRevenueTrend.Text = lblSalesTrend.Text;
            lblRevenueTrend.ForeColor = lblSalesTrend.ForeColor;

            lblAvgOrder.Text = $"₱{stats.AverageOrderValue:N2}";
            lblAvgOrderTrend.Text = "↑ 5.2%"; // placeholder
            lblAvgOrderTrend.ForeColor = Color.LightGreen;

            lblProfit.Text = $"₱{stats.TotalProfit:N0}";
        }

        // ========== LINE CHART (Revenue & Profit) ==========
        private void LoadLineChartData()
        {
            chartRevenueTrend.Series.Clear();
            chartRevenueTrend.ChartAreas.Clear();
            ChartArea area = new ChartArea
            {
                BackColor = Color.FromArgb(30, 30, 40),
                AxisX = { LabelStyle = { ForeColor = Color.White } },
                AxisY = { LabelStyle = { ForeColor = Color.White } }
            };
            chartRevenueTrend.ChartAreas.Add(area);

            lineSeries = new Series("Revenue") { ChartType = SeriesChartType.Line, Color = Color.FromArgb(33, 150, 243), BorderWidth = 3 };
            chartRevenueTrend.Series.Add(lineSeries);

            var revenueTrend = _salesService.GetRevenueTrend();
            lineDataQueue.Clear();
            foreach (var kvp in revenueTrend)
                lineDataQueue.Enqueue(kvp);
            lineChartTimer.Start();
        }

        private void LineChartTimer_Tick(object? sender, EventArgs e)
        {
            if (lineDataQueue.Count == 0)
            {
                lineChartTimer.Stop();
                return;
            }
            var point = lineDataQueue.Dequeue();
            lineSeries?.Points.AddXY(point.Key, point.Value);
            chartRevenueTrend.Invalidate();
        }

        // ========== PIE CHART (Sales by Category) ==========
        private void LoadPieChartData()
        {
            chartSalesByCategory.Series.Clear();
            chartSalesByCategory.ChartAreas.Clear();
            ChartArea area = new ChartArea { BackColor = Color.FromArgb(30, 30, 40) };
            chartSalesByCategory.ChartAreas.Add(area);

            pieSeries = new Series("Sales") { ChartType = SeriesChartType.Pie };
            chartSalesByCategory.Series.Add(pieSeries);

            var stats = _salesService.GetStatistics();
            var salesByCategory = stats.SalesByCategory;
            pieDataQueue.Clear();
            foreach (var cat in salesByCategory)
                pieDataQueue.Enqueue(cat);
            pieChartTimer.Start();
        }

        private void PieChartTimer_Tick(object? sender, EventArgs e)
        {
            if (pieDataQueue.Count == 0)
            {
                pieChartTimer.Stop();
                ApplyPieColorsAndLabels();
                return;
            }
            var point = pieDataQueue.Dequeue();
            pieSeries?.Points.AddXY(point.Key, point.Value);
            chartSalesByCategory.Invalidate();
        }

        private void ApplyPieColorsAndLabels()
        {
            if (pieSeries == null) return;
            decimal total = _salesService.GetStatistics().SalesByCategory.Values.Sum();
            Color[] colors = { Color.FromArgb(76, 175, 80), Color.FromArgb(33, 150, 243), Color.FromArgb(156, 39, 176), Color.FromArgb(255, 152, 0), Color.FromArgb(244, 67, 54) };
            for (int i = 0; i < pieSeries.Points.Count; i++)
            {
                pieSeries.Points[i].Color = colors[i % colors.Length];
                if (total > 0)
                    pieSeries.Points[i].LegendText = $"{pieSeries.Points[i].AxisLabel} ({(decimal)pieSeries.Points[i].YValues[0] / total * 100:F0}%)";
            }
            pieSeries.IsValueShownAsLabel = true;
            pieSeries.Label = "#PERCENT{P0}";
            pieSeries.LabelForeColor = Color.White;
            pieSeries.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            chartSalesByCategory.Invalidate();
        }

        // ========== TOP PRODUCTS CARDS ==========
        private void LoadTopProductsCards()
        {
            flowTopProducts.Controls.Clear();
            var stats = _salesService.GetStatistics();
            var topProducts = stats.TopProducts;

            foreach (var product in topProducts)
            {
                Guna2Panel card = new Guna2Panel
                {
                    Width = flowTopProducts.Width - 25,
                    Height = 55,
                    BackColor = Color.Transparent,
                    FillColor = Color.FromArgb(35, 35, 55),
                    BorderRadius = 8,
                    Margin = new Padding(5),
                    ShadowDecoration = { Enabled = true, Depth = 3 }
                };

                Label lblName = new Label { Text = product.ProductName, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 5), AutoSize = true };
                Label lblUnits = new Label { Text = $"Units Sold: {product.UnitsSold}", Font = new Font("Segoe UI", 8), ForeColor = Color.LightGray, Location = new Point(10, 30), AutoSize = true };
                Label lblRevenue = new Label { Text = $"Revenue: ₱{product.Revenue:N2}", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(250, 5), AutoSize = true };

                card.Controls.AddRange(new Control[] { lblName, lblUnits, lblRevenue });
                flowTopProducts.Controls.Add(card);
            }
        }
    }
}

//using System;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;
//using FLAVSMAGS_TAILORING.Services;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class SalesControl : UserControl
//    {
//        private readonly SalesService _salesService;

//        public SalesControl()
//        {
//            InitializeComponent();
//            this.Text = "Sales";

//            // Initialize service
//            _salesService = new SalesService();

//            // Subscribe to data changes
//            _salesService.DataChanged += OnDataChanged;

//            // Load initial data
//            LoadSalesData();
//        }

//        /// <summary>
//        /// Event handler for data changes - refresh all sales data
//        /// </summary>
//        private void OnDataChanged(object? sender, EventArgs e)
//        {
//            LoadSalesData();
//        }

//        /// <summary>
//        /// Load all sales data from service
//        /// </summary>
//        private void LoadSalesData()
//        {
//            try
//            {
//                LoadStatCards();
//                LoadChartData();
//                LoadPieChartData();
//                LoadTopProductsData();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading sales data: {ex.Message}",
//                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load real-time statistics into stat cards
//        /// </summary>
//        private void LoadStatCards()
//        {
//            var stats = _salesService.GetStatistics();

//            // Total Sales
//            lblTotalSales.Text = $"${stats.TotalSales:N0}";
//            lblSalesTrend.Text = $"↑ {stats.RevenueGrowth:F1}%";
//            lblSalesTrend.ForeColor = stats.RevenueGrowth >= 0 ? Color.LightGreen : Color.Red;

//            // Revenue (same as total sales for now)
//            lblRevenue.Text = $"${stats.TotalSales:N0}";
//            lblRevenueTrend.Text = $"↑ {stats.RevenueGrowth:F1}%";
//            lblRevenueTrend.ForeColor = stats.RevenueGrowth >= 0 ? Color.LightGreen : Color.Red;

//            // Average Order Value
//            lblAvgOrder.Text = $"${stats.AverageOrderValue:F2}";
//            lblAvgOrderTrend.Text = "↑ 5.2%"; // Placeholder

//            // Conversion Rate
//            lblConversion.Text = $"{stats.ConversionRate:F1}%";
//        }

//        /// <summary>
//        /// Load revenue and profit trend chart (last 6 months)
//        /// </summary>
//        private void LoadChartData()
//        {
//            try
//            {
//                chartRevenueTrend.Series.Clear();

//                // Revenue Series
//                Series revenueSeries = new Series("Revenue");
//                revenueSeries.ChartType = SeriesChartType.Line;
//                revenueSeries.Color = Color.FromArgb(33, 150, 243);
//                revenueSeries.BorderWidth = 3;

//                // Profit Series
//                Series profitSeries = new Series("Profit");
//                profitSeries.ChartType = SeriesChartType.Line;
//                profitSeries.Color = Color.FromArgb(76, 175, 80);
//                profitSeries.BorderWidth = 3;

//                // Get real data from service
//                var revenueTrend = _salesService.GetRevenueTrend();
//                var profitTrend = _salesService.GetProfitTrend();

//                foreach (var kvp in revenueTrend)
//                {
//                    revenueSeries.Points.AddXY(kvp.Key, kvp.Value);
//                }

//                foreach (var kvp in profitTrend)
//                {
//                    profitSeries.Points.AddXY(kvp.Key, kvp.Value);
//                }

//                chartRevenueTrend.Series.Add(revenueSeries);
//                chartRevenueTrend.Series.Add(profitSeries);

//                // Chart styling
//                if (chartRevenueTrend.ChartAreas.Count > 0)
//                {
//                    chartRevenueTrend.ChartAreas[0].BackColor = Color.FromArgb(26, 25, 62);
//                    chartRevenueTrend.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
//                    chartRevenueTrend.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
//                    chartRevenueTrend.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 100);
//                    chartRevenueTrend.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 100);
//                }

//                // Title
//                chartRevenueTrend.Titles.Clear();
//                Title title = new Title("Revenue & Profit Trend");
//                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
//                title.ForeColor = Color.White;
//                chartRevenueTrend.Titles.Add(title);

//                // Legend
//                if (chartRevenueTrend.Legends.Count > 0)
//                {
//                    chartRevenueTrend.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
//                    chartRevenueTrend.Legends[0].ForeColor = Color.White;
//                }

//                chartRevenueTrend.Invalidate();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Chart error: {ex.Message}", "Chart Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load sales distribution pie chart by category
//        /// </summary>
//        private void LoadPieChartData()
//        {
//            try
//            {
//                chartSalesByCategory.Series.Clear();

//                var stats = _salesService.GetStatistics();
//                var salesByCategory = stats.SalesByCategory;

//                if (!salesByCategory.Any())
//                {
//                    // No data available
//                    return;
//                }

//                Series pieSeries = new Series("Sales by Category");
//                pieSeries.ChartType = SeriesChartType.Pie;

//                // Color palette
//                Color[] colors = new Color[]
//                {
//                    Color.FromArgb(76, 175, 80),   // Green
//                    Color.FromArgb(33, 150, 243),  // Blue
//                    Color.FromArgb(156, 39, 176),  // Purple
//                    Color.FromArgb(255, 152, 0),   // Orange
//                    Color.FromArgb(244, 67, 54)    // Red
//                };

//                int colorIndex = 0;
//                foreach (var category in salesByCategory)
//                {
//                    pieSeries.Points.AddXY(category.Key, category.Value);
//                    pieSeries.Points[pieSeries.Points.Count - 1].Color =
//                        colors[colorIndex % colors.Length];
//                    colorIndex++;
//                }

//                pieSeries.IsValueShownAsLabel = true;
//                pieSeries.Label = "#PERCENT{P0}";
//                pieSeries.LabelForeColor = Color.White;
//                pieSeries.Font = new Font("Segoe UI", 10, FontStyle.Bold);

//                chartSalesByCategory.Series.Add(pieSeries);

//                // Chart styling
//                if (chartSalesByCategory.ChartAreas.Count > 0)
//                {
//                    chartSalesByCategory.ChartAreas[0].BackColor = Color.FromArgb(90, 88, 140);
//                }

//                // Title
//                chartSalesByCategory.Titles.Clear();
//                Title title = new Title("Sales by Category");
//                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
//                title.ForeColor = Color.White;
//                chartSalesByCategory.Titles.Add(title);

//                // Legend
//                if (chartSalesByCategory.Legends.Count > 0)
//                {
//                    chartSalesByCategory.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
//                    chartSalesByCategory.Legends[0].ForeColor = Color.White;
//                }

//                chartSalesByCategory.Invalidate();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Pie Chart error: {ex.Message}", "Chart Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Load top performing products into grid
//        /// </summary>
//        private void LoadTopProductsData()
//        {
//            try
//            {
//                var stats = _salesService.GetStatistics();
//                var topProducts = stats.TopProducts;

//                DataTable dt = new DataTable();
//                dt.Columns.Add("Product", typeof(string));
//                dt.Columns.Add("Units Sold", typeof(int));
//                dt.Columns.Add("Revenue", typeof(string));

//                foreach (var product in topProducts)
//                {
//                    dt.Rows.Add(
//                        product.ProductName,
//                        product.UnitsSold,
//                        $"${product.Revenue:N2}"
//                    );
//                }

//                dgvTopProducts.DataSource = dt;
//                dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//                // Style the grid
//                dgvTopProducts.BackgroundColor = Color.FromArgb(37, 36, 81);
//                dgvTopProducts.ForeColor = Color.White;
//                dgvTopProducts.GridColor = Color.FromArgb(90, 88, 140);
//                dgvTopProducts.EnableHeadersVisualStyles = false;
//                dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 88, 140);
//                dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"DataGridView error: {ex.Message}", "Grid Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // Keep existing event handlers for Designer compatibility
//        private void lblConversion_Click(object sender, EventArgs e) { }
//    }
//}