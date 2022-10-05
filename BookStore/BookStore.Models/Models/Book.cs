namespace BookStore.Models.Models
{
    public record Book
    {
        public int Id { get; init; }
        public int AuthorId { get; init; }
        public string Title { get; init; }
        public DateTime LastUpdated { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
