using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Updates
{
    public class CustomerProvision : User
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(Firstname) &&
                   !string.IsNullOrEmpty(Lastname) &&
                   !string.IsNullOrEmpty(PhoneNumber);
        }
    }
}
