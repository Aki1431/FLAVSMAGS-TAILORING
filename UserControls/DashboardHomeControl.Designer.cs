using Guna.UI2.WinForms;

namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class DashboardHomeControl
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
            // Carousel container
            carouselContainer = new Guna2Panel();
            btnCarouselLeft = new Guna2Button();
            btnCarouselRight = new Guna2Button();
            chartBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // Stat cards panel
            statPanel = new FlowLayoutPanel();
            cardTodaySale = new Guna2Panel();
            lblTodaySaleTitle = new Guna2HtmlLabel();
            lblTodaySaleValue = new Guna2HtmlLabel();
            cardMonthlySale = new Guna2Panel();
            lblMonthlySaleTitle = new Guna2HtmlLabel();
            lblMonthlySaleValue = new Guna2HtmlLabel();
            cardTotalCustomers = new Guna2Panel();
            lblTotalCustomersTitle = new Guna2HtmlLabel();
            lblTotalCustomersValue = new Guna2HtmlLabel();
            cardPendingOrders = new Guna2Panel();
            lblPendingOrdersTitle = new Guna2HtmlLabel();
            lblPendingOrdersValue = new Guna2HtmlLabel();

            carouselContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartPie).BeginInit();
            cardTodaySale.SuspendLayout();
            cardMonthlySale.SuspendLayout();
            cardTotalCustomers.SuspendLayout();
            cardPendingOrders.SuspendLayout();
            SuspendLayout();

            // 
            // carouselContainer
            // 
            carouselContainer.BackColor = Color.Transparent;
            carouselContainer.BorderRadius = 15;
            carouselContainer.FillColor = Color.FromArgb(30, 30, 40);
            carouselContainer.ShadowDecoration.Enabled = true;
            carouselContainer.ShadowDecoration.Depth = 20;
            carouselContainer.Location = new Point(20, 20);
            carouselContainer.Name = "carouselContainer";
            carouselContainer.Size = new Size(750, 420);
            carouselContainer.TabIndex = 0;
            // 
            // btnCarouselLeft
            // 
            btnCarouselLeft.Animated = true;
            btnCarouselLeft.BorderRadius = 20;
            btnCarouselLeft.FillColor = Color.FromArgb(80, 80, 100);
            btnCarouselLeft.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCarouselLeft.ForeColor = Color.White;
            btnCarouselLeft.Location = new Point(10, 180);
            btnCarouselLeft.Name = "btnCarouselLeft";
            btnCarouselLeft.Size = new Size(40, 40);
            btnCarouselLeft.Text = "<";
            btnCarouselLeft.Click += btnCarouselLeft_Click;
            // 
            // btnCarouselRight
            // 
            btnCarouselRight.Animated = true;
            btnCarouselRight.BorderRadius = 20;
            btnCarouselRight.FillColor = Color.FromArgb(80, 80, 100);
            btnCarouselRight.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCarouselRight.ForeColor = Color.White;
            btnCarouselRight.Location = new Point(700, 180);
            btnCarouselRight.Name = "btnCarouselRight";
            btnCarouselRight.Size = new Size(40, 40);
            btnCarouselRight.Text = ">";
            btnCarouselRight.Click += btnCarouselRight_Click;
            // 
            // chartBar (Bar chart)
            // 
            chartBar.BackColor = Color.FromArgb(30, 30, 40);
            chartBar.Location = new Point(0, 0);
            chartBar.Name = "chartBar";
            chartBar.Size = new Size(750, 400);
            chartBar.TabIndex = 1;
            // 
            // chartPie (Pie chart)
            // 
            chartPie.BackColor = Color.FromArgb(30, 30, 40);
            chartPie.Location = new Point(750, 0);   // off‑screen initially
            chartPie.Name = "chartPie";
            chartPie.Size = new Size(750, 400);
            chartPie.TabIndex = 2;

            // --- ADD CHILDREN TO carouselContainer ---
            carouselContainer.Controls.Add(chartBar);
            carouselContainer.Controls.Add(chartPie);
            carouselContainer.Controls.Add(btnCarouselLeft);
            carouselContainer.Controls.Add(btnCarouselRight);

            // 
            // statPanel
            // 
            statPanel.AutoScroll = true;
            statPanel.BackColor = Color.Transparent;
            statPanel.Dock = DockStyle.Bottom;
            statPanel.Height = 140;
            statPanel.Name = "statPanel";
            statPanel.Padding = new Padding(20, 10, 20, 10);
            // 
            // cardTodaySale
            // 
            cardTodaySale.BackColor = Color.Transparent;
            cardTodaySale.BorderRadius = 10;
            cardTodaySale.FillColor = Color.FromArgb(45, 44, 90);
            cardTodaySale.Margin = new Padding(10);
            cardTodaySale.ShadowDecoration.Enabled = true;
            cardTodaySale.ShadowDecoration.Depth = 10;
            cardTodaySale.Size = new Size(190, 110);
            cardTodaySale.Controls.Add(lblTodaySaleTitle);
            cardTodaySale.Controls.Add(lblTodaySaleValue);
            // 
            // lblTodaySaleTitle
            // 
            lblTodaySaleTitle.AutoSize = true;
            lblTodaySaleTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTodaySaleTitle.ForeColor = Color.White;
            lblTodaySaleTitle.Location = new Point(10, 10);
            lblTodaySaleTitle.Text = "Today's Sale";
            // 
            // lblTodaySaleValue
            // 
            lblTodaySaleValue.AutoSize = true;
            lblTodaySaleValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTodaySaleValue.ForeColor = Color.White;
            lblTodaySaleValue.Location = new Point(10, 50);
            lblTodaySaleValue.Text = "₱0.00";
            // 
            // cardMonthlySale
            // 
            cardMonthlySale.BackColor = Color.Transparent;
            cardMonthlySale.BorderRadius = 10;
            cardMonthlySale.FillColor = Color.FromArgb(45, 44, 90);
            cardMonthlySale.Margin = new Padding(10);
            cardMonthlySale.ShadowDecoration.Enabled = true;
            cardMonthlySale.ShadowDecoration.Depth = 10;
            cardMonthlySale.Size = new Size(190, 110);
            cardMonthlySale.Controls.Add(lblMonthlySaleTitle);
            cardMonthlySale.Controls.Add(lblMonthlySaleValue);
            // 
            // lblMonthlySaleTitle
            // 
            lblMonthlySaleTitle.AutoSize = true;
            lblMonthlySaleTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblMonthlySaleTitle.ForeColor = Color.White;
            lblMonthlySaleTitle.Location = new Point(10, 10);
            lblMonthlySaleTitle.Text = "Monthly Sales";
            // 
            // lblMonthlySaleValue
            // 
            lblMonthlySaleValue.AutoSize = true;
            lblMonthlySaleValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMonthlySaleValue.ForeColor = Color.White;
            lblMonthlySaleValue.Location = new Point(10, 50);
            lblMonthlySaleValue.Text = "₱0.00";
            // 
            // cardTotalCustomers
            // 
            cardTotalCustomers.BackColor = Color.Transparent;
            cardTotalCustomers.BorderRadius = 10;
            cardTotalCustomers.FillColor = Color.FromArgb(45, 44, 90);
            cardTotalCustomers.Margin = new Padding(10);
            cardTotalCustomers.ShadowDecoration.Enabled = true;
            cardTotalCustomers.ShadowDecoration.Depth = 10;
            cardTotalCustomers.Size = new Size(190, 110);
            cardTotalCustomers.Controls.Add(lblTotalCustomersTitle);
            cardTotalCustomers.Controls.Add(lblTotalCustomersValue);
            // 
            // lblTotalCustomersTitle
            // 
            lblTotalCustomersTitle.AutoSize = true;
            lblTotalCustomersTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotalCustomersTitle.ForeColor = Color.White;
            lblTotalCustomersTitle.Location = new Point(10, 10);
            lblTotalCustomersTitle.Text = "Total Customers";
            // 
            // lblTotalCustomersValue
            // 
            lblTotalCustomersValue.AutoSize = true;
            lblTotalCustomersValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalCustomersValue.ForeColor = Color.White;
            lblTotalCustomersValue.Location = new Point(10, 50);
            lblTotalCustomersValue.Text = "0";
            // 
            // cardPendingOrders
            // 
            cardPendingOrders.BackColor = Color.Transparent;
            cardPendingOrders.BorderRadius = 10;
            cardPendingOrders.FillColor = Color.FromArgb(45, 44, 90);
            cardPendingOrders.Margin = new Padding(10);
            cardPendingOrders.ShadowDecoration.Enabled = true;
            cardPendingOrders.ShadowDecoration.Depth = 10;
            cardPendingOrders.Size = new Size(190, 110);
            cardPendingOrders.Controls.Add(lblPendingOrdersTitle);
            cardPendingOrders.Controls.Add(lblPendingOrdersValue);
            // 
            // lblPendingOrdersTitle
            // 
            lblPendingOrdersTitle.AutoSize = true;
            lblPendingOrdersTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPendingOrdersTitle.ForeColor = Color.White;
            lblPendingOrdersTitle.Location = new Point(10, 10);
            lblPendingOrdersTitle.Text = "Pending Orders";
            // 
            // lblPendingOrdersValue
            // 
            lblPendingOrdersValue.AutoSize = true;
            lblPendingOrdersValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblPendingOrdersValue.ForeColor = Color.White;
            lblPendingOrdersValue.Location = new Point(10, 50);
            lblPendingOrdersValue.Text = "0";

            // --- ADD STAT CARDS TO statPanel ---
            statPanel.Controls.Add(cardTodaySale);
            statPanel.Controls.Add(cardMonthlySale);
            statPanel.Controls.Add(cardTotalCustomers);
            statPanel.Controls.Add(cardPendingOrders);

            // 
            // DashboardHomeControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(carouselContainer);
            Controls.Add(statPanel);
            Name = "DashboardHomeControl";
            Size = new Size(950, 640);
            carouselContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartPie).EndInit();
            cardTodaySale.ResumeLayout(false);
            cardMonthlySale.ResumeLayout(false);
            cardTotalCustomers.ResumeLayout(false);
            cardPendingOrders.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Guna2Panel carouselContainer;
        private Guna2Button btnCarouselLeft;
        private Guna2Button btnCarouselRight;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private FlowLayoutPanel statPanel;
        private Guna2Panel cardTodaySale;
        private Guna2HtmlLabel lblTodaySaleTitle;
        private Guna2HtmlLabel lblTodaySaleValue;
        private Guna2Panel cardMonthlySale;
        private Guna2HtmlLabel lblMonthlySaleTitle;
        private Guna2HtmlLabel lblMonthlySaleValue;
        private Guna2Panel cardTotalCustomers;
        private Guna2HtmlLabel lblTotalCustomersTitle;
        private Guna2HtmlLabel lblTotalCustomersValue;
        private Guna2Panel cardPendingOrders;
        private Guna2HtmlLabel lblPendingOrdersTitle;
        private Guna2HtmlLabel lblPendingOrdersValue;
    }
}