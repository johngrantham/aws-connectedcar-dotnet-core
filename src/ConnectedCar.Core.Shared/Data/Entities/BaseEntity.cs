using System;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public abstract class BaseEntity : Validatable
    {
        
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }

        [JsonProperty("updateDateTime")]
        public DateTime UpdateDateTime { get; set; }
    }
}
