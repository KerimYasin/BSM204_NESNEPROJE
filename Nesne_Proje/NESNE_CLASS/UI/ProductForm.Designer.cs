namespace Nesne_Proje.NESNE_CLASS.UI
{
    partial class ProductForm
    {
        // **************************************************
        // 1) Component Container
        // **************************************************
        private System.ComponentModel.IContainer components = null;

        // **************************************************
        // 2) Form Controls Declarations
        // (Burada form üzerinde kullanılan tüm kontroller tanımlı)
        // **************************************************
        private System.Windows.Forms.DataGridView dgvProductList;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox resimyolu;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        // **************************************************
        // 3) Dispose Method
        // **************************************************
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // **************************************************
        // 4) InitializeComponent Method
        //    — Burada kontroller initialize edilir,
        //      özellikleri ayarlanır ve form'a eklenir.
        // **************************************************
        private void InitializeComponent()
        {
            this.dgvProductList = new System.Windows.Forms.DataGridView();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resimyolu = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductList
            // 
            this.dgvProductList.AllowUserToAddRows = false;
            this.dgvProductList.AllowUserToDeleteRows = false;
            this.dgvProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductList.Location = new System.Drawing.Point(10, 241);
            this.dgvProductList.Name = "dgvProductList";
            this.dgvProductList.ReadOnly = true;
            this.dgvProductList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductList.Size = new System.Drawing.Size(600, 200);
            this.dgvProductList.TabIndex = 0;
            this.dgvProductList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductList_CellContentClick);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "Telefon",
            "Kol",
            "Oyun"});
            this.cmbCategory.Location = new System.Drawing.Point(100, 12);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(180, 21);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(100, 50);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(180, 20);
            this.txtProductName.TabIndex = 2;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(100, 90);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(180, 20);
            this.txtPrice.TabIndex = 3;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(100, 130);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(180, 20);
            this.txtStock.TabIndex = 4;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(320, 10);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(100, 30);
            this.btnAddProduct.TabIndex = 5;
            this.btnAddProduct.Text = "Ekle";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.Location = new System.Drawing.Point(320, 50);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateProduct.TabIndex = 6;
            this.btnUpdateProduct.Text = "Güncelle";
            this.btnUpdateProduct.UseVisualStyleBackColor = true;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Location = new System.Drawing.Point(320, 90);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteProduct.TabIndex = 7;
            this.btnDeleteProduct.Text = "Sil";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(320, 130);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Temizle";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(450, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // resimyolu
            // 
            this.resimyolu.Location = new System.Drawing.Point(450, 180);
            this.resimyolu.Name = "resimyolu";
            this.resimyolu.ReadOnly = true;
            this.resimyolu.Size = new System.Drawing.Size(160, 20);
            this.resimyolu.TabIndex = 10;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(450, 205);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(160, 30);
            this.btnSelectImage.TabIndex = 11;
            this.btnSelectImage.Text = "Resim Seç";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            this.openFileDialog1.Title = "Bir ürün görseli seçiniz";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "FİYAT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "ÜRÜN ADI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "STOK SAYISI";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 15;
            this.button1.Text = "ANA MENÜ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProductForm
            // 
            this.ClientSize = new System.Drawing.Size(624, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.resimyolu);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDeleteProduct);
            this.Controls.Add(this.btnUpdateProduct);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.dgvProductList);
            this.Name = "ProductForm";
            this.Text = "Ürün Yönetimi";
            this.Load += new System.EventHandler(this.ProductForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}
