using Eticaret.Order.Application.Commands;
using Eticaret.Order.Application.Dtos;
using Eticaret.Order.Domain;
using ETicaret.Order.Infrastructure;
using MediatR;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Handlers
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CustomResponse<CreatedInvoiceDto>>
    {
        private readonly OrderDbContext _context;

        public CreateInvoiceCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse<CreatedInvoiceDto>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var newInvoice = new Invoice
            {
                InvoiceItems = request.InvoiceItems.Select(x => new InvoiceItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };

            await _context.Invoices.AddAsync(newInvoice);

            await _context.SaveChangesAsync();

            return CustomResponse<CreatedInvoiceDto>.Success(new CreatedInvoiceDto { InvoiceId = newInvoice.Id }, 200);
        }
    }
}
