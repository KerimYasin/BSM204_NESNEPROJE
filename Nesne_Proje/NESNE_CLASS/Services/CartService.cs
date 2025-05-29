using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;



namespace Nesne_Proje.NESNE_CLASS.Services
{
    public class CartService
    {
        private readonly CartItemRepo _cartItemRepo;
        private readonly ProductRepo _productRepo;

        public CartService(CartItemRepo cartItemRepo, ProductRepo productRepo)
        {
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
        }

        public CartService()
        {
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Miktar 0'dan büyük olmalıdır.");

            Product product = _productRepo.GetProductById(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            if (product.Stock < quantity)
                throw new Exception("Yeterli stok bulunmamaktadır.");

            CartItem existingCartItem = _cartItemRepo.GetCartItemByUserAndProduct(userId, productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;

                if (existingCartItem.Quantity > product.Stock)
                    throw new Exception("Sepetteki toplam ürün miktarı stoktan fazla olamaz.");

                _cartItemRepo.UpdateCartItem(existingCartItem);
            }
            else
            {
                CartItem newCartItem = new CartItem
                {
                    KullaniciId = userId,
                    UrunId = productId,
                    Quantity = quantity
                };
                _cartItemRepo.AddCartItem(newCartItem);
            }
        }

        public void RemoveFromCart(int userId, int productId)
        {
            CartItem existingCartItem = _cartItemRepo.GetCartItemByUserAndProduct(userId, productId);
            if (existingCartItem == null)
                throw new Exception("Sepette bu ürün bulunmamaktadır.");

            _cartItemRepo.DeleteCartItem(existingCartItem.Id);
        }

        public List<CartItem> GetCartItems(int userId)
        {
            return _cartItemRepo.GetCartItemsByUser(userId);
        }

        public decimal GetCartTotal(int userId)
        {
            List<CartItem> cartItems = GetCartItems(userId);
            decimal total = 0;

            foreach (var item in cartItems)
            {
                Product product = _productRepo.GetProductById(item.UrunId);
                if (product != null)
                {
                    total += product.Price * item.Quantity;
                }
            }

            return total;
        }
        public void ClearCart(int userId)
        {
            _cartItemRepo.DeleteAllCartItemsForUser(userId);
        }

    }
}
