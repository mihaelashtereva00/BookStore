namespace BookStore.Models.Models.Configurations
{
    public class KafkaSettings

    {
        public string BootstrapServers { get; set; }
        public string AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
    }
}
