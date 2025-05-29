using Nesne_Proje.NESNE_CLASS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nesne_Proje
{
    public class CartItem
    {
  

        private readonly string connectionString;

        public CartItem() { }
        public CartItem(string connString)
        {
            connectionString = connString;
        }

        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int UrunId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Kullanıcı Kullanıcı { get; set; }

        public List<CartItem> GetByUserId(int userId)
        {
            List<CartItem> items = new List<CartItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ci.Id, ci.KullaniciId, ci.UrunId, ci.Quantity,
                           p.Name, p.Description, p.Price, p.StockQuantity, p.CategoryId
                    FROM SepetKalemleri ci
                    LEFT JOIN Urunler p ON ci.UrunId = p.Id
                    WHERE ci.KullaniciId = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItem item = new CartItem(connectionString)
                        {
                            Id = (int)reader["Id"],
                            KullaniciId = (int)reader["KullaniciId"],
                            UrunId = (int)reader["UrunId"],
                            Quantity = (int)reader["Quantity"],
                            Product = new Product(connectionString)
                            {
                                Id = (int)reader["UrunId"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                StockQuantity = (int)reader["StockQuantity"]
                            }
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO SepetKalemleri (KullaniciId, UrunId, Quantity)
                    VALUES (@UserId, @ProductId, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateCartItem(int id, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE SepetKalemleri
                    SET Quantity = @Quantity
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void RemoveFromCart(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM SepetKalemleri
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCartItem(int id) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM SepetKalemleri
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearCart(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM SepetKalemleri
                    WHERE KullaniciId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
