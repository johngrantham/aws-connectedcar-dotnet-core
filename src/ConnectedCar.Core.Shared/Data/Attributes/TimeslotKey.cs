using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Attributes
{
    public class TimeslotKey : Validatable
    {
        [JsonProperty("dealerId")]
        public string DealerId { get; set; }

        [JsonProperty("serviceDateHour")]
        public string ServiceDateHour { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(DealerId) &&
                   !string.IsNullOrEmpty(ServiceDateHour);
        }
    }
}
