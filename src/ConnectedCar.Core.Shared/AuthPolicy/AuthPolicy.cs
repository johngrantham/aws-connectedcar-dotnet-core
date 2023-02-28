using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.AuthPolicy
{
    public class AuthPolicy
    {
        [JsonProperty(PropertyName = "principalId")]
        public string PrincipalId { get; set; }

        [JsonProperty(PropertyName = "policyDocument")]
        public AuthPolicyDocument PolicyDocument { get; set; }
    }
}