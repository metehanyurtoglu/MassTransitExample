using ConsumerAPI.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region MassTransit Configuration
    builder.Services.AddMassTransit(config =>
    {
        config.AddConsumer<OrderConsumer>();

        config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("amqp://guest:guest@localhost:5672/");

            cfg.ReceiveEndpoint("order-queue", c =>
            {
                c.ConfigureConsumer<OrderConsumer>(context);
            });
        });
    });

    builder.Services.Configure<MassTransitHostOptions>(options =>
    {

    });
    #endregion
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.Run();
}