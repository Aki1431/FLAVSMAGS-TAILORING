namespace FLAVSMAGS_TAILORING.UserControls
{
    partial class InventoryControl
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
            panelTotalValue = new RoundedPanel();
            lblTotalValue = new Label();
            label1 = new Label();
            panelActiveMaterials = new RoundedPanel();
            lblActiveMaterials = new Label();
            label2 = new Label();
            panelMonthlyConsumption = new RoundedPanel();
            lblMonthlyConsumption = new Label();
            label3 = new Label();
            dgvMaterials = new DataGridView();
            roundedPanel1 = new RoundedPanel();
            panelTotalValue.SuspendLayout();
            panelActiveMaterials.SuspendLayout();
            panelMonthlyConsumption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaterials).BeginInit();
            SuspendLayout();
            // 
            // panelTotalValue
            // 
            panelTotalValue.BackColor = Color.FromArgb(90, 88, 140);
            panelTotalValue.Controls.Add(lblTotalValue);
            panelTotalValue.Controls.Add(label1);
            panelTotalValue.Location = new Point(72, 23);
            panelTotalValue.Name = "panelTotalValue";
            panelTotalValue.Size = new Size(227, 99);
            panelTotalValue.TabIndex = 0;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.ForeColor = Color.White;
            lblTotalValue.Location = new Point(24, 55);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(79, 20);
            lblTotalValue.TabIndex = 1;
            lblTotalValue.Text = "$45,250.00";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(26, 25, 62);
            label1.ForeColor = Color.White;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 0;
            label1.Text = "Total Value";
            // 
            // panelActiveMaterials
            // 
            panelActiveMaterials.BackColor = Color.FromArgb(90, 88, 140);
            panelActiveMaterials.Controls.Add(lblActiveMaterials);
            panelActiveMaterials.Controls.Add(label2);
            panelActiveMaterials.Location = new Point(332, 23);
            panelActiveMaterials.Name = "panelActiveMaterials";
            panelActiveMaterials.Size = new Size(274, 99);
            panelActiveMaterials.TabIndex = 1;
            // 
            // lblActiveMaterials
            // 
            lblActiveMaterials.AutoSize = true;
            lblActiveMaterials.ForeColor = Color.White;
            lblActiveMaterials.Location = new Point(23, 55);
            lblActiveMaterials.Name = "lblActiveMaterials";
            lblActiveMaterials.Size = new Size(33, 20);
            lblActiveMaterials.TabIndex = 2;
            lblActiveMaterials.Text = "156";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(26, 25, 62);
            label2.ForeColor = Color.White;
            label2.Location = new Point(23, 19);
            label2.Name = "label2";
            label2.Size = new Size(115, 20);
            label2.TabIndex = 1;
            label2.Text = "Active Materials";
            // 
            // panelMonthlyConsumption
            // 
            panelMonthlyConsumption.BackColor = Color.FromArgb(90, 88, 140);
            panelMonthlyConsumption.Controls.Add(lblMonthlyConsumption);
            panelMonthlyConsumption.Controls.Add(label3);
            panelMonthlyConsumption.Location = new Point(645, 23);
            panelMonthlyConsumption.Name = "panelMonthlyConsumption";
            panelMonthlyConsumption.Size = new Size(242, 99);
            panelMonthlyConsumption.TabIndex = 2;
            // 
            // lblMonthlyConsumption
            // 
            lblMonthlyConsumption.AutoSize = true;
            lblMonthlyConsumption.ForeColor = Color.White;
            lblMonthlyConsumption.Location = new Point(23, 55);
            lblMonthlyConsumption.Name = "lblMonthlyConsumption";
            lblMonthlyConsumption.Size = new Size(79, 20);
            lblMonthlyConsumption.TabIndex = 3;
            lblMonthlyConsumption.Text = "$12,350.00";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(26, 25, 62);
            label3.ForeColor = Color.White;
            label3.Location = new Point(23, 19);
            label3.Name = "label3";
            label3.Size = new Size(155, 20);
            label3.TabIndex = 2;
            label3.Text = "Monthly Consumption";
            // 
            // dgvMaterials
            // 
            dgvMaterials.AllowUserToAddRows = false;
            dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaterials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterials.Location = new Point(94, 179);
            dgvMaterials.Name = "dgvMaterials";
            dgvMaterials.ReadOnly = true;
            dgvMaterials.RowHeadersVisible = false;
            dgvMaterials.RowHeadersWidth = 51;
            dgvMaterials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterials.Size = new Size(768, 417);
            dgvMaterials.TabIndex = 3;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(90, 88, 140);
            roundedPanel1.Location = new Point(72, 158);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(815, 463);
            roundedPanel1.TabIndex = 4;
            // 
            // InventoryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 25, 62);
            Controls.Add(dgvMaterials);
            Controls.Add(panelMonthlyConsumption);
            Controls.Add(panelActiveMaterials);
            Controls.Add(panelTotalValue);
            Controls.Add(roundedPanel1);
            Name = "InventoryControl";
            Size = new Size(949, 640);
            panelTotalValue.ResumeLayout(false);
            panelTotalValue.PerformLayout();
            panelActiveMaterials.ResumeLayout(false);
            panelActiveMaterials.PerformLayout();
            panelMonthlyConsumption.ResumeLayout(false);
            panelMonthlyConsumption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaterials).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel panelTotalValue;
        private RoundedPanel panelActiveMaterials;
        private RoundedPanel panelMonthlyConsumption;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblTotalValue;
        private Label lblActiveMaterials;
        private Label lblMonthlyConsumption;
        private DataGridView dgvMaterials;
        private RoundedPanel roundedPanel1;
    }
}
