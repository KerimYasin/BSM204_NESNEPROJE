using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;

namespace Nesne_Proje.NESNE_CLASS.Services
{
    public class OrderService
    {

        private readonly OrderRepo _orderRepo;
        private readonly CartItemRepo _cartItemRepo;
        private readonly ProductRepo _productRepo;
        private readonly UserRepo _userRepo;

    

        public OrderService(OrderRepo orderRepo, CartItemRepo cartItemRepo, ProductRepo productRepo, UserRepo userRepo)
        {
            _orderRepo = orderRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;

        }

        public void CreateOrder(int userId)
        {
            var user = _userRepo.GetUserById(userId)
                      ?? throw new Exception("Kullanıcı bulunamadı.");

            var cartItems = _cartItemRepo.GetCartItemsByUserId(userId);
            if (cartItems.Count == 0)
            {
                throw new InvalidOperationException("Sepetiniz boş.");
            }

            decimal totalAmount = 0;
            foreach (var item in cartItems)
            {
                var product = _productRepo.GetProductById(item.UrunId);
                if (product == null)
                {
                    throw new Exception($"Ürün bulunamadı. Id: {item.UrunId}");
                }
                if (product.Stock < item.Quantity)
                {
                    throw new Exception($"Ürün stokta yeterli değil: {product.Name}");
                }
                totalAmount += product.Price * item.Quantity;
            }

            if (user.Bakiye < totalAmount)
                throw new InvalidOperationException("Yetersiz bakiye. Siparişi tamamlayamazsınız.");


            var order = new OrderEntity
            {
                KullaniciId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Hazırlanıyor"
            };

            int orderId = _orderRepo.AddOrder(order); 

            foreach (var item in cartItems)
            {
                var product = _productRepo.GetProductById(item.UrunId);

                _orderRepo.AddOrderItem(new OrderItem
                {
                    SiparisId = orderId,
                    UrunId = item.UrunId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                product.Stock -= item.Quantity;
                _productRepo.UpdateProduct(product);
            }

            user.Bakiye -= totalAmount;
            _userRepo.UpdateUser(user);

            _cartItemRepo.ClearCartByUserId(userId);
        }

        public OrderEntity GetOrderById(int orderId)
        {
            return _orderRepo.GetOrderById(orderId);
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            return _orderRepo.GetOrderItemsByOrderId(orderId);
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            _orderRepo.UpdateOrderStatus(orderId, newStatus);
        }

        internal void AddOrder(int userId, List<CartItem> cartItems)
        {
            var userRepo = _userRepo.GetUserById(userId);

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("Sepetiniz boş.");
            }

            decimal totalAmount = 0;
            foreach (var item in cartItems)
            {
                var product = _productRepo.GetProductById(item.UrunId);
                if (product == null)
                {
                    throw new Exception($"Ürün bulunamadı. Id: {item.UrunId}");
                }
                if (product.Stock < item.Quantity)
                {
                    throw new Exception($"Ürün stokta yeterli değil: {product.Name}");
                }
                totalAmount += product.Price * item.Quantity;
            }

            var order = new OrderEntity
            {
                KullaniciId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Hazırlanıyor"
            };

            int orderId = _orderRepo.AddOrder(order); 

            foreach (var item in cartItems)
            {
                var product = _productRepo.GetProductById(item.UrunId);

                _orderRepo.AddOrderItem(new OrderItem
                {
                    SiparisId = orderId,
                    UrunId = item.UrunId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                if (userRepo.Bakiye < totalAmount)
                    throw new Exception("Yetersiz bakiye.");

                userRepo.Bakiye -= totalAmount;
                _userRepo.UpdateUser(userRepo);

                product.Stock -= item.Quantity;
                _productRepo.UpdateProduct(product);
            }

            _cartItemRepo.ClearCartByUserId(userId);
        }
    }
}
