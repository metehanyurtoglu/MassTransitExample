using Application.Models;
using MassTransit;

namespace ConsumerAPI.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderConsumer> logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            logger.LogInformation($"New message {context.Message.Name}");
        }
    }
}
