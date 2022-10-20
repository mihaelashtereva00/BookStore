namespace BookStore.Models
{
    public interface ICacheItem<out T>
    {
        T GetKey();

    }
}
