using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.UI;
namespace Nesne_Proje
{
    public partial class Form1 : Form
    {
        private Kullanıcı yenikullanıcı;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            try
            {
                Kullanıcı kullaniciRepo = new Kullanıcı(@"Server=KERIM\SQLEXPRESS;Database=Site;Trusted_Connection=True;");

                Kullanıcı yeniKullanici = new Kullanıcı
                {
                    Username = txtUsername.Text,
                    PasswordHash = txtPassword.Text,
                    Email = txtEmail.Text,
                    Role = cmbRole.SelectedItem.ToString()
                };

                kullaniciRepo.Add(yeniKullanici);

                yenikullanıcı = yeniKullanici;

                MessageBox.Show("Kullanıcı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                return;
            }

            this.Hide();
            ___MainForm mainForm = new ___MainForm(yenikullanıcı, @"Server=KERIM\SQLEXPRESS;Database=Site;Trusted_Connection=True;");
            mainForm.ShowDialog();
        }


    }
}
