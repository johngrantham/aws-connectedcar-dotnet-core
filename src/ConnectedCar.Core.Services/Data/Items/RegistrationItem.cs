using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Shared.Data.Enums;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class RegistrationItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "username")]
        [DynamoDBGlobalSecondaryIndexRangeKey(indexName: VehicleRegistrationIndex)]
        public string Username { get; set; }

        [DynamoDBRangeKey(attributeName: "vin")]
        [DynamoDBGlobalSecondaryIndexHashKey(indexName: VehicleRegistrationIndex)]
        public string Vin { get; set; }

        [DynamoDBProperty(attributeName: "statusCode")]
        public StatusCodeEnum StatusCode { get; set; }

    }
}
