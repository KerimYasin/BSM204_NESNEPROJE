using Nesne_Proje.NESNE_CLASS.Services;
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
    public partial class CartForm : Form
    {
        private readonly CartService _cartService;
        private readonly OrderService _orderService;
        private int _userId;
        private List<CartItem> _cartItems;
        private Kullanıcı currentUser;
        private string connectionString;
        private readonly UserRepo _userRepo;   
        private Kullanıcı _currentUser;                


       

        public CartForm(int userId, string connectionString)
        {
            InitializeComponent();
            _userId = userId;
            this.connectionString = connectionString;
            _userRepo = new UserRepo(connectionString);
            _currentUser = _userRepo.GetUserById(_userId);
            var cartItemRepo = new CartItemRepo(connectionString);
            var productRepo = new ProductRepo(connectionString);
            var orderRepo = new OrderRepo(connectionString);

            _cartService = new CartService(cartItemRepo, productRepo);
            var userRepo = new UserRepo(connectionString);

            _orderService = new OrderService(orderRepo, cartItemRepo, productRepo, _userRepo);
        }


        public CartForm(Kullanıcı currentUser)
        {
            this.currentUser = currentUser;
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            LoadCartItems();

        }

        private void LoadCartItems()
        {
            _cartItems = _cartService.GetCartItems(_userId);
            dgvCartItems.DataSource = null;
            dgvCartItems.DataSource = _cartItems;

            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in _cartItems)
            {
                var product = new ProductService().GetProductById(item.UrunId);
                total += product.Price * item.Quantity;
            }
            lblTotalAmount.Text = $"Toplam Tutar: {total:C2}";
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCartItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen çıkarılacak ürünü seçiniz.");
                return;
            }

            var selectedItem = (CartItem)dgvCartItems.SelectedRows[0].DataBoundItem;
            _cartService.RemoveFromCart(_userId, selectedItem.UrunId);

            LoadCartItems();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (_cartItems == null || _cartItems.Count == 0)
            {
                MessageBox.Show("Sepetiniz boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _cartItems.Sum(item =>
            {
                var p = new ProductRepo(connectionString).GetProductById(item.UrunId);
                return p.Price * item.Quantity;
            });

            if (_currentUser.Bakiye < total)
            {
                MessageBox.Show("Yetersiz bakiye. Sepeti tamamlayamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _orderService.AddOrder(_userId, _cartItems);

                _currentUser.Bakiye -= total;
                _userRepo.UpdateUser(_currentUser);

                MessageBox.Show("Siparişiniz başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cartService.ClearCart(_userId);
                LoadCartItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnUpdateQuantity_Click(object sender, EventArgs e)
        {

        }
    }
}
