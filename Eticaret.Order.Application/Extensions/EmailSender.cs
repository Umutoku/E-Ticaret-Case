using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Order.Shared.Messages;

namespace Eticaret.Order.Application.Extensions
{
    public class EmailSender
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(string smtpServer, int port, string username, string password)
        {
            _smtpServer = smtpServer;
            _port = port;
            _username = username;
            _password = password;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_username, _password);
                client.EnableSsl = true;

                var message = new MailMessage(_username, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                try
                {
                    await client.SendMailAsync(message);
                    Console.WriteLine("E-posta gönderildi!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"E-posta gönderilirken hata oluştu: {ex.Message}");
                }
            }
        }

        public async Task SendOrderCreatedEmailAsync(string toEmail, ConsumeContext<CreateInvoiceMessageCommand> context)
        {
            using (var client = new SmtpClient(_smtpServer, _port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_username, _password);
                client.EnableSsl = true;

                var message = new MailMessage(_username, toEmail)
                {
                    Subject = "Siparişiniz oluşturuldu",
                    IsBodyHtml = true
                };

                var bodyBuilder = new StringBuilder();
                bodyBuilder.AppendLine("<h1>Siparişiniz oluşturuldu</h1>");
                bodyBuilder.AppendLine("<h2>Sipariş Detayları</h2>");
                bodyBuilder.AppendLine("<ul>");

                foreach (var item in context.Message.InvoiceItems)
                {
                    bodyBuilder.AppendLine("<li>");
                    bodyBuilder.AppendLine($"Ürün adı: {item.ProductName}</li>");
                    bodyBuilder.AppendLine($"Adet: {item.Quantity}</li>");
                    bodyBuilder.AppendLine($"Birim Fiyat: {item.UnitPrice}</li>");
                    bodyBuilder.AppendLine($"Toplam Fiyat: {item.TotalPrice}</li>");
                    bodyBuilder.AppendLine("</br>");
                }

                bodyBuilder.AppendLine("</ul>");

                message.Body = bodyBuilder.ToString();

            }
        }
    }
}