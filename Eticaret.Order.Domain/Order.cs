using Eticaret.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Order.Domain.Core;

namespace Eticaret.Order.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public Order()
        {
            _orderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;
            OrderStatus = "New";
        }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } 

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(int productId, string productName, int quantity, decimal price)
        {
            _orderItems.Add(new OrderItem(productId,productName, price,quantity));
        }

    }
}
