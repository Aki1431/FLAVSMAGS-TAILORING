using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class SalesControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Stat cards
            cardTotalSales = new Guna2Panel();
            lblTotalSales = new Guna2HtmlLabel();
            lblTotalSalesTitle = new Guna2HtmlLabel();
            lblSalesTrend = new Guna2HtmlLabel();
            cardRevenue = new Guna2Panel();
            lblRevenue = new Guna2HtmlLabel();
            lblRevenueTitle = new Guna2HtmlLabel();
            lblRevenueTrend = new Guna2HtmlLabel();
            cardAvgOrder = new Guna2Panel();
            lblAvgOrder = new Guna2HtmlLabel();
            lblAvgOrderTitle = new Guna2HtmlLabel();
            lblAvgOrderTrend = new Guna2HtmlLabel();
            cardProfit = new Guna2Panel();
            lblProfit = new Guna2HtmlLabel();
            lblProfitTitle = new Guna2HtmlLabel();

            // Charts
            chartRevenueTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartSalesByCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // Top products panel
            panelTopProducts = new Guna2Panel();
            flowTopProducts = new FlowLayoutPanel();

            cardTotalSales.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardAvgOrder.SuspendLayout();
            cardProfit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRevenueTrend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).BeginInit();
            panelTopProducts.SuspendLayout();
            SuspendLayout();

            // =============== STAT CARDS (Top Row) ===============
            // Total Sales
            cardTotalSales.BackColor = Color.Transparent;
            cardTotalSales.BorderRadius = 10;
            cardTotalSales.FillColor = Color.FromArgb(45, 44, 90);
            cardTotalSales.Size = new Size(210, 100);
            cardTotalSales.Location = new Point(20, 20);
            cardTotalSales.ShadowDecoration.Enabled = true;
            cardTotalSales.ShadowDecoration.Depth = 10;
            lblTotalSalesTitle.Text = "Total Sales";
            lblTotalSalesTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotalSalesTitle.ForeColor = Color.White;
            lblTotalSalesTitle.Location = new Point(15, 15);
            lblTotalSales.ForeColor = Color.White;
            lblTotalSales.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalSales.Location = new Point(15, 42);
            lblTotalSales.Text = "₱0";
            lblSalesTrend.ForeColor = Color.White;
            lblSalesTrend.Font = new Font("Segoe UI", 8);
            lblSalesTrend.Location = new Point(15, 70);
            lblSalesTrend.Text = "";
            cardTotalSales.Controls.Add(lblTotalSalesTitle);
            cardTotalSales.Controls.Add(lblTotalSales);
            cardTotalSales.Controls.Add(lblSalesTrend);

            // Revenue
            cardRevenue.BackColor = Color.Transparent;
            cardRevenue.BorderRadius = 10;
            cardRevenue.FillColor = Color.FromArgb(45, 44, 90);
            cardRevenue.Size = new Size(210, 100);
            cardRevenue.Location = new Point(250, 20);
            cardRevenue.ShadowDecoration.Enabled = true;
            cardRevenue.ShadowDecoration.Depth = 10;
            lblRevenueTitle.Text = "Revenue";
            lblRevenueTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.White;
            lblRevenueTitle.Location = new Point(15, 15);
            lblRevenue.ForeColor = Color.White;
            lblRevenue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblRevenue.Location = new Point(15, 42);
            lblRevenue.Text = "₱0";
            lblRevenueTrend.ForeColor = Color.White;
            lblRevenueTrend.Font = new Font("Segoe UI", 8);
            lblRevenueTrend.Location = new Point(15, 70);
            lblRevenueTrend.Text = "";
            cardRevenue.Controls.Add(lblRevenueTitle);
            cardRevenue.Controls.Add(lblRevenue);
            cardRevenue.Controls.Add(lblRevenueTrend);

            // Average Order
            cardAvgOrder.BackColor = Color.Transparent;
            cardAvgOrder.BorderRadius = 10;
            cardAvgOrder.FillColor = Color.FromArgb(45, 44, 90);
            cardAvgOrder.Size = new Size(210, 100);
            cardAvgOrder.Location = new Point(480, 20);
            cardAvgOrder.ShadowDecoration.Enabled = true;
            cardAvgOrder.ShadowDecoration.Depth = 10;
            lblAvgOrderTitle.Text = "Avg. Order Value";
            lblAvgOrderTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblAvgOrderTitle.ForeColor = Color.White;
            lblAvgOrderTitle.Location = new Point(15, 15);
            lblAvgOrder.ForeColor = Color.White;
            lblAvgOrder.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblAvgOrder.Location = new Point(15, 42);
            lblAvgOrder.Text = "₱0.00";
            lblAvgOrderTrend.ForeColor = Color.White;
            lblAvgOrderTrend.Font = new Font("Segoe UI", 8);
            lblAvgOrderTrend.Location = new Point(15, 70);
            lblAvgOrderTrend.Text = "";
            cardAvgOrder.Controls.Add(lblAvgOrderTitle);
            cardAvgOrder.Controls.Add(lblAvgOrder);
            cardAvgOrder.Controls.Add(lblAvgOrderTrend);

            // Profit
            cardProfit.BackColor = Color.Transparent;
            cardProfit.BorderRadius = 10;
            cardProfit.FillColor = Color.FromArgb(45, 44, 90);
            cardProfit.Size = new Size(210, 100);
            cardProfit.Location = new Point(710, 20);
            cardProfit.ShadowDecoration.Enabled = true;
            cardProfit.ShadowDecoration.Depth = 10;
            lblProfitTitle.Text = "Total Profit";
            lblProfitTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblProfitTitle.ForeColor = Color.White;
            lblProfitTitle.Location = new Point(15, 15);
            lblProfit.ForeColor = Color.White;
            lblProfit.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblProfit.Location = new Point(15, 42);
            lblProfit.Text = "₱0";
            cardProfit.Controls.Add(lblProfitTitle);
            cardProfit.Controls.Add(lblProfit);

            // =============== CHARTS ===============
            chartRevenueTrend.BackColor = Color.FromArgb(30, 30, 40);
            chartRevenueTrend.Location = new Point(20, 140);
            chartRevenueTrend.Size = new Size(450, 240);
            chartRevenueTrend.TabIndex = 5;
            chartSalesByCategory.BackColor = Color.FromArgb(30, 30, 40);
            chartSalesByCategory.Location = new Point(490, 140);
            chartSalesByCategory.Size = new Size(430, 240);
            chartSalesByCategory.TabIndex = 6;

            // =============== TOP PRODUCTS PANEL ===============
            panelTopProducts.BackColor = Color.Transparent;
            panelTopProducts.BorderRadius = 12;
            panelTopProducts.FillColor = Color.FromArgb(45, 44, 90);
            panelTopProducts.Location = new Point(20, 400);
            panelTopProducts.Size = new Size(900, 220);
            panelTopProducts.ShadowDecoration.Enabled = true;
            panelTopProducts.ShadowDecoration.Depth = 10;

            flowTopProducts.BackColor = Color.Transparent;
            flowTopProducts.Dock = DockStyle.Fill;
            flowTopProducts.FlowDirection = FlowDirection.TopDown;
            flowTopProducts.AutoScroll = true;
            flowTopProducts.Padding = new Padding(10);
            panelTopProducts.Controls.Add(flowTopProducts);

            // =============== PARENT CONTROL ===============
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(cardTotalSales);
            Controls.Add(cardRevenue);
            Controls.Add(cardAvgOrder);
            Controls.Add(cardProfit);
            Controls.Add(chartRevenueTrend);
            Controls.Add(chartSalesByCategory);
            Controls.Add(panelTopProducts);
            Name = "SalesControl";
            Size = new Size(949, 640);
            cardTotalSales.ResumeLayout(false);
            cardRevenue.ResumeLayout(false);
            cardAvgOrder.ResumeLayout(false);
            cardProfit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartRevenueTrend).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).EndInit();
            panelTopProducts.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Declarations
        private Guna2Panel cardTotalSales;
        private Guna2HtmlLabel lblTotalSales;
        private Guna2HtmlLabel lblTotalSalesTitle;
        private Guna2HtmlLabel lblSalesTrend;
        private Guna2Panel cardRevenue;
        private Guna2HtmlLabel lblRevenue;
        private Guna2HtmlLabel lblRevenueTitle;
        private Guna2HtmlLabel lblRevenueTrend;
        private Guna2Panel cardAvgOrder;
        private Guna2HtmlLabel lblAvgOrder;
        private Guna2HtmlLabel lblAvgOrderTitle;
        private Guna2HtmlLabel lblAvgOrderTrend;
        private Guna2Panel cardProfit;
        private Guna2HtmlLabel lblProfit;
        private Guna2HtmlLabel lblProfitTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueTrend;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesByCategory;
        private Guna2Panel panelTopProducts;
        private FlowLayoutPanel flowTopProducts;
    }
}

