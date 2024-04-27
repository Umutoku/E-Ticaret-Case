using Eticaret.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Domain
{
    public class Invoice : Entity
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public decimal InvoiceTotal { get => InvoiceItems.Sum(ii => ii.TotalPrice); }

        public void AddInvoiceItem(int quantity, decimal unitPrice, int productId, string productName)
        {
            InvoiceItems.Add(new InvoiceItem
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                ProductName = productName
            });
        }
    }



}
