using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Attributes
{
    public class Address : Validatable
    {
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string  State { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(StreetAddress) &&
                   !string.IsNullOrEmpty(City) &&
                   !string.IsNullOrEmpty(State) &&
                   !string.IsNullOrEmpty(ZipCode);
        }
    }
}
