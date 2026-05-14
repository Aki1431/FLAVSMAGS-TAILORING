namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class ReportsControl
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
            panelReportOptions = new RoundedPanel();
            panelPreview = new RoundedPanel();
            dgvReportPreview = new DataGridView();
            label1 = new Label();
            label3 = new Label();
            btnGenerateReport = new Button();
            cmbReportType = new ComboBox();
            btnPrintReport = new Button();
            btnExportPDF = new Button();
            panelReportOptions.SuspendLayout();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportPreview).BeginInit();
            SuspendLayout();
            // 
            // panelReportOptions
            // 
            panelReportOptions.BackColor = Color.FromArgb(90, 88, 140);
            panelReportOptions.Controls.Add(btnExportPDF);
            panelReportOptions.Controls.Add(btnPrintReport);
            panelReportOptions.Controls.Add(cmbReportType);
            panelReportOptions.Controls.Add(btnGenerateReport);
            panelReportOptions.Controls.Add(label3);
            panelReportOptions.Location = new Point(44, 41);
            panelReportOptions.Name = "panelReportOptions";
            panelReportOptions.Size = new Size(862, 167);
            panelReportOptions.TabIndex = 0;
            // 
            // panelPreview
            // 
            panelPreview.BackColor = Color.FromArgb(90, 88, 140);
            panelPreview.Controls.Add(dgvReportPreview);
            panelPreview.Location = new Point(44, 232);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(862, 368);
            panelPreview.TabIndex = 1;
            // 
            // dgvReportPreview
            // 
            dgvReportPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportPreview.Location = new Point(33, 38);
            dgvReportPreview.Name = "dgvReportPreview";
            dgvReportPreview.RowHeadersWidth = 51;
            dgvReportPreview.Size = new Size(801, 295);
            dgvReportPreview.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(44, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 0;
            label1.Text = "REPORTS";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(33, 77);
            label3.Name = "label3";
            label3.Size = new Size(96, 20);
            label3.TabIndex = 2;
            label3.Text = "Report Type :";
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.Location = new Point(33, 23);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(140, 29);
            btnGenerateReport.TabIndex = 3;
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // cmbReportType
            // 
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Location = new Point(135, 74);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(224, 28);
            cmbReportType.TabIndex = 4;
            // 
            // btnPrintReport
            // 
            btnPrintReport.Location = new Point(532, 23);
            btnPrintReport.Name = "btnPrintReport";
            btnPrintReport.Size = new Size(140, 29);
            btnPrintReport.TabIndex = 5;
            btnPrintReport.Text = "Print";
            btnPrintReport.UseVisualStyleBackColor = true;
            // 
            // btnExportPDF
            // 
            btnExportPDF.Location = new Point(694, 23);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(140, 29);
            btnExportPDF.TabIndex = 6;
            btnExportPDF.Text = "Export";
            btnExportPDF.UseVisualStyleBackColor = true;
            // 
            // ReportsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(panelPreview);
            Controls.Add(panelReportOptions);
            Controls.Add(label1);
            Name = "ReportsControl";
            Size = new Size(949, 640);
            panelReportOptions.ResumeLayout(false);
            panelReportOptions.PerformLayout();
            panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReportPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedPanel panelReportOptions;
        private RoundedPanel panelPreview;
        private DataGridView dgvReportPreview;
        private Button btnGenerateReport;
        private Label label3;
        private Label label1;
        private Button btnExportPDF;
        private Button btnPrintReport;
        private ComboBox cmbReportType;
    }
}
