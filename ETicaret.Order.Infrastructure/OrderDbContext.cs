using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Order.Domain;

namespace ETicaret.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {

        public const string DEFAULT_SCHEMA = "orders";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Eticaret.Order.Domain.Order> Orders { get; set; }
        public DbSet<Eticaret.Order.Domain.OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eticaret.Order.Domain.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<Eticaret.Order.Domain.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
