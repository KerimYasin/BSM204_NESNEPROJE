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
using Nesne_Proje.NESNE_CLASS.Repositories;



namespace Nesne_Proje.NESNE_CLASS.UI
{
    public partial class OrderHistoryForm : Form
    {
       

        private readonly OrderRepo _orderRepo;
        private readonly int _currentUserId; 
        private Kullanıcı currentUser;

        public OrderHistoryForm(string connectionString, int currentUserId)
        {
            InitializeComponent();

            _orderRepo = new OrderRepo(connectionString);
            _currentUserId = currentUserId;

            LoadOrders();
        }

        public OrderHistoryForm(Kullanıcı currentUser)
        {
            this.currentUser = currentUser;
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _orderRepo.GetOrdersByUserId(_currentUserId);

                dgvOrders.DataSource = orders;

                dgvOrders.Columns["Id"].HeaderText = "Sipariş No";
                dgvOrders.Columns["KullaniciId"].Visible = false;
                dgvOrders.Columns["OrderDate"].HeaderText = "Sipariş Tarihi";
                dgvOrders.Columns["TotalAmount"].HeaderText = "Toplam Tutar";
                dgvOrders.Columns["Status"].HeaderText = "Durum";

                dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvOrders.MultiSelect = false;
                dgvOrders.ReadOnly = true;

                dgvOrders.ClearSelection();

                ClearOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Siparişler yüklenirken hata oluştu: " + ex.Message);
            }
        }


        private void OrderHistoryForm_Load(object sender, EventArgs e)
        {

        }

       

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                ClearOrderDetails();
                return;
            }

            int selectedOrderId = (int)dgvOrders.SelectedRows[0].Cells["Id"].Value;

            LoadOrderDetails(selectedOrderId);
        }

        private void LoadOrderDetails(int orderId)
        {
            try
            {
                var order = _orderRepo.GetOrderById(orderId);
                if (order == null)
                {
                    ClearOrderDetails();
                    return;
                }

                lblStatus.Text = "Durum: " + order.Status;
                lblTotal.Text = "Toplam Tutar: " + order.TotalAmount.ToString("C");

                var items = _orderRepo.GetOrderItemsByOrderId(orderId);
                dgvOrderItems.DataSource = items;

                dgvOrderItems.Columns["Id"].Visible = false;
                dgvOrderItems.Columns["SiparisId"].Visible = false;
                dgvOrderItems.Columns["UrunId"].HeaderText = "Ürün No";
                dgvOrderItems.Columns["Quantity"].HeaderText = "Adet";
                dgvOrderItems.Columns["UnitPrice"].HeaderText = "Birim Fiyat";

                dgvOrderItems.ReadOnly = true;
                dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvOrderItems.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sipariş detayları yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void ClearOrderDetails()
        {
            lblStatus.Text = "Durum: ";
            lblTotal.Text = "Toplam Tutar: ";
            dgvOrderItems.DataSource = null;
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;

            int orderId = (int)dgvOrders.SelectedRows[0].Cells["Id"].Value;
            string newStatus = "Kargoya Verildi"; 

            try
            {
                _orderRepo.UpdateOrderStatus(orderId, newStatus);
                MessageBox.Show("Sipariş durumu güncellendi.");
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durum güncellenirken hata oluştu: " + ex.Message);
            }
        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new ___MainForm(); 
            mainForm.Show(); 
        }
    }
}
