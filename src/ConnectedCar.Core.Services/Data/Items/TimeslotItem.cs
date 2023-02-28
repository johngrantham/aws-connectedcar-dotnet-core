using Amazon.DynamoDBv2.DataModel;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class TimeslotItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "dealerId")]
        public string DealerId { get; set; }

        [DynamoDBRangeKey(attributeName: "serviceDateHour")]
        public string ServiceDateHour { get; set; }

        [DynamoDBProperty(attributeName: "serviceBayCount")]
        public int ServiceBayCount { get; set; }

     }
}
