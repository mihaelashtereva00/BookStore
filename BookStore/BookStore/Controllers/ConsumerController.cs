using BookStore.Caches;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController
    {
        private readonly KafkaCacheService<int, Book> _kafkaCahce;

        public ConsumerController(KafkaCacheService<int, Book> kafkaCahce)
        {
            _kafkaCahce = kafkaCahce;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public Task<List<Book>> GetAll()
        {
            return _kafkaCahce.GetData();
        }
    }
}
