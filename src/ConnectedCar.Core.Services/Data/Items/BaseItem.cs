using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Services.Data.Converters;
using System;

namespace ConnectedCar.Core.Services.Data.Items
{
    public abstract class BaseItem
    {
        public const string TimeslotAppointmentIndex = "TimeslotAppointmentIndex";
        public const string RegistrationAppointmentIndex = "RegistrationAppointmentIndex";
        public const string VehicleRegistrationIndex = "VehicleRegistrationIndex";
        
        [DynamoDBProperty(attributeName: "createDateTime", typeof(DateTimeConverter))]
        public DateTime CreateDateTime { get; set; }

        [DynamoDBProperty(attributeName: "updateDateTime", typeof(DateTimeConverter))]
        public DateTime UpdateDateTime { get; set; }
    }
}
