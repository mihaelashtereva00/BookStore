using BookStore.Caches;
using BookStore.Caches.Settings;
using BookStore.DL.Interfaces;
using BookStore.Models;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace BookStore.BL.DeliveryPurchaseConsumer
{
    public class PurchaseConsumerService : KafkaConsumerService<Guid, Purchase>// where Purchase : ICacheItem<Guid>, IHostedService
    {
        IBookRepository _bookRepository;
        TransformBlock<Purchase, Purchase> _transferBlockPurchase;
        private CancellationTokenSource _cancellationTokenSource;

        public PurchaseConsumerService(IOptions<KafkaSettingsConsumer> kafkaSettings,
            IBookRepository bookRepository)
            : base(kafkaSettings)
        {
            _bookRepository = bookRepository;
            _cancellationTokenSource = new CancellationTokenSource();

            _transferBlockPurchase = new TransformBlock<Purchase, Purchase>(purchase =>
            {
                if (purchase.Books != null && purchase.Books.Count() != 0)
                {
                    foreach (var book in purchase.Books)
                    {
                        var b = _bookRepository.GetById(book.Id).Result;

                        if (b != null)
                        {
                            b.Quantity--;
                            _bookRepository.UpdateBook(b);

                        }
                    }
                }

                return purchase;
            });

            var actionBlock = new ActionBlock<Purchase>(p =>
            {
                _transferBlockPurchase.Post(p);
                Console.WriteLine($"Book with id: {p.Id} was decremented");
            });

            _transferBlockPurchase.LinkTo(actionBlock);

        }

        public override void HandleMessage(Purchase purchase) //purchase
        {
            _transferBlockPurchase.SendAsync(purchase);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    HandleMessage(cr.Message.Value);
                };
            });
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }
}
