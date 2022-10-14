namespace WebAPI.Models
{
    public interface ICacheItem<out T>
    {
        T GetKey();

    }
}
