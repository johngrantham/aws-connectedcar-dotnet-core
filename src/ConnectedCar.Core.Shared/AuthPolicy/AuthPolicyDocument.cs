using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.AuthPolicy
{
    public class AuthPolicyDocument
    {
        [JsonProperty(PropertyName = "Version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "Statement")]
        public List<AuthPolicyStatement> Statement { get; set; }
    }
}