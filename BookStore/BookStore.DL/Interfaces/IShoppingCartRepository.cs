using BookStore.Models.Models.User;

namespace BookStore.DL.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task<ShoppingCart> AddToShoppingCart(ShoppingCart cart);
        public Task<ShoppingCart> RemoveFromShoppingCart(ShoppingCart cart);
        public Task<ShoppingCart> UpdateCart(ShoppingCart cart);
        public Task<ShoppingCart> Get(int userId);
    }
}
