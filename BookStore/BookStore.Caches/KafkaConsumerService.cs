using BookStore.Caches.Settings;
using BookStore.MessagePack;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using BookStore.Models;

namespace BookStore.Caches
{
    public abstract class KafkaConsumerService<Key, Value> : IHostedService 
    {
        public IConsumer<Key, Value> _consumer;
        private IOptions<KafkaSettingsConsumer> _kafkaSettings;
        private ConsumerConfig _consumerConfig;
        //private List<Value> _list;
        private Message<Key, Value> _message;
        private CancellationTokenSource _cancellationTokenSource;

        public KafkaConsumerService(IOptions<KafkaSettingsConsumer> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.Value.AutoOffsetReset,
                GroupId = _kafkaSettings.Value.GroupId
            };

            _consumer = new ConsumerBuilder<Key, Value>(_consumerConfig)
                    .SetKeyDeserializer(new MsgPackDeserializer<Key>())
                    .SetValueDeserializer(new MsgPackDeserializer<Value>())
                    .Build();

            _consumer.Subscribe(typeof(Value).Name);

            _cancellationTokenSource  = new CancellationTokenSource();
        }

        public abstract void HandleMessage(Value value);

        public abstract Task StartAsync(CancellationToken cancellationToken);
        public abstract Task StopAsync(CancellationToken cancellationToken);
    }
}
