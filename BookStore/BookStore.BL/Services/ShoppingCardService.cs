using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.User;

namespace BookStore.BL.Services
{
    public class ShoppingCardService : IShoppingCard
    {
        private readonly IPurcahseRepository _purchaseRepository; //shopping card repo
        private readonly IShoppingCartRepository _cart;

        public ShoppingCardService(IPurcahseRepository repository, IShoppingCartRepository cart)
        {
            _purchaseRepository = repository;
            _cart = cart;
        }

        public Task<ShoppingCart> AddToCart(ShoppingCart cart)
        {
            var result = _cart.AddToShoppingCart(cart);
            return result;
        }


        public Task<ShoppingCart> Update(ShoppingCart cart)
        {
            var result = _cart.UpdateCart(cart);
            return result;
        }

        public Task<ShoppingCart> FinishPurchase(int userId) //shopping card -> purchase repo 
        {
            var crt = _cart.Get(userId);

            var shoppingCart = new ShoppingCart()
            {
                Id = crt.Result.Id,
                UserId = crt.Result.UserId,
                Books = new Book[0]
            };

            var prc = new Purchase()
            {
                UserId = userId,
                Books = crt.Result.Books,
                Id = crt.Result.Id,
                TotalMoney = crt.Result.Books.Select(b => b.Price).Sum()
            };

            var result = _purchaseRepository.SavePurchase(prc);
            _cart.RemoveFromShoppingCart(shoppingCart);

            return Task.FromResult(shoppingCart);
        }

        public Task<ShoppingCart> GetContent(int userId)
        {
            var result = _cart.Get(userId);
            return result;
        }

        public  Task<ShoppingCart> RemoveFromCart(ShoppingCart cart)
        {
            var result = _cart.RemoveFromShoppingCart(cart);
            return  result;
        }
    }
}
