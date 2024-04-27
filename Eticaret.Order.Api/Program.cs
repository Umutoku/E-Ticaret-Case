using ETicaret.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Eticaret.Order.Application;
using MassTransit;
using MassTransit.KafkaIntegration;
using Eticaret.Order.Application.Consumers;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("ETicaret.Order.Infrastructure");

    });

});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Eticaret.Order.Application.Handlers.CreateOrderCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Eticaret.Order.Application.Handlers.CreateInvoiceCommandHandler).Assembly));


//builder.Services.AddMassTransit(x =>
//{
//    x.UsingInMemory();

//    x.AddRider(rider =>
//    {
//        rider.AddConsumer<CreateOrderMessageCommandConsumer>();

//        rider.UsingKafka((context, k) =>
//        {

//            k.Host("localhost:9092");

//            k.TopicEndpoint<Null, string>("topic-name", "consumer-group-name", e =>
//            {
//                e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
//            });
//        });
//    });
//});

builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<CreateOrderMessageCommandConsumer>();
    x.AddConsumer<CreateInvoiceMessageCommandConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-order-service", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });

        cfg.ReceiveEndpoint("create-invoice-service", e =>
        {
            e.ConfigureConsumer<CreateInvoiceMessageCommandConsumer>(context);
        });

        cfg.UseMessageRetry(r => r.Interval(2, 100));
    });
});

var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
