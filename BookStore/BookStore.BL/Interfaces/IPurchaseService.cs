using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> GetPurchases(int userId);
        public Task<Purchase?> SavePurchase(Purchase purchase);
        public Task<Purchase> DeletePurchase(Purchase purchase); //get basket, finish purchase, modify - remove 
    }
}
