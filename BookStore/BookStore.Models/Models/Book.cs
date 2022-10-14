using MessagePack;
using WebAPI.Models;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public record Book : ICacheItem<int>
    {
        [Key(0)]
        public int Id { get; init; }
        [Key(1)]
        public int AuthorId { get; init; }
        [Key(2)]
        public string Title { get; init; }
        [Key(3)]
        public DateTime LastUpdated { get; set; }
        [Key(4)]
        public int Quantity { get; set; }
        [Key(5)]
        public decimal Price { get; set; }

        public int GetKey() => Id;
    }
}
