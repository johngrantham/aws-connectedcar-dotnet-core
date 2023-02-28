using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Customer : BaseEntity
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Firstname) &&
                   !string.IsNullOrEmpty(Lastname) &&
                   !string.IsNullOrEmpty(PhoneNumber);
        }
    }
}
