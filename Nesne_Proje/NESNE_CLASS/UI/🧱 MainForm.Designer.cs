namespace Nesne_Proje.NESNE_CLASS.UI
{
    partial class ___MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.flpProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbKategoriler = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(447, 28);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(35, 13);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "label1";
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.Location = new System.Drawing.Point(66, 80);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(122, 51);
            this.btnManageProducts.TabIndex = 2;
            this.btnManageProducts.Text = "ÜRÜN YÖNETİMİ";
            this.btnManageProducts.UseVisualStyleBackColor = true;
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(228, 80);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(122, 51);
            this.btnViewCart.TabIndex = 3;
            this.btnViewCart.Text = "SEPET GÖRÜNTÜLE";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnViewCart_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(389, 80);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(122, 51);
            this.btnOrders.TabIndex = 4;
            this.btnOrders.Text = "SİPARİŞ GEÇMİŞİ";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // flpProducts
            // 
            this.flpProducts.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.flpProducts.Location = new System.Drawing.Point(24, 205);
            this.flpProducts.Name = "flpProducts";
            this.flpProducts.Size = new System.Drawing.Size(974, 471);
            this.flpProducts.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(694, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 51);
            this.button1.TabIndex = 6;
            this.button1.Text = "ÇIKIŞ YAP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbKategoriler
            // 
            this.cmbKategoriler.FormattingEnabled = true;
            this.cmbKategoriler.Location = new System.Drawing.Point(400, 154);
            this.cmbKategoriler.Name = "cmbKategoriler";
            this.cmbKategoriler.Size = new System.Drawing.Size(150, 21);
            this.cmbKategoriler.TabIndex = 7;
            this.cmbKategoriler.SelectedIndexChanged += new System.EventHandler(this.cmbKategoriler_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Kategori";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 51);
            this.button2.TabIndex = 9;
            this.button2.Text = "BAKİYE EKLE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ___MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 702);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbKategoriler);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flpProducts);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnViewCart);
            this.Controls.Add(this.btnManageProducts);
            this.Controls.Add(this.lblWelcome);
            this.Name = "___MainForm";
            this.Text = "___MainForm";
            this.Load += new System.EventHandler(this.___MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.FlowLayoutPanel flpProducts;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbKategoriler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}