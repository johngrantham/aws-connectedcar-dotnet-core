using ConnectedCar.Core.Shared.Data.Enums;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Registration : BaseEntity
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("vin")]
        public string Vin { get; set; }

        [JsonProperty("statusCode")]
        public StatusCodeEnum StatusCode { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Vin);
        }
    }
}
