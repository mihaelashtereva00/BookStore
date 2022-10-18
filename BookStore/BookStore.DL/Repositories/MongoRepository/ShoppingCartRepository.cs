using BookStore.DL.Interfaces;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models.User;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<ShoppingCart> _collection;
        IOptions<MongoDbConfiguration> _options;

        public ShoppingCartRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<ShoppingCart>("ShoppingCart");
        }

        public async Task<ShoppingCart> AddToShoppingCart(ShoppingCart cart)
        {

            var document = new ShoppingCart()
            {
                UserId = cart.UserId,
                Books = cart.Books
            };

            _collection.InsertOne(document);

            return await Task.FromResult(document);
        }

        public async Task<ShoppingCart> RemoveFromShoppingCart(ShoppingCart cart)
        {
            var filter = Builders<ShoppingCart>.Filter.Eq(s => s.Id, cart.Id);
            var result = _collection.ReplaceOneAsync(filter, cart);

            return await Task.FromResult(cart);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            var filter = Builders<ShoppingCart>.Filter.Eq(s => s.Id, cart.Id);
            var result = _collection.ReplaceOneAsync(filter, cart);

            return await Task.FromResult(cart);
        }

        public async Task<ShoppingCart> Get(int userId)
        {
            var filter = Builders<ShoppingCart>.Filter.Eq("UserId", userId);
            var cart = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(cart);
        }
    }
}
