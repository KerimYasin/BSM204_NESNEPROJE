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
    public class Category
    {
        private readonly string connectionString = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";

        public Category() { }
        public Category(string connString)
        {
            connectionString = connString;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category GetById(int id)
        {
            Category cat = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.Id, c.Name,
                           p.Id AS ProductId, p.Name AS ProductName, p.Description, p.Price, p.StockQuantity, p.CategoryId
                    FROM Categories c
                    LEFT JOIN Products p ON c.Id = p.CategoryId
                    WHERE c.Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (cat == null)
                        {
                            cat = new Category
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Products = new List<Product>()
                            };
                        }
                        if (reader["ProductId"] != DBNull.Value)
                        {
                            Product product = new Product(connectionString)
                            {
                                Id = (int)reader["ProductId"],
                                Name = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                StockQuantity = (int)reader["StockQuantity"],
                                CategoryId = (int)reader["CategoryId"]
                            };
                            cat.Products.Add(product);
                        }
                    }
                }
            }
            return cat;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category cat = new Category
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        };
                        categories.Add(cat);
                    }
                }
            }
            return categories;
        }
        public void Add()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Categories (Name) VALUES (@Name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete() 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Categories WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Update()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
