using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;

namespace Nesne_Proje.NESNE_CLASS.Services
{
    public class ProductService
    {
        private readonly ProductRepo _productRepo;

        public ProductService()
        {
            _productRepo = new ProductRepo();

        }

        public ProductService(string connectionString)
        {
            _productRepo = new ProductRepo(connectionString);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            return _productRepo.GetProductById(productId);
        }

        public bool IsProductInStock(int productId, int quantity)
        {
            var product = _productRepo.GetProductById(productId);
            if (product == null)
                return false;
            return product.Stock >= quantity;
        }
    }
}
