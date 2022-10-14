using BookStore.BL.Interfaces;
using BookStore.Caches;
using BookStore.Models.Models;
using BookStore.Models.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController
    {
        private readonly KafkaHostedService<int, Book> _kafkaHostedService;
        private readonly CancellationTokenSource _token;
        private readonly List<Book> _books;

        public ConsumerController(KafkaHostedService<int, Book> kafkaHostedService)
        {
            _kafkaHostedService = kafkaHostedService;
            _token = new CancellationTokenSource();
            _books = new List<Book>();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public  Task<List<Book>> GetAll()
        {
            return  _kafkaHostedService.StartAsync(_books, _token.Token);
           // return  Task.FromResult(_books);
        }
    }
}
