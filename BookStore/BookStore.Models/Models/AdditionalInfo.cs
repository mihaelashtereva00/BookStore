using MessagePack;

namespace BookStore
{
    [MessagePackObject]
    public class AdditionalInfo
    {
        [Key(0)]
        public int AuthorId { get; set; }
        [Key(1)]
        public string AditionalInfo { get; set; }
    }
}