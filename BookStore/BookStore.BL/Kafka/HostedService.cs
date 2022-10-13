using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Kafka
{
    public class HostedService //: IHostedService
    {
        //private KafkaConsumerService<int, string> _kafkaConsumerService;
        //private KafkaProducerService<int, string> _kafkaProducerService;
        //private Message<int, string> _message;

        //public HostedService(KafkaConsumerService<int, string> kafkaConsumerService, KafkaProducerService<int, string> kafkaProducerService, , Message<int, string> message)
        //{
        //    _kafkaConsumerService = kafkaConsumerService;
        //    _kafkaProducerService = kafkaProducerService;
        //    _message = message;
        //}
        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _kafkaConsumerService.Consume();
        //    _kafkaProducerService.Produce();
        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
