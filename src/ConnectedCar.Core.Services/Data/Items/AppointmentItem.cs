using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Services.Data.Converters;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Services.Data.Items
{
    public class AppointmentItem : BaseItem
    {
        [DynamoDBHashKey(attributeName: "appointmentId")]
        [DynamoDBGlobalSecondaryIndexRangeKey(indexNames: new string[] { TimeslotAppointmentIndex, RegistrationAppointmentIndex })]
        public string AppointmentId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey(indexName: TimeslotAppointmentIndex)]
        [DynamoDBProperty(attributeName: "timeslotKey", typeof(TimeslotKeyConverter))]
        public TimeslotKey TimeslotKey { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey(indexName: RegistrationAppointmentIndex)]
        [DynamoDBProperty(attributeName: "registrationKey", typeof(RegistrationKeyConverter))]
        public RegistrationKey RegistrationKey { get; set; }
    }
}
