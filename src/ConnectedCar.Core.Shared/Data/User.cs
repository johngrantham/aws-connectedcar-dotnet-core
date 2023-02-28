using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data
{
    public class User : Validatable
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Password);
        }
    }
}
