using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Services;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;



namespace Nesne_Proje
{

    public class Order
    {
        private readonly string connectionString = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";


        public Order() { }
        public Order(string connString)
        {
            connectionString = connString;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } 

       
        public Kullanıcı Kullanıcı { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order GetById(int id)
        {
            Order order = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT o.Id, o.UserId, o.OrderDate, o.TotalAmount, o.Status,
                           oi.Id AS OrderItemId, oi.ProductId, oi.Quantity,
                           p.Name AS ProductName, p.Description, p.Price
                    FROM Siparisler o
                    LEFT JOIN OrderItems oi ON o.Id = oi.OrderId
                    LEFT JOIN Urunler p ON oi.ProductId = p.Id
                    WHERE o.Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (order == null)
                        {
                            order = new Order
                            {
                                Id = (int)reader["Id"],
                                UserId = (int)reader["UserId"],
                                OrderDate = (DateTime)reader["OrderDate"],
                                TotalAmount = (decimal)reader["TotalAmount"],
                                Status = reader["Status"].ToString(),
                                OrderItems = new List<OrderItem>()
                            };
                        }

                        if (reader["OrderItemId"] != DBNull.Value)
                        {
                            OrderItem item = new OrderItem
                            {
                                Id = (int)reader["OrderItemId"],
                                OrderId = order.Id,
                                ProductId = (int)reader["ProductId"],
                                Quantity = (int)reader["Quantity"],
                                Product = new Product(connectionString)
                                {
                                    Id = (int)reader["ProductId"],
                                    Name = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (decimal)reader["Price"]
                                }
                            };
                            order.OrderItems.Add(item);
                        }
                    }
                }
            }
            return order;
        }

        public List<Order> GetByUserId(int userId)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT o.Id, o.UserId, o.OrderDate, o.TotalAmount, o.Status,
                           oi.Id AS OrderItemId, oi.ProductId, oi.Quantity,
                           p.Name AS ProductName, p.Description, p.Price
                    FROM Siparisler o
                    LEFT JOIN OrderItems oi ON o.Id = oi.OrderId
                    LEFT JOIN Urunler p ON oi.ProductId = p.Id
                    WHERE o.UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"],
                            Status = reader["Status"].ToString(),
                            OrderItems = new List<OrderItem>()
                        };
                        if (reader["OrderItemId"] != DBNull.Value)
                        {
                            OrderItem item = new OrderItem
                            {
                                Id = (int)reader["OrderItemId"],
                                OrderId = order.Id,
                                ProductId = (int)reader["ProductId"],
                                Quantity = (int)reader["Quantity"],
                                Product = new Product(connectionString)
                                {
                                    Id = (int)reader["ProductId"],
                                    Name = reader["ProductName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (decimal)reader["Price"]
                                }
                            };
                            order.OrderItems.Add(item);
                        }
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }
        public void AddOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Siparisler (UserId, OrderDate, TotalAmount, Status)
                    VALUES (@UserId, @OrderDate, @TotalAmount, @Status);
                    SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", order.UserId);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                conn.Open();
                order.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void DeleteOrder(Order order) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Siparisler WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", order.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Siparisler
                    SET UserId = @UserId, OrderDate = @OrderDate, TotalAmount = @TotalAmount, Status = @Status
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", order.Id);
                cmd.Parameters.AddWithValue("@UserId", order.UserId);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddOrderItem(OrderItem item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO OrderItems (OrderId, ProductId, Quantity)
                    VALUES (@OrderId, @ProductId, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderId", item.OrderId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrderItem(OrderItem item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM OrderItems WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateOrderItem(OrderItem item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE OrderItems
                    SET OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity
                    WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@OrderId", item.OrderId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }

}
