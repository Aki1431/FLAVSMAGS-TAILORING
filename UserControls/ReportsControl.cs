using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;                    // <-- required for GlobalFontSettings
using FLAVSMAGS_TAILORING.Services;

namespace FLAVSMAGS_TAILORING.UserControls
{
    public partial class ReportsControl : UserControl
    {
        private PrintDocument? printDocument;
        private DataTable? currentReportData;
        private string currentReportTitle = "";
        private readonly ReportService _reportService;

        public ReportsControl()
        {
            InitializeComponent();
            this.Text = "Reports";
            _reportService = new ReportService();
            SetupEvents();
            SetupPrintDocument();
            LoadOrdersReport();
        }

        private void SetupEvents()
        {
            btnGenerateReport.Click += BtnGenerateReport_Click!;
            btnPrintReport.Click += BtnPrintReport_Click!;
            btnExportPDF.Click += BtnExportPDF_Click!;
        }

        private void SetupPrintDocument()
        {
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void BtnGenerateReport_Click(object? sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null) return;
            string reportType = cmbReportType.SelectedItem.ToString()!;
            switch (reportType)
            {
                case "Orders": LoadOrdersReport(); break;
                case "Sales": LoadSalesReport(); break;
                case "Inventory": LoadInventoryReport(); break;
                case "Expenses": LoadExpensesReport(); break;
            }
        }

        // ---------- Real data loading methods (unchanged) ----------
        private void LoadOrdersReport()
        {
            try
            {
                currentReportTitle = "Orders Report";
                currentReportData = _reportService.GenerateOrdersReport();
                dgvReportPreview.DataSource = currentReportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load orders report: {ex.Message}", "Report Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesReport()
        {
            try
            {
                currentReportTitle = "Sales Report";
                currentReportData = _reportService.GenerateSalesReport();
                dgvReportPreview.DataSource = currentReportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load sales report: {ex.Message}", "Report Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryReport()
        {
            try
            {
                currentReportTitle = "Inventory Report";
                currentReportData = _reportService.GenerateInventoryReport();
                dgvReportPreview.DataSource = currentReportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load inventory report: {ex.Message}", "Report Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExpensesReport()
        {
            try
            {
                currentReportTitle = "Expenses Report";
                currentReportData = _reportService.GenerateExpensesReport();
                dgvReportPreview.DataSource = currentReportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load expenses report: {ex.Message}", "Report Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Print & Export (Print unchanged, Export now fixed) ----------
        private void BtnPrintReport_Click(object? sender, EventArgs e)
        {
            if (currentReportData == null || currentReportData.Rows.Count == 0)
            {
                MessageBox.Show("Please generate a report first.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument?.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (currentReportData == null) return;
            using (Font titleFont = new Font("Arial", 14, FontStyle.Bold))
            using (Font headerFont = new Font("Arial", 10, FontStyle.Bold))
            using (Font cellFont = new Font("Arial", 9, FontStyle.Regular))
            {
                int y = 50, x = 50, rowHeight = 25, colWidth = 120;
                e.Graphics!.DrawString(currentReportTitle, titleFont, Brushes.Black, x, y);
                y += 40;
                e.Graphics.DrawString($"Date: {DateTime.Now:MM/dd/yyyy}", cellFont, Brushes.Black, x, y);
                y += 30;
                for (int i = 0; i < currentReportData.Columns.Count; i++)
                {
                    e.Graphics.DrawString(currentReportData.Columns[i].ColumnName ?? "", headerFont, Brushes.Black, x + (i * colWidth), y);
                }
                y += rowHeight;
                for (int row = 0; row < currentReportData.Rows.Count; row++)
                {
                    for (int col = 0; col < currentReportData.Columns.Count; col++)
                    {
                        e.Graphics.DrawString(currentReportData.Rows[row][col]?.ToString() ?? "", cellFont, Brushes.Black, x + (col * colWidth), y);
                    }
                    y += rowHeight;
                    if (y > e.MarginBounds.Bottom - 50) { e.HasMorePages = true; return; }
                }
                e.HasMorePages = false;
            }
        }

        private void BtnExportPDF_Click(object? sender, EventArgs e)
        {
            if (currentReportData == null || currentReportData.Rows.Count == 0)
            {
                MessageBox.Show("Please generate a report first.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files|*.pdf";
            saveDialog.Title = "Save Report";
            saveDialog.FileName = $"{currentReportTitle}_{DateTime.Now:yyyyMMdd}.pdf";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPDF(saveDialog.FileName);
                MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportToPDF(string filePath)
        {
            // ---- FIX: enable Windows font resolver for PDFsharp Core ----
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            }

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                page.Width = XUnit.FromMillimeter(210);
                page.Height = XUnit.FromMillimeter(297);
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    XFont titleFont = new XFont("Arial", 14);
                    XFont headerFont = new XFont("Arial", 10);
                    XFont cellFont = new XFont("Arial", 9);
                    int y = 50, x = 40, colWidth = 120;
                    gfx.DrawString(currentReportTitle, titleFont, XBrushes.Black, x, y);
                    y += 40;
                    gfx.DrawString($"Date: {DateTime.Now:MM/dd/yyyy}", cellFont, XBrushes.Black, x, y);
                    y += 30;
                    for (int i = 0; i < currentReportData!.Columns.Count; i++)
                    {
                        gfx.DrawString(currentReportData.Columns[i].ColumnName ?? "", headerFont, XBrushes.Black, x + (i * colWidth), y);
                    }
                    y += 25;
                    for (int row = 0; row < currentReportData.Rows.Count; row++)
                    {
                        for (int col = 0; col < currentReportData.Columns.Count; col++)
                        {
                            gfx.DrawString(currentReportData.Rows[row][col]?.ToString() ?? "", cellFont, XBrushes.Black, x + (col * colWidth), y);
                        }
                        y += 25;
                    }
                }
                document.Save(filePath);
            }
        }
    }
}