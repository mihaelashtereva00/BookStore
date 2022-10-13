using BookStore.BL.Kafka;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController
    {
        private KafkaProducerService<int,string> _kafkaProducerService;

        public MessageController( KafkaProducerService<int, string> kafkaProducerService)
        {
            _kafkaProducerService = kafkaProducerService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Send message")]
        public Task SendMessage([FromBody] int p1, string p2)
        {
            _kafkaProducerService.Produce(p1,p2);
            return Task.CompletedTask;
        }
    }
}
