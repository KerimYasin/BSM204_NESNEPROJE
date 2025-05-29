using Nesne_Proje.NESNE_CLASS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Reflection;

namespace Nesne_Proje.NESNE_CLASS.Repositories
{
    public class CategoryRepo
    {
        private readonly string _connectionString;

        public CategoryRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

     

        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name FROM Kategoriler";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
            return categories;
        }

        public void AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Kategoriler (Name) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", category.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Kategoriler WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Category> GetAll()
        {
            var categories = new List<Category>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name FROM Kategoriler";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var category = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };

                    categories.Add(category);
                }

                reader.Close();
            }

            return categories;
        }
    }
}
