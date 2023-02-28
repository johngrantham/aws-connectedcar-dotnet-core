using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Vehicle : BaseEntity
    {
        [JsonProperty("vin")]
        public string Vin { get; set; }

        [JsonProperty("colors")]
        public Colors Colors { get; set; }

        [JsonProperty("vehiclePin")]
        public string VehiclePin { get; set; }

        [JsonProperty("vehicleCode")]
        public VehicleCodeEnum VehicleCode { get; set; }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Vin) &&
                   Colors != null && Colors.Validate() &&
                   !string.IsNullOrEmpty(VehiclePin);
        }
     }
}
