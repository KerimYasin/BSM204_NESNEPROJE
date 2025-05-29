using System;
using System.Windows.Forms;
using Nesne_Proje.NESNE_CLASS.Services;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace Nesne_Proje.NESNE_CLASS.UI
{
    public partial class ___MainForm : Form
    {
        private readonly string _connectionString;
        private readonly ProductService _productService;
        private readonly Kullanıcı _currentUser;
        private OrderService _orderService;
        private CategoryRepo _categoryRepo;
        private ProductRepo _productRepo;
        private readonly UserRepo _userRepo;         



        public ___MainForm(Kullanıcı currentUser, string connectionString)
        {
            if (currentUser == null)
                throw new ArgumentNullException(nameof(currentUser), "Kullanıcı bilgisi null olamaz!");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Geçerli bir connectionString giriniz.", nameof(connectionString));

            InitializeComponent();

            _currentUser = currentUser;
            _connectionString = connectionString;

            _productRepo = new ProductRepo(connectionString);
            _categoryRepo = new CategoryRepo(connectionString);

            var cartItemRepo = new CartItemRepo(connectionString);
            var orderRepo = new OrderRepo(connectionString);
            var userRepo = new UserRepo(connectionString); 
            _orderService = new OrderService(orderRepo, cartItemRepo, _productRepo, _userRepo);
            _productService = new ProductService(_connectionString);
            _userRepo = new UserRepo(_connectionString);

        }

        public ___MainForm()
        {
        }

        private void ___MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Hoşgeldiniz, {_currentUser.Username}";
            LoadProducts();

            var kategoriler = _categoryRepo.GetAll().ToList();

            kategoriler.Insert(0, new Category { Id = 4, Name = "Tümü" });

            cmbKategoriler.DisplayMember = "Name";
            cmbKategoriler.ValueMember = "Id";
            cmbKategoriler.DataSource = kategoriler;

        }

        private void LoadProducts()
        {
            flpProducts.Controls.Clear();
            var products = _productService.GetAllProducts();

            if (products == null || !products.Any())
            {
                return;
            }

            foreach (var product in products)
            {
var productCard = new UserControl1(product, AddToCart, _connectionString, _currentUser);
                flpProducts.Controls.Add(productCard);
            }
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            if (_currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                var pf = new ProductForm(_connectionString);
                pf.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Sadece yöneticiler ürünleri yönetebilir.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            var cartForm = new CartForm(_currentUser.Id, _connectionString);
            cartForm.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            var orderForm = new OrderHistoryForm(_connectionString, _currentUser.Id);
            orderForm.ShowDialog();
        }

        private void AddToCart(Product product)
        {
            try
            {
                var cartItemRepo = new CartItemRepo(_connectionString);
                var productRepo = new ProductRepo(_connectionString);
                var cartService = new CartService(cartItemRepo, productRepo);

                cartService.AddToCart(_currentUser.Id, product.Id, 1);
                MessageBox.Show($"{product.Name} sepete eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sepete eklerken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            var loginForm = new LoginForm();
            loginForm.Show(); 
        }

        private void cmbKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKategoriler.SelectedItem is Category selectedCategory)
            {
                int categoryId = selectedCategory.Id;

                IEnumerable<Product> urunler;

                if (categoryId == 4) 
                {
                    urunler = _productService.GetAllProducts();
                }
                else
                {
                    urunler = _productRepo.GetByCategoryId(categoryId);
                }

                flpProducts.Controls.Clear();

                foreach (var urun in urunler)
                {
                    var urunCard = new UserControl1(
                        urun,
                        this.AddToCart,
                        _connectionString,
                        _currentUser
                    );

                    flpProducts.Controls.Add(urunCard);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             var bakiyeForm = new BAKİYE(_currentUser, _userRepo);
            bakiyeForm.ShowDialog();

            lblWelcome.Text = $"Hoşgeldiniz, {_currentUser.Username} (Bakiye: {_currentUser.Bakiye:C2})";

        }
    }
}
