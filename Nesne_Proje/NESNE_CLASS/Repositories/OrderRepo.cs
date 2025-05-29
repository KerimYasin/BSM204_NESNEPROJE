using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Data.SqlClient;


namespace Nesne_Proje.NESNE_CLASS.Repositories
{
    public class OrderRepo
    {
        private readonly string _connectionString;

        public OrderRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

    
        public int AddOrder(OrderEntity order, List<OrderItem> items)
        {
            int orderId;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string orderQuery = @"INSERT INTO Siparisler (KullaniciId, OrderDate, TotalAmount, Status) 
                                              OUTPUT INSERTED.Id 
                                              VALUES (@KullaniciId, @OrderDate, @TotalAmount, @Status)";
                        using (SqlCommand cmd = new SqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@KullaniciId", order.KullaniciId);
                            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                            cmd.Parameters.AddWithValue("@Status", order.Status ?? "Hazırlanıyor");

                            orderId = (int)cmd.ExecuteScalar();
                        }

                        string itemQuery = @"INSERT INTO SiparisKalemleri (SiparisId, UrunId, Quantity, UnitPrice) 
                                             VALUES (@SiparisId, @UrunId, @Quantity, @UnitPrice)";
                        foreach (var item in items)
                        {
                            using (SqlCommand cmd = new SqlCommand(itemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SiparisId", orderId);
                                cmd.Parameters.AddWithValue("@UrunId", item.UrunId);
                                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return orderId;
        }

        
        public List<OrderEntity> GetOrdersByUserId(int userId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Siparisler WHERE KullaniciId = @UserId ORDER BY OrderDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new OrderEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                KullaniciId = Convert.ToInt32(reader["KullaniciId"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }

            return orders;
        }

      
        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Siparisler SET Status = @Status WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@Id", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

      
        public List<OrderItem> GetOrderItems(int orderId)
        {
            List<OrderItem> items = new List<OrderItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SiparisKalemleri WHERE SiparisId = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

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


        public int AddOrder(OrderEntity order)
        {
            int newOrderId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Siparisler (KullaniciId, OrderDate, TotalAmount, Status)
                                 VALUES (@KullaniciId, @OrderDate, @TotalAmount, @Status);
                                 SELECT CAST(scope_identity() AS int);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciId", order.KullaniciId);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@Status", order.Status);

                    newOrderId = (int)cmd.ExecuteScalar();
                }
            }

            return newOrderId;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO SiparisKalemleri (SiparisId, UrunId, Quantity, UnitPrice)
                                 VALUES (@SiparisId, @UrunId, @Quantity, @UnitPrice);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SiparisId", orderItem.SiparisId);
                    cmd.Parameters.AddWithValue("@UrunId", orderItem.UrunId);
                    cmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@UnitPrice", orderItem.UnitPrice);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public OrderEntity GetOrderById(int orderId)
        {
            OrderEntity order = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM Siparisler WHERE Id = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new OrderEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                KullaniciId = Convert.ToInt32(reader["KullaniciId"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            return order;
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM SiparisKalemleri WHERE SiparisId = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new OrderItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                SiparisId = Convert.ToInt32(reader["SiparisId"]),
                                UrunId = Convert.ToInt32(reader["UrunId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                            };
                            orderItems.Add(item);
                        }
                    }
                }
            }

            return orderItems;
        }

        public int CreateOrder(OrderEntity order, List<OrderItem> items)
        {
            int orderId;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string orderQuery = @"
                        INSERT INTO Siparisler (KullaniciId, OrderDate, TotalAmount, Status) 
                        OUTPUT INSERTED.Id 
                        VALUES (@KullaniciId, @OrderDate, @TotalAmount, @Status)";

                        using (SqlCommand cmd = new SqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@KullaniciId", order.KullaniciId);
                            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                            cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                            cmd.Parameters.AddWithValue("@Status", order.Status ?? "Hazırlanıyor");

                            orderId = (int)cmd.ExecuteScalar();
                        }

                        string itemQuery = @"
                        INSERT INTO SiparisKalemleri (SiparisId, UrunId, Quantity, UnitPrice) 
                        VALUES (@SiparisId, @UrunId, @Quantity, @UnitPrice)";

                        foreach (var item in items)
                        {
                            using (SqlCommand cmd = new SqlCommand(itemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SiparisId", orderId);
                                cmd.Parameters.AddWithValue("@UrunId", item.UrunId);
                                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return orderId;
        }

        
    }
}
