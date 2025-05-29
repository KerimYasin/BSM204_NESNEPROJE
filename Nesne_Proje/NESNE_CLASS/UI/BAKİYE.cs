using Nesne_Proje.NESNE_CLASS.Repositories;
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

namespace Nesne_Proje.NESNE_CLASS.UI
{
    public partial class BAKİYE : Form
    {
        private readonly Kullanıcı _user;
        private readonly UserRepo _userRepo;
        public BAKİYE(Kullanıcı user, UserRepo userRepo)
        {
            InitializeComponent();
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));

            lblCurrentBalance.Text = $"Mevcut Bakiye: {_user.Bakiye:C2}";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void BAKİYE_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out var miktar) || miktar <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir tutar giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _user.Bakiye += miktar;
            if (_userRepo.UpdateUser(_user))
            {
                MessageBox.Show($"Yeni bakiye: {_user.Bakiye:C2}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCurrentBalance.Text = $"Mevcut Bakiye: {_user.Bakiye:C2}";
                txtAmount.Clear();
            }
            else
            {
                MessageBox.Show("Bakiye güncellenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new ___MainForm();
            mainForm.Show();
        }
    }
    }

