using ConnectedCar.Core.Shared.Data.Enums;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Event : BaseEntity
    {
        [JsonProperty("vin")]
        public string Vin { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("eventCode")]
        public EventCodeEnum EventCode { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Vin) &&
                   Timestamp > 0L;
        }
     }
}
