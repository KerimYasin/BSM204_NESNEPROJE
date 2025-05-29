using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Data.SqlClient;


namespace Nesne_Proje.NESNE_CLASS.Repositories
{
    public class CartItemRepo
    {
        private readonly string _connectionString;

        public CartItemRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

       
        public List<CartItem> GetCartItemsByUserId(int userId)
        {
            List<CartItem> items = new List<CartItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SepetKalemleri WHERE KullaniciId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new CartItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                KullaniciId = Convert.ToInt32(reader["KullaniciId"]),
                                UrunId = Convert.ToInt32(reader["UrunId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public void AddCartItem(CartItem item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO SepetKalemleri (KullaniciId, UrunId, Quantity)
                                 VALUES (@KullaniciId, @UrunId, @Quantity)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciId", item.KullaniciId);
                    cmd.Parameters.AddWithValue("@UrunId", item.UrunId);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCartItem(CartItem item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"UPDATE SepetKalemleri SET Quantity = @Quantity 
                                 WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCartItem(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SepetKalemleri WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void ClearCartByUserId(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SepetKalemleri WHERE KullaniciId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public CartItem GetCartItemByUserAndProduct(int userId, int productId)
        {
            CartItem cartItem = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SepetKalemleri WHERE KullaniciId = @UserId AND UrunId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cartItem = new CartItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                KullaniciId = Convert.ToInt32(reader["KullaniciId"]),
                                UrunId = Convert.ToInt32(reader["UrunId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };
                        }
                    }
                }
            }

            return cartItem;
        }

        internal List<CartItem> GetCartItemsByUser(int userId)
        {
            List<CartItem> items = new List<CartItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SepetKalemleri WHERE KullaniciId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new CartItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                KullaniciId = Convert.ToInt32(reader["KullaniciId"]),
                                UrunId = Convert.ToInt32(reader["UrunId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public void DeleteAllCartItemsForUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SepetKalemleri WHERE KullaniciId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
