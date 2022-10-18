using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPurcahseRepository
    {
        public Task<Purchase> GetPurchases(int userId);
        public Task<Purchase?> SavePurchase(Purchase purchase);
        public Task<Purchase> DeletePurchase(Purchase purchase);
    }
}
