using System;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Panels;
using FLAVSMAGS_TAILORING.Models;

namespace FLAVSMAGS_TAILORING
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';

            // Clear any existing session
            UserSession.Instance.Logout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Authentication logic (can be upgraded to database later)
            if (AuthenticateUser(username, password))
            {
                // Create user session
                UserSession.Instance.Login(username);

                // Open Dashboard
                Dashboard dashboard = new Dashboard();
                dashboard.Show();

                // Hide login form
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password field for security
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// Authenticate user credentials
        /// TODO: Replace with database authentication
        /// </summary>
        private bool AuthenticateUser(string username, string password)
        {
            // Hardcoded credentials (upgrade to DB later)
            return username == "admin" && password == "1234";
        }

        // Keep existing empty event handlers for Designer compatibility
        private void label1_Click(object sender, EventArgs e) { }
        private void txtName_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}