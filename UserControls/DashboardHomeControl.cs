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
    public partial class DashboardHomeControl : UserControl
    {
        private readonly ReportService _reportService;
        private readonly OrderService _orderService;
        private readonly SalesService _salesService;
        private readonly InventoryService _inventoryService;
        private readonly ExpenseService _expenseService;

        private int currentSlideIndex = 0;
        private bool animating = false;
        private int targetX = 0;
        private System.Windows.Forms.Timer slideTimer = null!;
        private List<Control> slides = null!;

        public DashboardHomeControl()
        {
            InitializeComponent();
            this.Text = "Dashboard";
            this.AutoScroll = false;

            _reportService = new ReportService();
            _orderService = new OrderService();
            _salesService = new SalesService();
            _inventoryService = new InventoryService();
            _expenseService = new ExpenseService();

            SubscribeToDataChanges();
            SetupCarousel();
            LoadData();
        }

        private void SubscribeToDataChanges()
        {
            _orderService.DataChanged += OnDataChanged;
            _salesService.DataChanged += OnDataChanged;
            _inventoryService.DataChanged += OnDataChanged;
            _expenseService.DataChanged += OnDataChanged;
        }

        private void OnDataChanged(object? sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)LoadData);
            else
                LoadData();
        }

        // --------------------------------------------------------------
        //  Carousel setup and animation
        // --------------------------------------------------------------
        private void SetupCarousel()
        {
            slides = new List<Control> { chartBar, chartPie };

            chartBar.Location = new Point(0, 0);
            chartPie.Location = new Point(750, 0);

            slideTimer = new System.Windows.Forms.Timer { Interval = 15 };
            slideTimer.Tick += SlideTimer_Tick;

            StyleBarChart();
            StylePieChart();
        }

        private void btnCarouselLeft_Click(object sender, EventArgs e) => MoveCarousel(-1);
        private void btnCarouselRight_Click(object sender, EventArgs e) => MoveCarousel(1);

        private void MoveCarousel(int direction)
        {
            if (animating) return;
            int newIndex = currentSlideIndex + direction;
            if (newIndex < 0) newIndex = slides.Count - 1;
            if (newIndex >= slides.Count) newIndex = 0;
            ShowSlide(newIndex);
        }

        private void ShowSlide(int index)
        {
            currentSlideIndex = index;
            targetX = -index * 750;
            animating = true;
            slideTimer.Start();
        }

        private void SlideTimer_Tick(object? sender, EventArgs e)
        {
            int step = 30;
            foreach (var slide in slides)
            {
                int currentLeft = slide.Left;
                int desiredLeft = currentLeft + (targetX > currentLeft ? step : -step);
                if (Math.Abs(desiredLeft - targetX) < step)
                    desiredLeft = targetX;
                slide.Left = desiredLeft;
            }

            if (slides.All(s => s.Left == targetX))
            {
                slideTimer.Stop();
                animating = false;
            }
        }

        // --------------------------------------------------------------
        //  Chart appearance
        // --------------------------------------------------------------
        private void StyleBarChart()
        {
            chartBar.ChartAreas.Clear();
            ChartArea area = new ChartArea
            {
                BackColor = Color.FromArgb(30, 30, 40),
                AxisX = { LabelStyle = { ForeColor = Color.White }, Title = "Month", TitleForeColor = Color.White },
                AxisY = { LabelStyle = { ForeColor = Color.White }, Title = "Amount (₱)", TitleForeColor = Color.White }
            };
            chartBar.ChartAreas.Add(area);

            Series income = new Series("Income") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(76, 175, 80) };
            Series expenses = new Series("Expenses") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(244, 67, 54) };
            chartBar.Series.Add(income);
            chartBar.Series.Add(expenses);

            Legend legend = new Legend { BackColor = Color.FromArgb(30, 30, 40), ForeColor = Color.White };
            chartBar.Legends.Add(legend);
        }

        private void StylePieChart()
        {
            chartPie.ChartAreas.Clear();
            chartPie.ChartAreas.Add(new ChartArea { BackColor = Color.FromArgb(30, 30, 40) });

            Series pie = new Series("Sales") { ChartType = SeriesChartType.Pie };
            chartPie.Series.Add(pie);

            chartPie.Legends.Add(new Legend { BackColor = Color.FromArgb(30, 30, 40), ForeColor = Color.White });
        }

        // --------------------------------------------------------------
        //  Load data from services
        // --------------------------------------------------------------
        private void LoadData()
        {
            LoadStatCards();
            LoadBarChartData();
            LoadPieChartData();
        }

        private void LoadStatCards()
        {
            var metrics = _reportService.GetDashboardMetrics();
            lblTodaySaleValue.Text = $"₱{metrics.TodaysSales:N2}";
            lblMonthlySaleValue.Text = $"₱{metrics.MonthlySales:N2}";
            lblTotalCustomersValue.Text = metrics.TotalCustomers.ToString();
            lblPendingOrdersValue.Text = metrics.PendingOrders.ToString();
        }

        private void LoadBarChartData()
        {
            var incomeSeries = chartBar.Series["Income"];
            var expenseSeries = chartBar.Series["Expenses"];
            incomeSeries.Points.Clear();
            expenseSeries.Points.Clear();

            var revenueTrend = _salesService.GetRevenueTrend();
            var expensesByMonth = GetExpensesByMonth();

            foreach (var kvp in revenueTrend)
            {
                incomeSeries.Points.AddXY(kvp.Key, kvp.Value);
                expenseSeries.Points.AddXY(kvp.Key,
                    expensesByMonth.ContainsKey(kvp.Key) ? expensesByMonth[kvp.Key] : 0);
            }
            chartBar.Invalidate();
        }

        private void LoadPieChartData()
        {
            var pieSeries = chartPie.Series["Sales"];
            pieSeries.Points.Clear();

            var salesStats = _salesService.GetStatistics();
            var salesByCategory = salesStats.SalesByCategory;
            if (!salesByCategory.Any()) return;

            decimal total = salesByCategory.Values.Sum();
            Color[] colors = {
                Color.FromArgb(76,175,80), Color.FromArgb(33,150,243),
                Color.FromArgb(156,39,176), Color.FromArgb(255,152,0),
                Color.FromArgb(244,67,54)
            };
            int i = 0;
            foreach (var cat in salesByCategory)
            {
                int idx = pieSeries.Points.AddXY(cat.Key, cat.Value);
                var point = pieSeries.Points[idx];
                point.Color = colors[i % colors.Length];
                point.LegendText = $"{cat.Key} ({cat.Value / total * 100:F0}%)";
                i++;
            }
            pieSeries.IsValueShownAsLabel = true;
            pieSeries.Label = "#PERCENT{P0}";
            pieSeries.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            chartPie.Invalidate();
        }

        private Dictionary<string, decimal> GetExpensesByMonth()
        {
            var result = new Dictionary<string, decimal>();
            var allExpenses = _expenseService.GetAllExpenses();
            for (int i = 5; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var monthName = date.ToString("MMM");
                result[monthName] = allExpenses
                    .Where(e => e.ExpenseDate.Year == date.Year && e.ExpenseDate.Month == date.Month)
                    .Sum(e => e.Amount);
            }
            return result;
        }
    }
}