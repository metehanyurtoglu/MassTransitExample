using Application.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ProducerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _publishEndpoint.Publish<Order>(order);

            return Ok();
        }
    }
}