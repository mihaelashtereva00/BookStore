namespace BookStore.Caches.Settings
{
    public class KafkaSettingsConsumer
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
    }
}
