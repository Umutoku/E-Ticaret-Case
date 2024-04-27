using Eticaret.Order.Application.Dtos;
using MassTransit.Mediator;
using MediatR;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Queueries
{
    public class GetOrderByIdQuery : IRequest<CustomResponse<OrderDto>>
    {
        public int OrderId { get; set; }

    }
}
