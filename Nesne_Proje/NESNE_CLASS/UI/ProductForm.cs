using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;

namespace Nesne_Proje.NESNE_CLASS.UI
{
    public partial class ProductForm : Form
    {
        private readonly string _connectionString;
        private readonly ProductRepo _productRepo;
        private readonly CategoryRepo _categoryRepo;
        private int? selectedProductId = null;

        public ProductForm(string connectionString)
        {
            _connectionString = connectionString;
            
            InitializeComponent();

            _productRepo = new ProductRepo(_connectionString);
            _categoryRepo = new CategoryRepo(_connectionString);
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);

        }

        public ProductForm()
        {
        }

        

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
            ClearForm();
        }

        private void LoadCategories()
        {
            var categories = _categoryRepo.GetAllCategories();
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
            cmbCategory.SelectedIndex = -1;
        }

        private void LoadProducts()
        {
            var products = _productRepo.GetAllProducts();
            dgvProductList.DataSource = products;
            dgvProductList.ClearSelection();
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvProductList.Rows[e.RowIndex];
            selectedProductId = Convert.ToInt32(row.Cells["Id"].Value);
            txtProductName.Text = row.Cells["Name"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            txtStock.Text = row.Cells["Stock"].Value.ToString();
            cmbCategory.SelectedValue = row.Cells["CategoryId"].Value;

            if (row.Cells["ImagePath"].Value != null)
            {
                string imgPath = Path.Combine(Application.StartupPath, row.Cells["ImagePath"].Value.ToString());
                if (File.Exists(imgPath))
                    pictureBox1.Image = System.Drawing.Image.FromFile(imgPath);
                else
                    pictureBox1.Image = null;
                resimyolu.Text = row.Cells["ImagePath"].Value.ToString();
            }
            else
            {
                pictureBox1.Image = null;
                resimyolu.Clear();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Ürün adı boş olamaz.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz.");
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Geçerli bir stok miktarı giriniz.");
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen kategori seçiniz.");
                return;
            }

            var product = new Product
            {
                Name = txtProductName.Text.Trim(),
                Price = price,
                Stock = stock,
                CategoryId = (int)cmbCategory.SelectedValue,
                ImagePath = resimyolu.Text.Trim()
            };

            _productRepo.AddProduct(product);
            LoadProducts();
            ClearForm();
            MessageBox.Show("Ürün başarıyla eklendi.");
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductId == null)
            {
                MessageBox.Show("Lütfen güncellenecek ürünü seçiniz.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz.");
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Geçerli bir stok miktarı giriniz.");
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen kategori seçiniz.");
                return;
            }

            var product = new Product
            {
                Id = selectedProductId.Value,
                Name = txtProductName.Text.Trim(),
                Price = price,
                Stock = stock,
                CategoryId = (int)cmbCategory.SelectedValue,
                ImagePath = resimyolu.Text.Trim()
            };

            _productRepo.UpdateProduct(product);
            LoadProducts();
            ClearForm();
            MessageBox.Show("Ürün başarıyla güncellendi.");
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductId == null)
            {
                MessageBox.Show("Lütfen silinecek ürünü seçiniz.");
                return;
            }

            _productRepo.DeleteProduct(selectedProductId.Value);
            LoadProducts();
            ClearForm();
            MessageBox.Show("Ürün başarıyla silindi.");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedProductId = null;
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                resimyolu.Text = openFileDialog1.FileName;

                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new ___MainForm();
        }
    }
}