using System;
using System.Windows.Forms;
using FLAVSMAGS_TAILORING.Panels; 

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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string username = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();

            
            string correctUsername = "admin";
            string correctPassword = "1234";

            
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            
            if (username == correctUsername && password == correctPassword)
            {
               
                Dashboard dashboard = new Dashboard();
                dashboard.Show();

                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
    }
}