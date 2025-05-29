using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace Nesne_Proje.NESNE_CLASS.Repositories
{

    public class ProductRepo
    {
        private readonly string _connectionString = @"Data Source=KERIM\SQLEXPRESS;Initial Catalog=Site;Integrated Security=True";

        
        public ProductRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProductRepo()
        {
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Urunler";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            ImagePath = reader["ImagePath"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"])
                        });
                    }
                }
                
            }

            return products;
        }

        public void AddProduct(Product product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
            INSERT INTO Urunler (Name, Price, Stock, ImagePath, CategoryId)
            VALUES (@Name, @Price, @Stock, @ImagePath, @CategoryId)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"UPDATE Urunler SET 
                                    Name = @Name, 
                                    Price = @Price, 
                                    Stock = @Stock, 
                                    ImagePath = @ImagePath, 
                                    CategoryId = @CategoryId 
                                 WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Urunler WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Urunler WHERE CategoryId = @CategoryId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                CategoryId = Convert.ToInt32(reader["CategoryId"])
                            });
                        }
                    }
                }
            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Urunler WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                CategoryId = Convert.ToInt32(reader["CategoryId"])
                            };
                        }
                    }
                }
            }

            return product;
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Urunler WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Price = (decimal)reader["Price"],
                        Stock = (int)reader["Stock"],
                        ImagePath = reader["ImagePath"].ToString(),
                        CategoryId = (int)reader["CategoryId"]
                    });
                }
                conn.Close();
            }

            return products;
        }



    }
}

