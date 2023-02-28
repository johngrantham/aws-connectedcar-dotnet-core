using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Services.Data.Converters;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class VehicleItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "vin")]
        public string Vin { get; set; }

        [DynamoDBProperty(attributeName: "colors", typeof(ColorsConverter))]
        public Colors Colors { get; set; }

        [DynamoDBProperty(attributeName: "vehiclePin")]
        public string VehiclePin { get; set; }

        [DynamoDBProperty(attributeName: "vehicleCode")]
        public VehicleCodeEnum VehicleCode { get; set; }

     }
}
