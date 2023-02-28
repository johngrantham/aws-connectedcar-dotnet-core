using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Attributes
{
    public class Colors : Validatable
    {
        [JsonProperty("interior")]
        public string Interior { get; set; }

        [JsonProperty("exterior")]
        public string Exterior { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Interior) &&
                   !string.IsNullOrEmpty(Exterior);
        }
    }
}
