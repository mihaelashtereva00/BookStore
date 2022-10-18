using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoRepository
{
    public class PurchaseRepository : IPurcahseRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Purchase> _collection;
        IOptions<MongoDbConfiguration> _options;

        public PurchaseRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<Purchase>("Purchase");
        }
        
        public async Task<Purchase> GetPurchases(int userId) 
        {
            var filter = Builders<Purchase>.Filter.Eq("UserId", userId);
            var purchase = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(purchase); 
        }

        public async Task<Purchase?> SavePurchase(Purchase purchase) 
        {
            var document = new Purchase()
            {
                Books = purchase.Books,
                TotalMoney = purchase.TotalMoney,
                UserId = purchase.UserId
            };

            _collection.InsertOne(document);

            return await Task.FromResult(document);
        }

        public async Task<Purchase> DeletePurchase(Purchase purchase) 
        {
            _collection.DeleteOne(x => x.Id == purchase.Id);
            return await Task.FromResult(purchase);
        }
    }
}
