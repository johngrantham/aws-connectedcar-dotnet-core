using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Shared.Data.Enums;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class EventItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "vin")]
        public string Vin { get; set; }

        [DynamoDBRangeKey(attributeName: "timestamp")]
        public long Timestamp { get; set; }

        [DynamoDBProperty(attributeName: "eventCode")]
        public EventCodeEnum EventCode { get; set; }

     }
}
