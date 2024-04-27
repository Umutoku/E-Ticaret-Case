using Eticaret.Order.Application.Commands;
using Eticaret.Order.Application.Dtos;
using Eticaret.Order.Application.Extensions;
using Eticaret.Order.Application.Queueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Shared.ControllerBases;

namespace Eticaret.Order.Api.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery { OrderId = id });
            return CreateActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var result = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> GenerateDataAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var orderItems = GenerateProduct.GenerateRandomOrderItems();
                var createOrderCommand = new CreateOrderCommand
                {
                    OrderItems = orderItems
                };

                await _mediator.Send(createOrderCommand, cancellationToken);

                Thread.Sleep(100); // Saniyede 60 veri için 1 saniye bekle
            }

            return Ok();
            
        }




    }
}
