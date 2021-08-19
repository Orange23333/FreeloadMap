using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace FreeloadMap.Data
{
    public class UrgeMoreInfo
    {
        [JsonProperty(nameof(ID))]
        public Guid ID { get; set; }

        [JsonProperty(nameof(DateTime))]
        public DateTime DateTime { get; set; }

        [JsonProperty(nameof(IPEndPoint))]
        public string IPEndPoint { get; set; }

        [JsonProperty(nameof(UserAgent))]
        public string UserAgent { get; set; }

        [JsonProperty(nameof(Message))]
        public string Message { get; set; }

        public string ToNormalText()
        {
            return String.Format("[{0}]: {1}", this.DateTime.ToLocalTime().ToString("\"UTC\"zz yyyy.MM.dd HH:mm:ss"), this.Message);
        }
    }
}
