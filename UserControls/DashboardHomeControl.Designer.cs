namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class DashboardHomeControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label2 = new Label();
            Today_Sale = new FlowLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            label1 = new Label();
            MonthlySale = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            label3 = new Label();
            flowLayoutPanel5 = new FlowLayoutPanel();
            label4 = new Label();
            TotalCustomer = new FlowLayoutPanel();
            flowLayoutPanel8 = new FlowLayoutPanel();
            label5 = new Label();
            flowLayoutPanel9 = new FlowLayoutPanel();
            label6 = new Label();
            PendingCustomers = new FlowLayoutPanel();
            flowLayoutPanel11 = new FlowLayoutPanel();
            label7 = new Label();
            flowLayoutPanel12 = new FlowLayoutPanel();
            label8 = new Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            flowLayoutPanel1.SuspendLayout();
            Today_Sale.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            MonthlySale.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            TotalCustomer.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            PendingCustomers.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.BackColor = Color.FromArgb(90, 88, 140);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Location = new Point(13, 58);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(152, 64);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;
            label2.Location = new Point(13, 10);
            label2.Name = "label2";
            label2.Size = new Size(17, 20);
            label2.TabIndex = 1;
            label2.Text = "$";
            // 
            // Today_Sale
            // 
            Today_Sale.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Today_Sale.BackColor = Color.FromArgb(90, 88, 140);
            Today_Sale.Controls.Add(flowLayoutPanel6);
            Today_Sale.Controls.Add(flowLayoutPanel1);
            Today_Sale.Location = new Point(24, 26);
            Today_Sale.Name = "Today_Sale";
            Today_Sale.Padding = new Padding(10);
            Today_Sale.Size = new Size(192, 140);
            Today_Sale.TabIndex = 3;
            Today_Sale.Paint += flowLayoutPanel2_Paint;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.BackColor = Color.FromArgb(26, 25, 62);
            flowLayoutPanel6.Controls.Add(label1);
            flowLayoutPanel6.Location = new Point(13, 13);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Padding = new Padding(10);
            flowLayoutPanel6.Size = new Size(152, 39);
            flowLayoutPanel6.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(13, 10);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 1;
            label1.Text = "Today's Sale";
            label1.Click += label1_Click_1;
            // 
            // MonthlySale
            // 
            MonthlySale.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MonthlySale.BackColor = Color.FromArgb(90, 88, 140);
            MonthlySale.Controls.Add(flowLayoutPanel4);
            MonthlySale.Controls.Add(flowLayoutPanel5);
            MonthlySale.Location = new Point(246, 26);
            MonthlySale.Name = "MonthlySale";
            MonthlySale.Padding = new Padding(10);
            MonthlySale.Size = new Size(192, 140);
            MonthlySale.TabIndex = 4;
            MonthlySale.Paint += MonthlySale_Paint;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.BackColor = Color.FromArgb(26, 25, 62);
            flowLayoutPanel4.Controls.Add(label3);
            flowLayoutPanel4.ForeColor = Color.FromArgb(26, 25, 62);
            flowLayoutPanel4.Location = new Point(13, 13);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new Padding(10);
            flowLayoutPanel4.Size = new Size(158, 39);
            flowLayoutPanel4.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            label3.Location = new Point(13, 10);
            label3.Name = "label3";
            label3.Size = new Size(101, 20);
            label3.TabIndex = 1;
            label3.Text = "Monthly Sales";
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel5.BackColor = Color.FromArgb(90, 88, 140);
            flowLayoutPanel5.Controls.Add(label4);
            flowLayoutPanel5.Location = new Point(13, 58);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Padding = new Padding(10);
            flowLayoutPanel5.Size = new Size(158, 64);
            flowLayoutPanel5.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.ForeColor = Color.White;
            label4.Location = new Point(13, 10);
            label4.Name = "label4";
            label4.Size = new Size(17, 20);
            label4.TabIndex = 1;
            label4.Text = "$";
            // 
            // TotalCustomer
            // 
            TotalCustomer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TotalCustomer.BackColor = Color.FromArgb(90, 88, 140);
            TotalCustomer.Controls.Add(flowLayoutPanel8);
            TotalCustomer.Controls.Add(flowLayoutPanel9);
            TotalCustomer.Location = new Point(473, 26);
            TotalCustomer.Name = "TotalCustomer";
            TotalCustomer.Padding = new Padding(10);
            TotalCustomer.Size = new Size(192, 140);
            TotalCustomer.TabIndex = 5;
            TotalCustomer.Paint += TotalCustomer_Paint;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel8.BackColor = Color.FromArgb(26, 25, 62);
            flowLayoutPanel8.Controls.Add(label5);
            flowLayoutPanel8.Location = new Point(13, 13);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Padding = new Padding(10);
            flowLayoutPanel8.Size = new Size(155, 39);
            flowLayoutPanel8.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.ForeColor = Color.White;
            label5.Location = new Point(13, 10);
            label5.Name = "label5";
            label5.Size = new Size(115, 20);
            label5.TabIndex = 1;
            label5.Text = "Total Customers";
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel9.BackColor = Color.FromArgb(90, 88, 140);
            flowLayoutPanel9.Controls.Add(label6);
            flowLayoutPanel9.ForeColor = Color.White;
            flowLayoutPanel9.Location = new Point(13, 58);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            flowLayoutPanel9.Padding = new Padding(10);
            flowLayoutPanel9.Size = new Size(155, 64);
            flowLayoutPanel9.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(13, 10);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 1;
            label6.Text = "Total: ";
            // 
            // PendingCustomers
            // 
            PendingCustomers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PendingCustomers.BackColor = Color.FromArgb(90, 88, 140);
            PendingCustomers.Controls.Add(flowLayoutPanel11);
            PendingCustomers.Controls.Add(flowLayoutPanel12);
            PendingCustomers.Location = new Point(699, 26);
            PendingCustomers.Name = "PendingCustomers";
            PendingCustomers.Padding = new Padding(10);
            PendingCustomers.Size = new Size(192, 140);
            PendingCustomers.TabIndex = 6;
            PendingCustomers.Paint += PendingCustomers_Paint;
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel11.BackColor = Color.FromArgb(26, 25, 62);
            flowLayoutPanel11.Controls.Add(label7);
            flowLayoutPanel11.Location = new Point(13, 13);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Padding = new Padding(10);
            flowLayoutPanel11.Size = new Size(157, 39);
            flowLayoutPanel11.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.ForeColor = Color.White;
            label7.Location = new Point(13, 10);
            label7.Name = "label7";
            label7.Size = new Size(110, 20);
            label7.TabIndex = 1;
            label7.Text = "Pending Orders";
            // 
            // flowLayoutPanel12
            // 
            flowLayoutPanel12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel12.BackColor = Color.FromArgb(90, 88, 140);
            flowLayoutPanel12.Controls.Add(label8);
            flowLayoutPanel12.Location = new Point(13, 58);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Padding = new Padding(10);
            flowLayoutPanel12.Size = new Size(157, 64);
            flowLayoutPanel12.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.ForeColor = Color.White;
            label8.Location = new Point(13, 10);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 1;
            label8.Text = "Total: ";
            // 
            // chart1
            // 
            chart1.BackColor = Color.FromArgb(90, 88, 140);
            chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chart1.BackSecondaryColor = Color.White;
            chart1.BorderlineColor = Color.FromArgb(26, 25, 62);
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.BackColor = Color.FromArgb(26, 25, 62);
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.BorderColor = Color.FromArgb(90, 88, 140);
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = Color.White;
            chart1.ChartAreas.Add(chartArea1);
            chart1.Cursor = Cursors.Hand;
            chart1.DataSource = chart1.Annotations;
            legend1.BackColor = Color.FromArgb(90, 88, 140);
            legend1.ForeColor = Color.White;
            legend1.Name = "Legend1";
            legend1.TitleForeColor = Color.White;
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(34, 206);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            chart1.PaletteCustomColors = new Color[]
    {
    Color.FromArgb(26, 25, 62)
    };
            series1.ChartArea = "ChartArea1";
            series1.Color = Color.Lime;
            series1.LabelBackColor = Color.White;
            series1.LabelBorderColor = Color.FromArgb(26, 25, 62);
            series1.LabelForeColor = Color.White;
            series1.Legend = "Legend1";
            series1.Name = "Income";
            series2.ChartArea = "ChartArea1";
            series2.Color = Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Expenses";
            series2.YValuesPerPoint = 2;
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Size = new Size(478, 400);
            chart1.TabIndex = 7;
            chart1.Text = "Hello";
            title1.BackColor = Color.FromArgb(26, 25, 62);
            title1.ForeColor = Color.White;
            title1.Name = "Title1";
            title1.Text = "Financial Performance";
            chart1.Titles.Add(title1);
            chart1.Click += chart1_Click;
            // 
            // chart2
            // 
            chart2.BackColor = Color.FromArgb(90, 88, 140);
            chartArea2.BackColor = Color.FromArgb(90, 88, 140);
            chartArea2.BorderColor = Color.BlanchedAlmond;
            chartArea2.Name = "ChartArea1";
            chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart2.Legends.Add(legend2);
            chart2.Location = new Point(537, 206);
            chart2.Name = "chart2";
            chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Color = Color.FromArgb(90, 88, 140);
            series3.LabelForeColor = Color.FromArgb(90, 88, 140);
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.YValuesPerPoint = 2;
            chart2.Series.Add(series3);
            chart2.Size = new Size(354, 400);
            chart2.TabIndex = 8;
            chart2.Text = "chart2";
            chart2.Click += chart2_Click;
            // 
            // DashboardHomeControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(chart2);
            Controls.Add(chart1);
            Controls.Add(PendingCustomers);
            Controls.Add(TotalCustomer);
            Controls.Add(Today_Sale);
            Controls.Add(MonthlySale);
            Name = "DashboardHomeControl";
            Size = new Size(1103, 640);
            Load += DashboardHomeControl_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            Today_Sale.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            MonthlySale.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            TotalCustomer.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            PendingCustomers.ResumeLayout(false);
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel Today_Sale;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel6;
        private Label label1;
        private FlowLayoutPanel MonthlySale;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel5;
        private Label label4;
        private FlowLayoutPanel TotalCustomer;
        private FlowLayoutPanel flowLayoutPanel8;
        private Label label5;
        private FlowLayoutPanel flowLayoutPanel9;
        private Label label6;
        private FlowLayoutPanel PendingCustomers;
        private FlowLayoutPanel flowLayoutPanel11;
        private Label label7;
        private FlowLayoutPanel flowLayoutPanel12;
        private Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}
