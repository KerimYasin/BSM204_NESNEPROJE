using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nesne_Proje.NESNE_CLASS.Services;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Configuration;




namespace Nesne_Proje.NESNE_CLASS.UI
{
    public partial class LoginForm : Form
    {
        private const string ConnStr = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";
        private readonly UserService _userService;


        public LoginForm()
        {
            InitializeComponent();
            _userService = new UserService(ConnStr);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text; 

                Kullanıcı user = _userService.AuthenticateUser(username, password);
                if (user == null)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Hide();
                var main = new ___MainForm(user, ConnStr);
                main.FormClosed += (s, args) => this.Close();
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 registerForm = new Form1();
            this.Hide(); 
            registerForm.ShowDialog();
            this.Show(); 
        }
    }
}

