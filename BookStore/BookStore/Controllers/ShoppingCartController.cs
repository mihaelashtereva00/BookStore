using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController
    {
        private readonly IShoppingCard _shoppingCard;

        public ShoppingCartController(IShoppingCard shoppingCard)
        {
            _shoppingCard = shoppingCard;
        }

        [HttpPost(nameof(AddToCart))]
        public async Task<ShoppingCart> AddToCart(ShoppingCart shoppingCart)
        {
            return await  _shoppingCard.AddToCart(shoppingCart);
        }

        [HttpGet(nameof(FinishPurchase))]
        public async Task<ShoppingCart> FinishPurchase(int userId)
        {
            return await _shoppingCard.FinishPurchase(userId);
        }

        [HttpGet(nameof(GetContent))]
        public async Task<ShoppingCart> GetContent(int userId)
        {
            return await _shoppingCard.GetContent(userId);
        }
        
        [HttpPut(nameof(Update))]
        public async Task<ShoppingCart> Update(ShoppingCart shoppingCart)
        {
            return await _shoppingCard.Update(shoppingCart);
        }

        [HttpDelete(nameof(RemoveFromCart))]
        public async Task<ShoppingCart> RemoveFromCart(ShoppingCart shoppingCart)
        {
            return await _shoppingCard.RemoveFromCart(shoppingCart);
        }
    }
}
