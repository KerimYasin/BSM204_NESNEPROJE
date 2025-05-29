namespace Nesne_Proje.NESNE_CLASS.UI
{
    partial class CartForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnUpdateQuantity = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvCartItems = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(538, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TUTAR";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(638, 381);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(35, 13);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "label2";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(92, 282);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(106, 66);
            this.btnRemoveItem.TabIndex = 2;
            this.btnRemoveItem.Text = "ÜRÜNÜ ÇIKAR";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnUpdateQuantity
            // 
            this.btnUpdateQuantity.Location = new System.Drawing.Point(241, 282);
            this.btnUpdateQuantity.Name = "btnUpdateQuantity";
            this.btnUpdateQuantity.Size = new System.Drawing.Size(106, 66);
            this.btnUpdateQuantity.TabIndex = 3;
            this.btnUpdateQuantity.Text = "SEPETİ GÜNCELLE";
            this.btnUpdateQuantity.UseVisualStyleBackColor = true;
            this.btnUpdateQuantity.Click += new System.EventHandler(this.btnUpdateQuantity_Click);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.Location = new System.Drawing.Point(556, 282);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(106, 61);
            this.btnPlaceOrder.TabIndex = 4;
            this.btnPlaceOrder.Text = "SİPARİŞ ET";
            this.btnPlaceOrder.UseVisualStyleBackColor = true;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(167, 377);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 61);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "KAPAT";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvCartItems
            // 
            this.dgvCartItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartItems.Location = new System.Drawing.Point(33, 12);
            this.dgvCartItems.Name = "dgvCartItems";
            this.dgvCartItems.Size = new System.Drawing.Size(735, 241);
            this.dgvCartItems.TabIndex = 6;
            // 
            // CartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvCartItems);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnUpdateQuantity);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.label1);
            this.Name = "CartForm";
            this.Text = "CartForm";
            this.Load += new System.EventHandler(this.CartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnUpdateQuantity;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvCartItems;
    }
}