using BookStore.Caches;
using BookStore.Caches.Settings;
using BookStore.DL.Interfaces;
using BookStore.Models;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace BookStore.BL.DeliveryPurchaseConsumer
{
    public class PurchaseConsumerService : KafkaConsumerService<Guid, Purchase>
    {
        private IBookRepository _bookRepository;
        private TransformBlock<Purchase, Purchase> _transferBlockPurchase;
        private CancellationTokenSource _cancellationTokenSource;
        private HttpClientProvider _service;
        private IOptions<HttpClientConfig> _httpClientConfig;

        public PurchaseConsumerService(IOptions<KafkaSettingsConsumer> kafkaSettings,
            IBookRepository bookRepository,
            IOptions<HttpClientConfig> httpClientConfig)
            : base(kafkaSettings)
        {
            _httpClientConfig = httpClientConfig;
            _bookRepository = bookRepository;
            _cancellationTokenSource = new CancellationTokenSource();
            _service = new HttpClientProvider(_httpClientConfig);

            _transferBlockPurchase = new TransformBlock<Purchase, Purchase>(purchase =>
            {
                List<string> list = new List<string>();

                if (purchase.Books != null && purchase.Books.Count() != 0)
                {
                    var aditionalInfo = _service.AddAditionalInfo().Result.Distinct();

                    foreach (var book in purchase.Books)
                    {
                        var b = _bookRepository.GetById(book.Id).Result;

                        if (b != null)
                        {
                            b.Quantity--;
                            _bookRepository.UpdateBook(b);
                        }

                        if (aditionalInfo.Count() != 0)
                        {
                            var info = aditionalInfo.FirstOrDefault(a => a.AuthorId == b.AuthorId);

                            if (info != null)
                            {
                                list.Add(info.AditionalInfo);
                            }
                        }
                    }
                }
                purchase.AdditionalInfo = list;

                Console.WriteLine(purchase.AdditionalInfo.Count());

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
