using MassTransit;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region MassTransit Configuration
    builder.Services.AddMassTransit(config =>
    {
        config.UsingRabbitMq((context, c) =>
        {
            c.Host("amqp://guest:guest@localhost:5672/");
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

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}