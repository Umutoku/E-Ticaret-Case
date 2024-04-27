using Eticaret.Order.Application.Dtos;
using Eticaret.Order.Domain;
using MediatR;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Order.Application.Commands
{
    public class CreateInvoiceCommand : IRequest<CustomResponse<CreatedInvoiceDto>>
    {
        public List<InvoiceItemDto> InvoiceItems { get; set; }
    }
}
