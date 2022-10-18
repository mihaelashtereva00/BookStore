namespace BookStore.Models.Models
{
    public class Purchase
    {
        public Guid Id { get; init; }
        public IEnumerable<Book> Books { get; set; }// = Enumerable.Empty<Book>();
        public decimal TotalMoney { get; set; }
        public int UserId { get; set; }
    }
}
