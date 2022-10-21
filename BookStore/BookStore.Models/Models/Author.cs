namespace BookStore.Models.Models
{
    public record Author : Person
    {
        public DateTime DateOfBirth { get; init; }
        public string Nickname { get; init; }
    }
}
