using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MessagePack;
using BookStore.MessagePack;

namespace BookStore.BL.Kafka
{
    [MessagePackObject]
    public class KafkaProducerService<Key, Value>
    {
        IOptions<KafkaSettingsProducer> _kafkaSettings;
        public KafkaProducerService(IOptions<KafkaSettingsProducer> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
        }

        public void Produce(Key key, Value value)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
            };

            var producer = new ProducerBuilder<Key, Value>(config)
                .SetValueSerializer(new MsgPackSerializer<Value>())
                .Build();

            var msg = new Message<Key, Value>() { Key = key, Value = value};

            var result = producer.ProduceAsync("Topic", msg);
        }
    }
}