//namespace FLAVSMAGS_TAILORING.UserControls
//{
//    partial class SalesControl
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
//            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
//            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
//            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
//            cardTotalSales = new RoundedPanel();
//            lblSalesTrend = new Label();
//            lblTotalSales = new Label();
//            label1 = new Label();
//            cardRevenue = new RoundedPanel();
//            lblRevenueTrend = new Label();
//            lblRevenue = new Label();
//            panel = new Label();
//            cardAvgOrder = new RoundedPanel();
//            lblAvgOrderTrend = new Label();
//            lblAvgOrder = new Label();
//            label7 = new Label();
//            cardConversion = new RoundedPanel();
//            label8 = new Label();
//            lblConversion = new Label();
//            label10 = new Label();
//            chartRevenueTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
//            chartSalesByCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
//            roundedPanel1 = new RoundedPanel();
//            dgvTopProducts = new DataGridView();
//            cardTotalSales.SuspendLayout();
//            cardRevenue.SuspendLayout();
//            cardAvgOrder.SuspendLayout();
//            cardConversion.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)chartRevenueTrend).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).BeginInit();
//            roundedPanel1.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
//            SuspendLayout();
//            // 
//            // cardTotalSales
//            // 
//            cardTotalSales.BackColor = Color.FromArgb(90, 88, 140);
//            cardTotalSales.Controls.Add(lblSalesTrend);
//            cardTotalSales.Controls.Add(lblTotalSales);
//            cardTotalSales.Controls.Add(label1);
//            cardTotalSales.Location = new Point(30, 30);
//            cardTotalSales.Name = "cardTotalSales";
//            cardTotalSales.Size = new Size(197, 101);
//            cardTotalSales.TabIndex = 0;
//            // 
//            // lblSalesTrend
//            // 
//            lblSalesTrend.AutoSize = true;
//            lblSalesTrend.BackColor = Color.FromArgb(90, 88, 140);
//            lblSalesTrend.ForeColor = Color.White;
//            lblSalesTrend.Location = new Point(111, 54);
//            lblSalesTrend.Name = "lblSalesTrend";
//            lblSalesTrend.Size = new Size(59, 20);
//            lblSalesTrend.TabIndex = 2;
//            lblSalesTrend.Text = "↑ 12.5%";
//            // 
//            // lblTotalSales
//            // 
//            lblTotalSales.AutoSize = true;
//            lblTotalSales.BackColor = Color.FromArgb(90, 88, 140);
//            lblTotalSales.ForeColor = Color.White;
//            lblTotalSales.Location = new Point(12, 54);
//            lblTotalSales.Name = "lblTotalSales";
//            lblTotalSales.Size = new Size(68, 20);
//            lblTotalSales.TabIndex = 1;
//            lblTotalSales.Text = "$245,891";
//            // 
//            // label1
//            // 
//            label1.AutoSize = true;
//            label1.BackColor = Color.FromArgb(26, 25, 62);
//            label1.ForeColor = Color.White;
//            label1.Location = new Point(12, 16);
//            label1.Name = "label1";
//            label1.Size = new Size(80, 20);
//            label1.TabIndex = 0;
//            label1.Text = "Total Sales";
//            // 
//            // cardRevenue
//            // 
//            cardRevenue.BackColor = Color.FromArgb(90, 88, 140);
//            cardRevenue.Controls.Add(lblRevenueTrend);
//            cardRevenue.Controls.Add(lblRevenue);
//            cardRevenue.Controls.Add(panel);
//            cardRevenue.Location = new Point(252, 30);
//            cardRevenue.Name = "cardRevenue";
//            cardRevenue.Size = new Size(197, 101);
//            cardRevenue.TabIndex = 3;
//            // 
//            // lblRevenueTrend
//            // 
//            lblRevenueTrend.AutoSize = true;
//            lblRevenueTrend.BackColor = Color.FromArgb(90, 88, 140);
//            lblRevenueTrend.ForeColor = Color.White;
//            lblRevenueTrend.Location = new Point(111, 54);
//            lblRevenueTrend.Name = "lblRevenueTrend";
//            lblRevenueTrend.Size = new Size(51, 20);
//            lblRevenueTrend.TabIndex = 2;
//            lblRevenueTrend.Text = "↑ 8.3%";
//            // 
//            // lblRevenue
//            // 
//            lblRevenue.AutoSize = true;
//            lblRevenue.BackColor = Color.FromArgb(90, 88, 140);
//            lblRevenue.ForeColor = Color.White;
//            lblRevenue.Location = new Point(12, 54);
//            lblRevenue.Name = "lblRevenue";
//            lblRevenue.Size = new Size(68, 20);
//            lblRevenue.TabIndex = 1;
//            lblRevenue.Text = "$198,432";
//            // 
//            // panel
//            // 
//            panel.AutoSize = true;
//            panel.BackColor = Color.FromArgb(26, 25, 62);
//            panel.ForeColor = Color.White;
//            panel.Location = new Point(12, 16);
//            panel.Name = "panel";
//            panel.Size = new Size(65, 20);
//            panel.TabIndex = 0;
//            panel.Text = "Revenue";
//            // 
//            // cardAvgOrder
//            // 
//            cardAvgOrder.BackColor = Color.FromArgb(90, 88, 140);
//            cardAvgOrder.Controls.Add(lblAvgOrderTrend);
//            cardAvgOrder.Controls.Add(lblAvgOrder);
//            cardAvgOrder.Controls.Add(label7);
//            cardAvgOrder.Location = new Point(470, 30);
//            cardAvgOrder.Name = "cardAvgOrder";
//            cardAvgOrder.Size = new Size(197, 101);
//            cardAvgOrder.TabIndex = 3;
//            // 
//            // lblAvgOrderTrend
//            // 
//            lblAvgOrderTrend.AutoSize = true;
//            lblAvgOrderTrend.BackColor = Color.FromArgb(90, 88, 140);
//            lblAvgOrderTrend.ForeColor = Color.White;
//            lblAvgOrderTrend.Location = new Point(111, 54);
//            lblAvgOrderTrend.Name = "lblAvgOrderTrend";
//            lblAvgOrderTrend.Size = new Size(51, 20);
//            lblAvgOrderTrend.TabIndex = 2;
//            lblAvgOrderTrend.Text = "↑ 5.2%";
//            // 
//            // lblAvgOrder
//            // 
//            lblAvgOrder.AutoSize = true;
//            lblAvgOrder.BackColor = Color.FromArgb(90, 88, 140);
//            lblAvgOrder.ForeColor = Color.White;
//            lblAvgOrder.Location = new Point(12, 54);
//            lblAvgOrder.Name = "lblAvgOrder";
//            lblAvgOrder.Size = new Size(60, 20);
//            lblAvgOrder.TabIndex = 1;
//            lblAvgOrder.Text = "$127.50";
//            // 
//            // label7
//            // 
//            label7.AutoSize = true;
//            label7.BackColor = Color.FromArgb(26, 25, 62);
//            label7.ForeColor = Color.White;
//            label7.Location = new Point(12, 16);
//            label7.Name = "label7";
//            label7.Size = new Size(120, 20);
//            label7.TabIndex = 0;
//            label7.Text = "Avg. Order Value";
//            // 
//            // cardConversion
//            // 
//            cardConversion.BackColor = Color.FromArgb(90, 88, 140);
//            cardConversion.Controls.Add(label8);
//            cardConversion.Controls.Add(lblConversion);
//            cardConversion.Controls.Add(label10);
//            cardConversion.Location = new Point(690, 30);
//            cardConversion.Name = "cardConversion";
//            cardConversion.Size = new Size(197, 101);
//            cardConversion.TabIndex = 3;
//            // 
//            // label8
//            // 
//            label8.AutoSize = true;
//            label8.BackColor = Color.FromArgb(90, 88, 140);
//            label8.ForeColor = Color.White;
//            label8.Location = new Point(111, 54);
//            label8.Name = "label8";
//            label8.Size = new Size(59, 20);
//            label8.TabIndex = 2;
//            label8.Text = "↑ 12.5%";
//            // 
//            // lblConversion
//            // 
//            lblConversion.AutoSize = true;
//            lblConversion.BackColor = Color.FromArgb(90, 88, 140);
//            lblConversion.ForeColor = Color.White;
//            lblConversion.Location = new Point(12, 54);
//            lblConversion.Name = "lblConversion";
//            lblConversion.Size = new Size(48, 20);
//            lblConversion.TabIndex = 1;
//            lblConversion.Text = "24.8%";
//            // 
//            // label10
//            // 
//            label10.AutoSize = true;
//            label10.BackColor = Color.FromArgb(26, 25, 62);
//            label10.ForeColor = Color.White;
//            label10.Location = new Point(12, 16);
//            label10.Name = "label10";
//            label10.Size = new Size(116, 20);
//            label10.TabIndex = 0;
//            label10.Text = "Conversion Rate";
//            // 
//            // chartRevenueTrend
//            // 
//            chartRevenueTrend.BackColor = Color.FromArgb(90, 88, 140);
//            chartArea1.Name = "ChartArea1";
//            chartRevenueTrend.ChartAreas.Add(chartArea1);
//            legend1.BackColor = Color.FromArgb(90, 88, 140);
//            legend1.ForeColor = Color.White;
//            legend1.Name = "Legend1";
//            chartRevenueTrend.Legends.Add(legend1);
//            chartRevenueTrend.Location = new Point(30, 155);
//            chartRevenueTrend.Name = "chartRevenueTrend";
//            series1.ChartArea = "ChartArea1";
//            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
//            series1.Legend = "Legend1";
//            series1.Name = "Series1";
//            chartRevenueTrend.Series.Add(series1);
//            chartRevenueTrend.Size = new Size(419, 212);
//            chartRevenueTrend.TabIndex = 4;
//            chartRevenueTrend.Text = "chart1";
//            // 
//            // chartSalesByCategory
//            // 
//            chartSalesByCategory.BackColor = Color.FromArgb(90, 88, 140);
//            chartArea2.BackColor = Color.FromArgb(90, 88, 140);
//            chartArea2.BorderColor = Color.Blue;
//            chartArea2.Name = "ChartArea1";
//            chartSalesByCategory.ChartAreas.Add(chartArea2);
//            legend2.Name = "Legend1";
//            chartSalesByCategory.Legends.Add(legend2);
//            chartSalesByCategory.Location = new Point(468, 155);
//            chartSalesByCategory.Name = "chartSalesByCategory";
//            series2.ChartArea = "ChartArea1";
//            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
//            series2.Legend = "Legend1";
//            series2.Name = "Series1";
//            chartSalesByCategory.Series.Add(series2);
//            chartSalesByCategory.Size = new Size(419, 212);
//            chartSalesByCategory.TabIndex = 5;
//            chartSalesByCategory.Text = "chart2";
//            // 
//            // roundedPanel1
//            // 
//            roundedPanel1.BackColor = Color.FromArgb(90, 88, 140);
//            roundedPanel1.Controls.Add(dgvTopProducts);
//            roundedPanel1.Location = new Point(34, 396);
//            roundedPanel1.Name = "roundedPanel1";
//            roundedPanel1.Size = new Size(853, 229);
//            roundedPanel1.TabIndex = 6;
//            // 
//            // dgvTopProducts
//            // 
//            dgvTopProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            dgvTopProducts.Location = new Point(19, 17);
//            dgvTopProducts.Name = "dgvTopProducts";
//            dgvTopProducts.RowHeadersWidth = 51;
//            dgvTopProducts.Size = new Size(814, 188);
//            dgvTopProducts.TabIndex = 0;
//            // 
//            // SalesControl
//            // 
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(26, 25, 62);
//            Controls.Add(roundedPanel1);
//            Controls.Add(chartSalesByCategory);
//            Controls.Add(chartRevenueTrend);
//            Controls.Add(cardConversion);
//            Controls.Add(cardAvgOrder);
//            Controls.Add(cardRevenue);
//            Controls.Add(cardTotalSales);
//            Name = "SalesControl";
//            Size = new Size(949, 640);
//            cardTotalSales.ResumeLayout(false);
//            cardTotalSales.PerformLayout();
//            cardRevenue.ResumeLayout(false);
//            cardRevenue.PerformLayout();
//            cardAvgOrder.ResumeLayout(false);
//            cardAvgOrder.PerformLayout();
//            cardConversion.ResumeLayout(false);
//            cardConversion.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)chartRevenueTrend).EndInit();
//            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).EndInit();
//            roundedPanel1.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
//            ResumeLayout(false);
//        }

//        #endregion

//        private RoundedPanel cardTotalSales;
//        private RoundedPanel cardRevenue;
//        private RoundedPanel cardAvgOrder;
//        private RoundedPanel cardConversion;
//        private Label label1;
//        private Label lblTotalSales;
//        private Label lblSalesTrend;
//        private Label lblRevenueTrend;
//        private Label lblRevenue;
//        private Label panel;
//        private Label lblAvgOrderTrend;
//        private Label lblAvgOrder;
//        private Label label7;
//        private Label label8;
//        private Label lblConversion;
//        private Label label10;
//        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueTrend;
//        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesByCategory;
//        private RoundedPanel roundedPanel1;
//        private DataGridView dgvTopProducts;
//    }
//}