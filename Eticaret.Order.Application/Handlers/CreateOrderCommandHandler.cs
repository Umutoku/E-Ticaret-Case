using Eticaret.Order.Application.Commands;
using Eticaret.Order.Application.Dtos;
using ETicaret.Order.Infrastructure;
using MassTransit;
using MediatR;
using Order.Shared.DTOs;
using Order.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CustomResponse<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CreateOrderCommandHandler(OrderDbContext context, ISendEndpointProvider sendEndpointProvider)
        {
            _context = context;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task<CustomResponse<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var newOrder = new Domain.Order();

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(quantity: x.Quantity, price: x.UnitPrice, productId: x.ProductId, productName: x.ProductName);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            var invoiceCommand = new CreateInvoiceMessageCommand
            {
                OrderId = newOrder.Id,
                InvoiceItems = request.OrderItems.Select(x => new InvoiceItem
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                    
                }).ToList()
            };

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-invoice-service"));


            await sendEndpoint.Send<CreateInvoiceMessageCommand>(invoiceCommand);


            return CustomResponse<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
