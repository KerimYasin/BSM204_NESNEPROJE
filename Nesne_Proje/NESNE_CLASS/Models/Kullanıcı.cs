
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Nesne_Proje
{
    public class Kullanıcı
    {
        private readonly string connectionString = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";

        public Kullanıcı() { }

        public Kullanıcı(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Kullanıcı GetById(int id)
        {
            Kullanıcı user = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT k.Id, k.Username, k.PasswordHash, k.Email, k.Role, k.Bakiye,
                       s.Id AS SiparisId, s.OrderDate, s.TotalAmount, s.Status
                FROM Kullanicilar k
                LEFT JOIN Siparisler s ON k.Id = s.KullaniciId
                WHERE k.Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (user == null)
                        {
                            user = new Kullanıcı
                            {
                                Id = (int)reader["Id"],
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Email = reader["Email"].ToString(),
                                Role = reader["Role"].ToString(),
                                Bakiye = (decimal)reader["Bakiye"],        // <-- Yeni eklendi
                            };
                        }
                        if (reader["SiparisId"] != DBNull.Value)
                        {
                            Order order = new Order
                            {
                                Id = (int)reader["SiparisId"],
                                UserId = user.Id,
                                OrderDate = (DateTime)reader["OrderDate"],
                                TotalAmount = (decimal)reader["TotalAmount"],
                                Status = reader["Status"].ToString()
                            };
                            user.Orders.Add(order);
                        }
                    }
                }
            }
            return user;
        }

        public List<Kullanıcı> GetAll()
        {
            var users = new List<Kullanıcı>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Kullanicilar";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new Kullanıcı
                        {
                            Id = (int)reader["Id"],
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Email = reader["Email"].ToString(),
                            Role = reader["Role"].ToString(),
                            Bakiye = (decimal)reader["Bakiye"],          
                            Orders = new List<Order>()
                        });
                    }
                }
            }

            return users;
        }

        public void Add(Kullanıcı user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Kullanicilar (Username, PasswordHash, Email, Role, Bakiye) VALUES (@Username, @PasswordHash, @Email, @Role, @Bakiye)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@Bakiye", user.Bakiye);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Kullanıcı user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Kullanicilar SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email, Role = @Role, Bakiye = @Bakiye WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@Bakiye", user.Bakiye);          
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Kullanicilar WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } 
        public decimal Bakiye { get; set; }   

  
        public List<Order> Orders { get; set; }
    }
}

