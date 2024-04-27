using Eticaret.Order.Application.Dtos;
using MediatR;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<CustomResponse<CreatedOrderDto>>
    {
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
