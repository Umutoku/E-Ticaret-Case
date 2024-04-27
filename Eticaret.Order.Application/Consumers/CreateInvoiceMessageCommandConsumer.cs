using ETicaret.Order.Infrastructure;
using MassTransit;
using Order.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Eticaret.Order.Application.Extensions;

namespace Eticaret.Order.Application.Consumers
{
    public class CreateInvoiceMessageCommandConsumer : IConsumer<CreateInvoiceMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        private const string _smtpServer ="";
        private const int _port =0;
        private MailAddress _username = new MailAddress(address: "deneme@deneme.com");
        private SecureString _password = new SecureString();

        public CreateInvoiceMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateInvoiceMessageCommand> context)
        {
            var message = context.Message;
            var invoice = new Domain.Invoice
            {
                Id = message.InvoiceId,
            };

            context.Message.InvoiceItems.ForEach(x =>
            {
                invoice.AddInvoiceItem(quantity: x.Quantity, unitPrice: x.UnitPrice, productId: x.ProductId, productName: x.ProductName);
            });

            await _orderDbContext.Invoices.AddAsync(invoice);
            await _orderDbContext.SaveChangesAsync();

            var emailSender = new EmailSender("smtp.gmail.com", 587, "example@gmail.com", "your_password");
            //await emailSender.SendEmailAsync("recipient@example.com", "Test Subject", "This is a test email.");
            await emailSender.SendOrderCreatedEmailAsync("gidenmail@gmail.com", context);

        }
    }
    }

