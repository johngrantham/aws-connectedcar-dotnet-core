using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Services.Data.Converters;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class DealerItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "dealerId")]
        public string DealerId { get; set; }

        [DynamoDBProperty(attributeName: "name")]
        public string Name { get; set; }

        [DynamoDBProperty(attributeName: "address", typeof(AddressConverter))]
        public Address Address { get; set; }

        [DynamoDBProperty(attributeName: "stateCode")]
        public StateCodeEnum StateCode { get; set; }
        
     }
}
