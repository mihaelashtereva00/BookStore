using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.MongoRepository;
using BookStore.Models.Models;
using BookStore.Models.Models.User;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IShoppingCard
    {
        public Task<ShoppingCart> AddToCart(ShoppingCart cart);

        public Task<ShoppingCart> Update(ShoppingCart cart);
        public Task<ShoppingCart> FinishPurchase(int userId);
        public Task<ShoppingCart> GetContent(int userId);
        public Task<ShoppingCart> RemoveFromCart(ShoppingCart cart);
    }
}
