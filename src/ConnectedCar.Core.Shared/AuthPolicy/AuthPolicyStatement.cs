using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.AuthPolicy
{
    public class AuthPolicyStatement
    {
        [JsonProperty(PropertyName = "Action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "Effect")]
        public string Effect { get; set; }

        [JsonProperty(PropertyName = "Resource")]
        public string Resource { get; set; }
    }
}