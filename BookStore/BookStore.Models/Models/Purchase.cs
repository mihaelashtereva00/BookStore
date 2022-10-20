using MessagePack;
using BookStore.Models;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public class Purchase : ICacheItem<Guid>
    {
        [Key(0)]
        public Guid Id { get; init; }
        [Key(1)]
        public IEnumerable<Book> Books { get; set; }
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
        [Key(4)]
        public IEnumerable<string> AdditionalInfo { get; set; } = Enumerable.Empty<string>(); //!!!

        public Guid GetKey() => Id;
    }
}
