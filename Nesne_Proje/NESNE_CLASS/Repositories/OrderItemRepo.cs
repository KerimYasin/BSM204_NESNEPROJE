using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Data.SqlClient;


namespace Nesne_Proje.NESNE_CLASS.Repositories
{
    public class OrderItemRepo
    {
        private readonly string _connectionString;

        public OrderItemRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OrderItem> GetItemsByOrderId(int siparisId)
        {
            List<OrderItem> items = new List<OrderItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SiparisKalemleri WHERE SiparisId = @SiparisId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SiparisId", siparisId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new OrderItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                SiparisId = Convert.ToInt32(reader["SiparisId"]),
                                UrunId = Convert.ToInt32(reader["UrunId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public void AddOrderItem(OrderItem item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO SiparisKalemleri (SiparisId, UrunId, Quantity, UnitPrice)
                                 VALUES (@SiparisId, @UrunId, @Quantity, @UnitPrice)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SiparisId", item.SiparisId);
                    cmd.Parameters.AddWithValue("@UrunId", item.UrunId);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItemsByOrderId(int siparisId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SiparisKalemleri WHERE SiparisId = @SiparisId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SiparisId", siparisId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
