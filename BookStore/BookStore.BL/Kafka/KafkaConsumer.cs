using BookStore.MessagePack;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class KafkaConsumer<Key, Value> : IHostedService
    {

        IOptions<KafkaSettings> _kafkaSettings;
        public IConsumer<Key, Value> _consumer;
        private ConsumerConfig _consumerConfig;


        public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _kafkaSettings.Value.GroupId
            };

            _consumer = new ConsumerBuilder<Key, Value>(_consumerConfig)
                    .SetValueDeserializer(new MsgPackDeserializer<Value>())
                    .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("Topic");
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    Console.WriteLine($"Recieved msg with key:{cr.Message.Key} value:{cr.Message.Value}");
                };
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
