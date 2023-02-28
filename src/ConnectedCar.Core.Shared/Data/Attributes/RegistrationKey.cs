using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Attributes
{
    public class RegistrationKey : Validatable
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("vin")]
        public string Vin { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Vin);
        }
    }
}
