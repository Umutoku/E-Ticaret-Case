using Eticaret.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Extensions
{
    public static class GenerateProduct
    {
        public static List<OrderItemDto> GenerateRandomOrderItems()
        {
            var orderItems = new List<OrderItemDto>();
            for (int i = 0; i < 200; i++) // Her komutta 10 ürün
            {
                var randomProductId = new Random().Next(1, 100); // Rastgele ürün kimliği
                var randomProductName = $"Ürün {randomProductId}"; // Rastgele ürün adı
                var randomQuantity = new Random().Next(1, 10); // Rastgele ürün miktarı
                var randomUnitPrice = new Random().Next(1, 100); // Rastgele ürün birim fiyatı

                orderItems.Add(new OrderItemDto
                {
                    ProductId = randomProductId,
                    ProductName = randomProductName,
                    Quantity = randomQuantity,
                    UnitPrice = randomUnitPrice
                });
            }

            return orderItems;
        }
    }
}
