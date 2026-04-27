using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING.Panels
{
    public partial class Dashboard : Form
    {
        private Button currentButton;
        private Panel leftBorderPanel;

        public Dashboard()
        {
            InitializeComponent();
            InitializeLeftBorder();
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
                if (previousBtn is Button btn)
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }
            }

            
            if (leftBorderPanel != null)
                leftBorderPanel.Visible = false;
        }

        
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currentButton = (Button)senderBtn;
                currentButton.BackColor = Color.FromArgb(37, 36, 81);
                currentButton.ForeColor = color;
                currentButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                
                leftBorderPanel.BackColor = color;
                leftBorderPanel.Location = new Point(0, currentButton.Top);
                leftBorderPanel.Height = currentButton.Height;
                leftBorderPanel.Visible = true;
                leftBorderPanel.BringToFront();
            }
        }

        
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }


        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnDASHBOARD_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
        }

        private void btnORDERS_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
        }

        private void btnINVENTORY_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
        }

        private void btnSALES_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void btnEXPENSES_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
        }

        private void btnREPORTS_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}