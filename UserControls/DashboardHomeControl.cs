using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FLAVSMAGS_TAILORING.Services;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class DashboardHomeControl : UserControl
    {
        // Service instances
        private readonly ReportService _reportService;
        private readonly OrderService _orderService;
        private readonly SalesService _salesService;
        private readonly InventoryService _inventoryService;
        private readonly ExpenseService _expenseService;

        public DashboardHomeControl()
        {
            InitializeComponent();
            this.Text = "Dashboard";
            this.AutoScroll = false;

            // Initialize services
            _reportService = new ReportService();
            _orderService = new OrderService();
            _salesService = new SalesService();
            _inventoryService = new InventoryService();
            _expenseService = new ExpenseService();

            // Subscribe to data changes for real-time updates
            SubscribeToDataChanges();

            // Load initial data
            RefreshAllData();
        }

        /// <summary>
        /// Subscribe to all service data change events
        /// When any data changes, dashboard automatically refreshes
        /// </summary>
        private void SubscribeToDataChanges()
        {
            _orderService.DataChanged += OnDataChanged;
            _salesService.DataChanged += OnDataChanged;
            _inventoryService.DataChanged += OnDataChanged;
            _expenseService.DataChanged += OnDataChanged;
        }

        /// <summary>
        /// Event handler for data changes - refreshes all dashboard data
        /// </summary>
        private void OnDataChanged(object? sender, EventArgs e)
        {
            RefreshAllData();
        }

        /// <summary>
        /// Refresh all dashboard data from services
        /// </summary>
        private void RefreshAllData()
        {
            try
            {
                LoadStatCardsData();
                LoadChartData();
                LoadPieChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}",
                    "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load real-time statistics into stat cards
        /// </summary>
        private void LoadStatCardsData()
        {
            var metrics = _reportService.GetDashboardMetrics();

            // Today's Sale - Real data
            label2.Text = $"${metrics.TodaysSales:N2}";

            // Monthly Sale - Real data
            label4.Text = $"${metrics.MonthlySales:N2}";

            // Total Customers - Real data
            label6.Text = metrics.TotalCustomers.ToString();

            // Pending Orders - Real data
            label8.Text = metrics.PendingOrders.ToString();
        }

        /// <summary>
        /// Load financial performance chart with real revenue/expense data
        /// </summary>
        private void LoadChartData()
        {
            chart1.Series["Income"].Points.Clear();
            chart1.Series["Expenses"].Points.Clear();

            chart1.Series["Income"].ChartType = SeriesChartType.Column;
            chart1.Series["Expenses"].ChartType = SeriesChartType.Column;

            chart1.Series["Income"].Color = Color.FromArgb(76, 175, 80);
            chart1.Series["Expenses"].Color = Color.FromArgb(244, 67, 54);

            chart1.Series["Income"].IsXValueIndexed = true;
            chart1.Series["Expenses"].IsXValueIndexed = true;

            // Get real revenue trend data (last 6 months)
            var revenueTrend = _salesService.GetRevenueTrend();

            // Get real expense data by month
            var expensesByMonth = GetExpensesByMonth();

            foreach (var kvp in revenueTrend)
            {
                chart1.Series["Income"].Points.AddXY(kvp.Key, kvp.Value);

                // Add corresponding expenses
                if (expensesByMonth.ContainsKey(kvp.Key))
                {
                    chart1.Series["Expenses"].Points.AddXY(kvp.Key, expensesByMonth[kvp.Key]);
                }
                else
                {
                    chart1.Series["Expenses"].Points.AddXY(kvp.Key, 0);
                }
            }

            // Chart styling
            if (chart1.ChartAreas.Count > 0)
            {
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
                chart1.ChartAreas[0].AxisX.Title = "Month";
                chart1.ChartAreas[0].AxisY.Title = "Amount ($)";
                chart1.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
                chart1.ChartAreas[0].AxisX.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
                chart1.ChartAreas[0].BackColor = Color.FromArgb(26, 25, 62);
            }

            chart1.Series["Income"]["PointWidth"] = "0.6";
            chart1.Series["Expenses"]["PointWidth"] = "0.6";

            chart1.Series["Income"].ToolTip = "Income: $#VAL{0:N0}";
            chart1.Series["Expenses"].ToolTip = "Expenses: $#VAL{0:N0}";

            // Title
            chart1.Titles.Clear();
            Title title = new Title("Financial Performance");
            title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            title.Alignment = ContentAlignment.TopCenter;
            title.ForeColor = Color.White;
            chart1.Titles.Add(title);

            // Legend
            if (chart1.Legends.Count > 0)
            {
                chart1.Legends[0].Docking = Docking.Bottom;
                chart1.Legends[0].Font = new Font("Segoe UI", 9);
                chart1.Legends[0].BackColor = Color.FromArgb(26, 25, 62);
                chart1.Legends[0].ForeColor = Color.White;
                chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
            }

            chart1.Invalidate();
            chart1.Update();
        }

        /// <summary>
        /// Get expenses grouped by month
        /// </summary>
        private Dictionary<string, decimal> GetExpensesByMonth()
        {
            var result = new Dictionary<string, decimal>();
            var allExpenses = _expenseService.GetAllExpenses();

            for (int i = 5; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var monthName = date.ToString("MMM");

                var monthExpenses = allExpenses
                    .Where(e => e.ExpenseDate.Year == date.Year && e.ExpenseDate.Month == date.Month)
                    .Sum(e => e.Amount);

                result[monthName] = monthExpenses;
            }

            return result;
        }

        /// <summary>
        /// Load real sales distribution pie chart
        /// </summary>
        private void LoadPieChartData()
        {
            if (chart2 == null) return;

            chart2.Series.Clear();

            var salesStats = _salesService.GetStatistics();
            var salesByCategory = salesStats.SalesByCategory;

            if (!salesByCategory.Any())
            {
                // No data available
                return;
            }

            Series pieSeries = new Series("Profit Distribution");
            pieSeries.ChartType = SeriesChartType.Pie;

            // Calculate total
            decimal total = salesByCategory.Values.Sum();

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
                // Add data point
                var point = pieSeries.Points.AddXY(category.Key, category.Value);

                // Set legend text with percentage
                decimal percentage = (category.Value / total) * 100;
                pieSeries.Points[pieSeries.Points.Count - 1].LegendText =
                    $"{category.Key} - {percentage:F0}%";

                // Set color
                pieSeries.Points[pieSeries.Points.Count - 1].Color =
                    colors[colorIndex % colors.Length];

                colorIndex++;
            }

            // Show percentages on slices
            pieSeries.IsValueShownAsLabel = true;
            pieSeries.Label = "#PERCENT{P0}";
            pieSeries.LabelForeColor = Color.White;
            pieSeries.LabelBackColor = Color.Transparent;
            pieSeries.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            chart2.Series.Add(pieSeries);

            // Chart Area Background
            if (chart2.ChartAreas.Count > 0)
            {
                chart2.ChartAreas[0].BackColor = Color.FromArgb(90, 88, 140);
            }

            // Chart Title
            chart2.Titles.Clear();
            Title title = new Title("Sales Distribution by Category");
            title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            title.Alignment = ContentAlignment.TopCenter;
            title.ForeColor = Color.White;
            chart2.Titles.Add(title);

            // Legend
            if (chart2.Legends.Count > 0)
            {
                chart2.Legends[0].Docking = Docking.Bottom;
                chart2.Legends[0].Font = new Font("Segoe UI", 9);
                chart2.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
                chart2.Legends[0].ForeColor = Color.White;
                chart2.Legends[0].TableStyle = LegendTableStyle.Wide;
            }
            else
            {
                Legend legend = new Legend();
                legend.Docking = Docking.Bottom;
                legend.BackColor = Color.FromArgb(90, 88, 140);
                legend.ForeColor = Color.White;
                legend.TableStyle = LegendTableStyle.Wide;
                chart2.Legends.Add(legend);
            }

            chart2.Invalidate();
            chart2.Update();
        }

        // Keep existing empty event handlers for Designer compatibility
        private void label2_Click(object sender, EventArgs e) { }
        private void panelCreateOrder_Paint(object sender, PaintEventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void chart1_Click(object sender, EventArgs e) { }
        private void DashboardHomeControl_Load(object sender, EventArgs e) { }
        private void chart2_Click(object sender, EventArgs e) { }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
        private void MonthlySale_Paint(object sender, PaintEventArgs e) { }
        private void TotalCustomer_Paint(object sender, PaintEventArgs e) { }
        private void PendingCustomers_Paint(object sender, PaintEventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
    }
}


//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    public partial class DashboardHomeControl : UserControl
//    {
//        public DashboardHomeControl()
//        {
//            InitializeComponent();
//            this.Text = "Dashboard";
//            this.AutoScroll = false;
//            LoadChartData();
//            LoadPieChartData();
//            LoadStatCardsData();
//        }

//        private void label2_Click(object sender, EventArgs e)
//        {
//            // Empty - required for Designer
//        }

//        private void panelCreateOrder_Paint(object sender, PaintEventArgs e)
//        {
//            // Empty - required for Designer
//        }

//        private void label6_Click(object sender, EventArgs e)
//        {
//            // Empty - required for Designer
//        }

//        private void LoadChartData()
//        {
//            chart1.Series["Income"].Points.Clear();
//            chart1.Series["Expenses"].Points.Clear();

//            chart1.Series["Income"].ChartType = SeriesChartType.Column;
//            chart1.Series["Expenses"].ChartType = SeriesChartType.Column;

//            chart1.Series["Income"].Color = Color.FromArgb(76, 175, 80);
//            chart1.Series["Expenses"].Color = Color.FromArgb(244, 67, 54);

//            chart1.Series["Income"].IsXValueIndexed = true;
//            chart1.Series["Expenses"].IsXValueIndexed = true;

//            chart1.Series["Income"].Points.AddXY("Jan", 12500);
//            chart1.Series["Expenses"].Points.AddXY("Jan", 8900);
//            chart1.Series["Income"].Points.AddXY("Feb", 14200);
//            chart1.Series["Expenses"].Points.AddXY("Feb", 9500);
//            chart1.Series["Income"].Points.AddXY("Mar", 16800);
//            chart1.Series["Expenses"].Points.AddXY("Mar", 10200);
//            chart1.Series["Income"].Points.AddXY("Apr", 15900);
//            chart1.Series["Expenses"].Points.AddXY("Apr", 9800);
//            chart1.Series["Income"].Points.AddXY("May", 18300);
//            chart1.Series["Expenses"].Points.AddXY("May", 11200);
//            chart1.Series["Income"].Points.AddXY("Jun", 19500);
//            chart1.Series["Expenses"].Points.AddXY("Jun", 10800);

//            if (chart1.ChartAreas.Count > 0)
//            {
//                chart1.ChartAreas[0].AxisX.Interval = 1;
//                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
//                chart1.ChartAreas[0].AxisX.Title = "Month";
//                chart1.ChartAreas[0].AxisY.Title = "Amount ($)";
//                chart1.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
//                chart1.ChartAreas[0].AxisX.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
//                chart1.ChartAreas[0].BackColor = Color.FromArgb(26, 25, 62);
//            }

//            chart1.Series["Income"]["PointWidth"] = "0.6";
//            chart1.Series["Expenses"]["PointWidth"] = "0.6";

//            chart1.Series["Income"].IsValueShownAsLabel = false;
//            chart1.Series["Expenses"].IsValueShownAsLabel = false;

//            chart1.Series["Income"].ToolTip = "Income: $#VAL{0:N0}";
//            chart1.Series["Expenses"].ToolTip = "Expenses: $#VAL{0:N0}";

//            chart1.Titles.Clear();
//            Title title = new Title("Financial Performance");
//            title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
//            title.Alignment = ContentAlignment.TopCenter;
//            title.ForeColor = Color.White;
//            chart1.Titles.Add(title);

//            // Legend at BOTTOM for bar chart
//            if (chart1.Legends.Count > 0)
//            {
//                chart1.Legends[0].Docking = Docking.Bottom;
//                chart1.Legends[0].Font = new Font("Segoe UI", 9);
//                chart1.Legends[0].BackColor = Color.FromArgb(26, 25, 62);
//                chart1.Legends[0].ForeColor = Color.White;
//                chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
//            }

//            chart1.Invalidate();
//            chart1.Update();
//        }

//        private void LoadPieChartData()
//        {
//            if (chart2 == null) return;

//            chart2.Series.Clear();

//            Series pieSeries = new Series("Profit Distribution");
//            pieSeries.ChartType = SeriesChartType.Pie;

//            // Add data points
//            pieSeries.Points.AddXY("Shirts", 12500);
//            pieSeries.Points.AddXY("Trousers", 9800);
//            pieSeries.Points.AddXY("Dresses", 15400);
//            pieSeries.Points.AddXY("Accessories", 4200);
//            pieSeries.Points.AddXY("Alterations", 3100);

//            // Calculate total for percentages
//            double total = 12500 + 9800 + 15400 + 4200 + 3100;

//            // Set custom legend text with percentages
//            pieSeries.Points[0].LegendText = $"Shirts - {((12500 / total) * 100):F0}%";
//            pieSeries.Points[1].LegendText = $"Trousers - {((9800 / total) * 100):F0}%";
//            pieSeries.Points[2].LegendText = $"Dresses - {((15400 / total) * 100):F0}%";
//            pieSeries.Points[3].LegendText = $"Accessories - {((4200 / total) * 100):F0}%";
//            pieSeries.Points[4].LegendText = $"Alterations - {((3100 / total) * 100):F0}%";

//            // Set custom colors for each slice
//            pieSeries.Points[0].Color = Color.FromArgb(76, 175, 80);   // Green
//            pieSeries.Points[1].Color = Color.FromArgb(33, 150, 243);  // Blue
//            pieSeries.Points[2].Color = Color.FromArgb(156, 39, 176);  // Purple
//            pieSeries.Points[3].Color = Color.FromArgb(255, 152, 0);   // Orange
//            pieSeries.Points[4].Color = Color.FromArgb(244, 67, 54);   // Red

//            // Show percentages on the slices
//            pieSeries.IsValueShownAsLabel = true;
//            pieSeries.Label = "#PERCENT{P0}";
//            pieSeries.LabelForeColor = Color.White;
//            pieSeries.LabelBackColor = Color.Transparent;
//            pieSeries.Font = new Font("Segoe UI", 9, FontStyle.Bold);

//            chart2.Series.Add(pieSeries);

//            // Chart Area Background
//            if (chart2.ChartAreas.Count > 0)
//            {
//                chart2.ChartAreas[0].BackColor = Color.FromArgb(90, 88, 140);
//            }

//            // Chart Title
//            chart2.Titles.Clear();
//            Title title = new Title("Profit Distribution by Category");
//            title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
//            title.Alignment = ContentAlignment.TopCenter;
//            title.ForeColor = Color.White;
//            chart2.Titles.Add(title);

//            // Legend at BOTTOM for pie chart
//            if (chart2.Legends.Count > 0)
//            {
//                chart2.Legends[0].Docking = Docking.Bottom;
//                chart2.Legends[0].Font = new Font("Segoe UI", 9);
//                chart2.Legends[0].BackColor = Color.FromArgb(90, 88, 140);
//                chart2.Legends[0].ForeColor = Color.White;
//                chart2.Legends[0].TableStyle = LegendTableStyle.Wide;
//            }
//            else
//            {
//                Legend legend = new Legend();
//                legend.Docking = Docking.Bottom;
//                legend.BackColor = Color.FromArgb(90, 88, 140);
//                legend.ForeColor = Color.White;
//                legend.TableStyle = LegendTableStyle.Wide;
//                chart2.Legends.Add(legend);
//            }

//            chart2.Invalidate();
//            chart2.Update();
//        }

//        private void LoadStatCardsData()
//        {
//            // Today's Sale
//            label2.Text = "$344,599";

//            // Monthly Sale  
//            label4.Text = "$1,245,899";

//            // Total Customers
//            label6.Text = "1,245";

//            // Pending Orders
//            label8.Text = "23";
//        }

//        private void panel1_Paint(object sender, PaintEventArgs e) { }
//        private void label1_Click(object sender, EventArgs e) { }
//        private void label1_Click_1(object sender, EventArgs e) { }
//        private void chart1_Click(object sender, EventArgs e) { }
//        private void DashboardHomeControl_Load(object sender, EventArgs e) { }
//        private void chart2_Click(object sender, EventArgs e) { }
//        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
//        private void MonthlySale_Paint(object sender, PaintEventArgs e) { }
//        private void TotalCustomer_Paint(object sender, PaintEventArgs e) { }
//        private void PendingCustomers_Paint(object sender, PaintEventArgs e) { }

//private void label8_Click(object sender, EventArgs e)
//{

//}
//    }
//}