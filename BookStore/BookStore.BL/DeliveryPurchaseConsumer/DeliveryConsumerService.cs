using BookStore.Caches;
using BookStore.Caches.Settings;
using BookStore.DL.Interfaces;
using Confluent.Kafka;
using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace BookStore.BL.DeliveryPurchaseConsumer
{
    public class DeliveryConsumerService : KafkaConsumerService<int, Delivery>
    {
        IBookRepository _bookRepository;
        TransformBlock<Delivery, Delivery> transferBlockDeliverye;
        private CancellationTokenSource _cancellationTokenSource;

        public DeliveryConsumerService(IOptions<KafkaSettingsConsumer> kafkaSettings,
            IBookRepository bookRepository)
            : base(kafkaSettings)
        {
            _bookRepository = bookRepository;
            _cancellationTokenSource = new CancellationTokenSource();

             transferBlockDeliverye = new TransformBlock<Delivery, Delivery>(delivery =>
            {
                if (delivery != null && delivery.Book != null)
                {
                    var book = _bookRepository.GetById(delivery.Book.Id).Result;
                    if (book != null)
                    {
                        book.Quantity++;
                        _bookRepository.UpdateBook(book);
                    }
                }
                return delivery;
            });

            var actionBlock = new ActionBlock<Delivery>(p =>
            {
                transferBlockDeliverye.Post(p);
                Console.WriteLine($"The quantity of book with Id {p.Id} was incremented to {p.Book.Quantity}");
            });

            transferBlockDeliverye.LinkTo(actionBlock);
        }

        public override void HandleMessage(Delivery delivery) 
        {
            transferBlockDeliverye.SendAsync(delivery);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    HandleMessage(cr.Message.Value);
                }
            });

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }
}
