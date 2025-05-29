using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;
using Nesne_Proje.NESNE_CLASS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nesne_Proje
{
    public partial class UserControl1 : UserControl
    {
        private Product _product;
        private Action<Product> _addToCartCallback;
        private string _connectionString;
        private Kullanıcı _currentUser;


       
        private void addToCartCallback(Product product)
        {
            throw new NotImplementedException();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        public UserControl1(Product product, Action<Product> addToCartCallback, string connectionString, Kullanıcı currentUser)
        {
            InitializeComponent();
            _product = product;
            _addToCartCallback = addToCartCallback;
            _connectionString = connectionString;
            _currentUser = currentUser;
            LoadProductInfo();
            LoadProductData();
        }

        private void LoadProductData()
        {
            lblName.Text = _product.Name;
            lblPrice.Text = $"₺{_product.Price}";
            lblStock.Text = $"Stok: {_product.Stock}";

            if (!string.IsNullOrEmpty(_product.ImagePath))
            {
                pictureBox1.ImageLocation = _product.ImagePath;
            }
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
                MessageBox.Show("Sepete eklerken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductInfo()
        {
            lblName.Text = _product.Name;
            lblPrice.Text = $"Fiyat: {_product.Price:C2}";
            lblStock.Text = $"Stok: {_product.Stock}";

            if (!string.IsNullOrWhiteSpace(_product.ImagePath))
            {
                var full = Path.Combine(Application.StartupPath, _product.ImagePath);
                if (File.Exists(full))
                    pictureBox1.Image = Image.FromFile(full);
            }

            btnAddToCart.Click += (s, e) =>
            {
                if (_product.Stock > 0)
                    _addToCartCallback(_product);
                else
                    MessageBox.Show("Ürün stokta yok!");
            };
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {

        }
       

    }
}
