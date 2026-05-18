using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using FLAVSMAGS_TAILORING.Data;
using FLAVSMAGS_TAILORING.Models;
using FLAVSMAGS_TAILORING.Panels;

namespace FLAVSMAGS_TAILORING
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            // Pressing Enter anywhere on the form clicks the login button
            this.AcceptButton = button1;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            UserSession.Instance.Logout();
            txtName.Focus();                    // convenient starting point
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthenticateUser(username, password))
            {
                UserSession.Instance.Login(username);
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqliteCommand(
                "SELECT COUNT(*) FROM Users WHERE Username = @u AND PasswordHash = @p", conn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                object? result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return false;
                long count = Convert.ToInt64(result);
                return count > 0;
            }
        }

        // empty designer event handlers (kept for compatibility)
        private void label1_Click(object sender, EventArgs e) { }
        private void txtName_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}