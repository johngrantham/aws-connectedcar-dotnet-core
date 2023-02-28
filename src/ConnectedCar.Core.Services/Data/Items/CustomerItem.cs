using Amazon.DynamoDBv2.DataModel;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class CustomerItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "username")]
        public string Username { get; set; }

        [DynamoDBProperty(attributeName: "firstname")]
        public string Firstname { get; set; }

        [DynamoDBProperty(attributeName: "lastname")]
        public string Lastname { get; set; }

        [DynamoDBProperty(attributeName: "lastnameLower")]
        public string LastnameLower { get; set; }

        [DynamoDBProperty(attributeName: "phoneNumber")]
        public string PhoneNumber { get; set; }
   }
}
