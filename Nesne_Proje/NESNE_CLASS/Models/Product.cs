using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nesne_Proje.NESNE_CLASS.Models
{
    public class Product
    {
        private readonly string connectionString = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";

        public Product(string connString)
        {
            connectionString = connString;
        }
        public Product() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public int Stock { get; internal set; }
        public string ImagePath { get; internal set; }
        
        public Product GetById(int id)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.CategoryId,
                           c.Id AS CatId, c.Name AS CatName
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.Id
                    WHERE p.Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            StockQuantity = (int)reader["StockQuantity"],
                            CategoryId = (int)reader["CategoryId"],
                            Category = new Category
                            {
                                Id = (int)reader["CatId"],
                                Name = reader["CatName"].ToString()
                            }
                        };
                    }
                }
            }
            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            StockQuantity = (int)reader["StockQuantity"],
                            CategoryId = (int)reader["CategoryId"]
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
        public void Add()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Description, Price, StockQuantity, CategoryId) VALUES (@Name, @Description, @Price, @StockQuantity, @CategoryId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@StockQuantity", StockQuantity);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete() 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE Id = @Id";
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
                string query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, CategoryId = @CategoryId WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@StockQuantity", StockQuantity);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
