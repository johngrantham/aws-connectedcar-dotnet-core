using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Enums;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Dealer : BaseEntity
    {
        [JsonProperty("dealerId")]
        public string DealerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("stateCode")]
        public StateCodeEnum StateCode { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Name) &&
                   Address != null && Address.Validate();
        }
     }
}
