using Eticaret.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Domain
{
    public class OrderItem : Entity
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal TotalPrice { get => Quantity * UnitPrice; }

        public OrderItem()
        {

        }

        public OrderItem(int productId, string productName, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = price;
            Quantity = quantity;
        }
    }
}
