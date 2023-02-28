using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Timeslot : BaseEntity
    {
        [JsonProperty("dealerId")]
        public string DealerId { get; set; }

        [JsonProperty("serviceDateHour")]
        public string ServiceDateHour { get; set; }

        [JsonProperty("serviceBayCount")]
        public int ServiceBayCount { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(DealerId) &&
                   !string.IsNullOrEmpty(ServiceDateHour);
        }
     }
}
