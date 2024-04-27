using ETicaret.Order.Infrastructure;
using MassTransit;
using Order.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var message = context.Message;
            var order = new Domain.Order
            {
                Id = message.OrderId,
            };

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(quantity: x.Quantity, price: x.Price, productId: x.ProductId,productName: x.ProductName);
            });

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
