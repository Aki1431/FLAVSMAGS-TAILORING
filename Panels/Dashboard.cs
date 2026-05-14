using FLAVSMAGS_TAILORING.UserControls;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING.Panels
{
    public partial class Dashboard : Form
    {
        private Button? currentButton;
        private Panel? leftBorderPanel;
        private UserControl? currentChildControl;

        public Dashboard()
        {
            InitializeComponent();
            InitializeLeftBorder();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            // Fix form size - cannot resize
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
        }

        private void InitializeLeftBorder()
        {
            leftBorderPanel = new Panel();
            leftBorderPanel.Size = new Size(7, 50);
            leftBorderPanel.Visible = false;
            this.Controls.Add(leftBorderPanel);
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in this.Controls)
            {
                if (previousBtn is Button btn && btn.Name != "btnSignOut")
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

                    if (btn is IconButton iconBtn)
                    {
                        iconBtn.IconColor = Color.Black;
                    }
                }
            }

            if (leftBorderPanel != null)
                leftBorderPanel.Visible = false;
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn == null) return;

            DisableButton();

            currentButton = (Button)senderBtn;
            currentButton.BackColor = Color.FromArgb(37, 36, 81);
            currentButton.ForeColor = color;
            currentButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (currentButton is IconButton iconBtn)
            {
                iconBtn.IconColor = color;
                iconCurrentChildForm.IconChar = iconBtn.IconChar;
            }
            else
            {
                iconCurrentChildForm.IconChar = IconChar.None;
            }

            iconCurrentChildForm.IconColor = color;

            if (leftBorderPanel != null)
            {
                leftBorderPanel.BackColor = color;
                leftBorderPanel.Location = new Point(0, currentButton.Top);
                leftBorderPanel.Height = currentButton.Height;
                leftBorderPanel.Visible = true;
                leftBorderPanel.BringToFront();
            }
        }

        private void OpenChildControl(UserControl childControl)
        {
            if (currentChildControl != null)
            {
                panelContainer.Controls.Remove(currentChildControl);
                currentChildControl.Dispose();
            }

            currentChildControl = childControl;
            childControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childControl);
            childControl.BringToFront();
            lblTitleChildForm.Text = childControl.Text;
        }

        private struct RGBColors
        {
            public static readonly Color color1 = Color.FromArgb(172, 126, 241);
            public static readonly Color color2 = Color.FromArgb(249, 118, 176);
            public static readonly Color color3 = Color.FromArgb(253, 138, 114);
            public static readonly Color color4 = Color.FromArgb(95, 77, 221);
            public static readonly Color color5 = Color.FromArgb(249, 88, 155);
            public static readonly Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void Dashboard_Load(object sender, EventArgs e) { }

        private void btnDASHBOARD_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildControl(new DashboardHomeControl());
        }

        private void btnORDERS_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildControl(new OrdersControl());
        }

        private void btnINVENTORY_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildControl(new InventoryControl());
        }

        private void btnSALES_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildControl(new SalesControl());
        }

        private void btnEXPENSES_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildControl(new ExpensesControl());
        }

        private void btnREPORTS_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildControl(new ReportsControl());
        }

        private void iconPictureBox1_Click(object sender, EventArgs e) { }

        private void Reset()
        {
            DisableButton();
            if (leftBorderPanel != null)
                leftBorderPanel.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_Paint(object sender, PaintEventArgs e) { }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        // FIXED Sign Out button - Goes back to Login form
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "Confirm Sign Out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Close the Dashboard
                this.Close();

                // Open the Login form
                Login loginForm = new Login();
                loginForm.Show();
            }
        }
    }
}