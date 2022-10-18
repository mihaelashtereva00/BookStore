using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IPurcahseRepository _purchaseRepository;

        public PurchaseService(IPurcahseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public Task<Purchase> DeletePurchase(Purchase purchase)
        {
            return _purchaseRepository.DeletePurchase(purchase);
        }

        public Task<Purchase> GetPurchases(int userId)
        {
           return _purchaseRepository.GetPurchases(userId);
        }

        public Task<Purchase?> SavePurchase(Purchase purchase)
        {
            return _purchaseRepository.SavePurchase(purchase);
        }
    }
}
