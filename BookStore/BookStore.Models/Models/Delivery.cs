using BookStore.Models.Models;
using MessagePack;
using BookStore.Models;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public class Delivery : ICacheItem<int>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public Book Book { get; set; }
        [Key(2)]
        public int Quantity { get; set; }

        public int GetKey() => Id;
    }
}
