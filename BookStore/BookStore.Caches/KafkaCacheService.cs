using BookStore.Models;

namespace BookStore.Caches
{
    public class KafkaCacheService<Key, Value> where Value : ICacheItem<Key>
    {
        private KafkaConsumerService<Key, Value> _consumer;
        private List<Value> _list;
        private CancellationTokenSource _token;

        public KafkaCacheService(KafkaConsumerService<Key, Value> consumer)
        {
            _consumer = consumer;
            _list = new List<Value>();  
            _token = new CancellationTokenSource();

            //_consumer.StartAsync(_list, _token.Token);
        }

        public async Task<List<Value>> GetData()
        {
            return await Task.FromResult(_list);
        }
    }
}
