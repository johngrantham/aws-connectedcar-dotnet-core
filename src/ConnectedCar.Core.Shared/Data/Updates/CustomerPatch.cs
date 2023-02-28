using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Updates
{
    public class CustomerPatch : Validatable
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(PhoneNumber);
        }
    }
}
