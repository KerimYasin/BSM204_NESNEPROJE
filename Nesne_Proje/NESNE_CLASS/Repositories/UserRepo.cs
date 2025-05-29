
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Data.SqlClient;

namespace Nesne_Proje.NESNE_CLASS.Repositories
{
    public class UserRepo
    {
        private readonly string _connectionString;

        public UserRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(Kullanıcı user)
        {
             var con = new SqlConnection(_connectionString);
            const string query = @"
                INSERT INTO Kullanicilar 
                    (Username, PasswordHash, Email, Role, Bakiye) 
                VALUES 
                    (@Username, @PasswordHash, @Email, @Role, @Bakiye)";
             var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@Bakiye", user.Bakiye);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public Kullanıcı GetUserById(int id)
        {
             var con = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM Kullanicilar WHERE Id = @Id";
             var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
             var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Kullanıcı
            {
                Id = (int)reader["Id"],
                Username = reader["Username"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Email = reader["Email"].ToString(),
                Role = reader["Role"].ToString(),
                Bakiye = (decimal)reader["Bakiye"]
            };
        }

        public List<Kullanıcı> GetAllUsers()
        {
            var users = new List<Kullanıcı>();
             var con = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM Kullanicilar";
             var cmd = new SqlCommand(query, con);
            con.Open();
             var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new Kullanıcı
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString(),
                    PasswordHash = reader["PasswordHash"].ToString(),
                    Email = reader["Email"].ToString(),
                    Role = reader["Role"].ToString(),
                    Bakiye = (decimal)reader["Bakiye"]
                });
            }
            return users;
        }

        public bool DeleteUser(int userId)
        {
             var conn = new SqlConnection(_connectionString);
            const string query = "DELETE FROM Kullanicilar WHERE Id = @Id";
             var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", userId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public Kullanıcı GetUserByCredentials(string username, string passwordHash)
        {
             var conn = new SqlConnection(_connectionString);
            const string query = @"
                SELECT * FROM Kullanicilar 
                WHERE Username = @Username 
                  AND PasswordHash = @PasswordHash";
             var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            conn.Open();
             var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Kullanıcı
            {
                Id = (int)reader["Id"],
                Username = reader["Username"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Email = reader["Email"].ToString(),
                Role = reader["Role"].ToString(),
                Bakiye = (decimal)reader["Bakiye"]
            };
        }

        public bool UpdateUser(Kullanıcı user)
        {
             var conn = new SqlConnection(_connectionString);
            const string query = @"
                UPDATE Kullanicilar
                SET Username = @Username,
                    Email    = @Email,
                    Role     = @Role,
                    Bakiye   = @Bakiye
                WHERE Id = @Id";
             var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@Bakiye", user.Bakiye);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateUserPassword(int userId, string newPasswordHash)
        {
             var conn = new SqlConnection(_connectionString);
            const string query = @"
                UPDATE Kullanicilar
                SET PasswordHash = @PasswordHash
                WHERE Id = @Id";
             var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
            cmd.Parameters.AddWithValue("@Id", userId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

