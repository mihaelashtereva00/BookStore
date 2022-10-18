using BookStore.Models.Models;

namespace BookStore.Models.Requests
{
    public class PurchaseRequest
    {
        public IEnumerable<Book> Books { get; set; }// = Enumerable.Empty<Book>();
        public decimal TotalMoney { get; set; }
        public int UserId { get; set; }

    }

}

