using Eticaret.Order.Application.Dtos;
using Eticaret.Order.Application.Queueries;
using ETicaret.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Handlers
{
    public class GetOrderByOrderIdQueryHandler : IRequestHandler<GetOrderByIdQuery, CustomResponse<OrderDto>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByOrderIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<CustomResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null)
            {
                return CustomResponse<OrderDto>.Fail("Order not found", 404);
            }
            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };

            return CustomResponse<OrderDto>.Success(orderDto, 200);
        }
    }

}

