using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models.Configurations
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public string AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
    }
}
